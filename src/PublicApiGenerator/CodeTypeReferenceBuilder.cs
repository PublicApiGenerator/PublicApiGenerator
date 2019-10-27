using Mono.Cecil;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace PublicApiGenerator
{
    internal static class CodeTypeReferenceBuilder
    {
        private const int MAX_COUNT = 1000;

        internal static CodeTypeReference CreateCodeTypeReference(this TypeReference type, ICustomAttributeProvider attributeProvider = null)
        {
            return CreateCodeTypeReferenceWithNullabilityMap(type, attributeProvider.GetNullabilityMap().GetEnumerator(), false);
        }

        static CodeTypeReference CreateCodeTypeReferenceWithNullabilityMap(TypeReference type, IEnumerator<bool> nullabilityMap, bool forceNullable)
        {
            var typeName = GetTypeName(type, nullabilityMap, forceNullable);
            if (type.IsValueType && type.Name == "Nullable`1" && type.Namespace == "System")
            {
                // unwrap System.Nullable<Type> into Type? for readability
                var genericArgs = type is IGenericInstance instance ? instance.GenericArguments : type.HasGenericParameters ? type.GenericParameters.Cast<TypeReference>() : null;
                return CreateCodeTypeReferenceWithNullabilityMap(genericArgs.Single(), nullabilityMap, true);
            }
            else
            {
                return new CodeTypeReference(typeName, CreateGenericArguments(type, nullabilityMap));
            }
        }

        internal static IEnumerable<bool> GetNullabilityMap(this ICustomAttributeProvider attributeProvider)
        {
            var nullableAttr = attributeProvider?.CustomAttributes.SingleOrDefault(d => d.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute");

            if (nullableAttr == null)
            {
                foreach (var provider in NullableContext.Providers)
                {
                    nullableAttr = provider.CustomAttributes.SingleOrDefault(d => d.AttributeType.FullName == "System.Runtime.CompilerServices.NullableContextAttribute");
                    if (nullableAttr != null)
                        break;
                }
            }

            if (nullableAttr == null)
                return Enumerable.Repeat(false, MAX_COUNT);

            var value = nullableAttr.ConstructorArguments[0].Value;
            if (value is CustomAttributeArgument[] arguments)
                return arguments.Select(a => (byte)a.Value == 2);

            return Enumerable.Repeat((byte)value == 2, MAX_COUNT);
        }

        // The compiler optimizes the size of metadata bypassing a sequence of bytes for value types.
        // Thus, it can even delete the entire NullableAttribute if the whole signature consists only of value types,
        // for example KeyValuePair<int, int?>, thus we can call IsNullable() only by looking first deep into the signature
        private static bool HasAnyReferenceType(TypeReference type)
        {
            if (!type.IsValueType)
                return true;

            // ReSharper disable once RedundantEnumerableCastCall
            var genericArgs = type is IGenericInstance instance ? instance.GenericArguments : type.HasGenericParameters ? type.GenericParameters.Cast<TypeReference>() : null;
            if (genericArgs == null) return false;

            foreach (var argument in genericArgs)
            {
                if (HasAnyReferenceType(argument))
                    return true;
            }

            return false;
        }

        static string GetTypeName(TypeReference type, IEnumerator<bool> nullabilityMap, bool forceNullable)
        {
            bool nullable = forceNullable || HasAnyReferenceType(type) && IsNullable();

            var typeName = GetTypeNameCore(type, nullabilityMap, nullable);

            if (nullable)
                typeName = CSharpAlias.Get(typeName) + "?";

            return typeName;

            bool IsNullable()
            {
                if (nullabilityMap == null)
                    return false;

                if (!nullabilityMap.MoveNext())
                {
                    throw new InvalidOperationException("Not enough nullability information");
                }
                return nullabilityMap.Current;
            }
        }

        static string GetTypeNameCore(TypeReference type, IEnumerator<bool> nullabilityMap, bool nullable)
        {
            if (type.IsGenericParameter)
            {
                return type.Name;
            }

            if (type is ArrayType array)
            {
                if (nullable)
                    return CSharpAlias.Get(GetTypeName(array.ElementType, nullabilityMap, false)) + "[]";
                else
                    return GetTypeName(array.ElementType, nullabilityMap, false) + "[]";
            }

            if (!type.IsNested)
            {
                return (!string.IsNullOrEmpty(type.Namespace) ? (type.Namespace + ".") : "") + type.Name;
            }

            return GetTypeName(type.DeclaringType, null, false) + "." + GetTypeName(type, nullabilityMap, false);
        }

        static CodeTypeReference[] CreateGenericArguments(TypeReference type, IEnumerator<bool> nullabilityMap)
        {
            // ReSharper disable once RedundantEnumerableCastCall
            var genericArgs = type is IGenericInstance instance ? instance.GenericArguments : type.HasGenericParameters ? type.GenericParameters.Cast<TypeReference>() : null;
            if (genericArgs == null) return null;

            var genericArguments = new List<CodeTypeReference>();
            foreach (var argument in genericArgs)
            {
                genericArguments.Add(CreateCodeTypeReferenceWithNullabilityMap(argument, nullabilityMap, false));
            }
            return genericArguments.ToArray();
        }
    }
}
