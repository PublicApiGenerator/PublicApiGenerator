using Mono.Cecil;
using System.CodeDom;

namespace PublicApiGenerator;

internal static class CodeTypeReferenceBuilder
{
    private const int MAX_COUNT = 100;

    internal static CodeTypeReference CreateCodeTypeReference(this TypeReference type, ICustomAttributeProvider? attributeProvider = null, NullableMode mode = NullableMode.Default)
    {
        return CreateCodeTypeReferenceWithNullabilityMap(type, attributeProvider.GetNullabilityMap().GetEnumerator(), mode, false);
    }

    private static CodeTypeReference CreateCodeTypeReferenceWithNullabilityMap(TypeReference type, IEnumerator<bool?> nullabilityMap, NullableMode mode, bool disableNested)
    {
        var typeName = GetTypeName(type, nullabilityMap, mode, disableNested);
        return new CodeTypeReference(typeName, CreateGenericArguments(type, nullabilityMap));
    }

    private static CodeTypeReference[] CreateGenericArguments(TypeReference type, IEnumerator<bool?> nullabilityMap)
    {
        var genericArgs = type is IGenericInstance instance ? instance.GenericArguments : type.HasGenericParameters ? type.GenericParameters.Cast<TypeReference>() : null;
        if (genericArgs == null)
        {
            return type is ArrayType arr
                ? CreateGenericArguments(arr.ElementType, nullabilityMap)
                : [];
        }

        var genericArguments = new List<CodeTypeReference>();
        foreach (var argument in genericArgs)
        {
            genericArguments.Add(CreateCodeTypeReferenceWithNullabilityMap(argument, nullabilityMap, NullableMode.Default, false));
        }
        return genericArguments.ToArray();
    }

    internal static IEnumerable<bool?> GetNullabilityMap(this ICustomAttributeProvider? attributeProvider)
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
            return Enumerable.Repeat((bool?)null, MAX_COUNT);

        var argumentValue = nullableAttr.ConstructorArguments[0].Value;
        if (argumentValue is CustomAttributeArgument[] arguments)
            return arguments.Select(a => Convert((byte)a.Value));

        return Enumerable.Repeat(Convert((byte)argumentValue), MAX_COUNT);

        // https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-metadata.md
        // returns:
        // true : explicitly nullable
        // false: explicitly not nullable
        // null : oblivious
        bool? Convert(byte value) => value switch
        {
            2 => true,
            1 => false,
            0 => null,
            _ => throw new NotSupportedException(value.ToString())
        };
    }

    // The compiler optimizes the size of metadata bypassing a sequence of bytes for value types.
    // Thus, it can even delete the entire NullableAttribute if the whole signature consists only of value types,
    // for example KeyValuePair<int, int?>, thus we can call IsNullable() only by looking first deep into the signature.
    private static bool HasAnyReferenceType(TypeReference type)
    {
        if (!type.IsValueType)
            return true;

        var genericArgs = type is IGenericInstance instance ? instance.GenericArguments : type.HasGenericParameters ? type.GenericParameters.Cast<TypeReference>() : null;
        if (genericArgs == null)
            return false;

        foreach (var argument in genericArgs)
        {
            if (HasAnyReferenceType(argument))
                return true;
        }

        return false;
    }

    private static string GetTypeName(TypeReference type, IEnumerator<bool?>? nullabilityMap, NullableMode mode, bool disableNested)
    {
        bool nullable = mode != NullableMode.Disable && (mode == NullableMode.Force || HasAnyReferenceType(type) && IsNullable());

        var typeName = GetTypeNameCore(type, nullabilityMap, nullable, disableNested);

        if (nullable && typeName != "System.Void")
            typeName = CSharpTypeKeyword.Get(typeName) + "?";

        return typeName;

        bool IsNullable()
        {
            if (nullabilityMap == null)
                return false;

            if (!nullabilityMap.MoveNext())
            {
                throw new InvalidOperationException("Not enough nullability information");
            }
            return nullabilityMap.Current == true;
        }
    }

    private static string GetTypeNameCore(TypeReference type, IEnumerator<bool?>? nullabilityMap, bool nullable, bool disableNested)
    {
        if (type.IsGenericParameter)
        {
            return type.Name;
        }

        if (type is ArrayType array)
        {
            return nullable
                ? CSharpTypeKeyword.Get(GetTypeName(array.ElementType, nullabilityMap, NullableMode.Default, disableNested)) + "[]"
                : GetTypeName(array.ElementType, nullabilityMap, NullableMode.Default, disableNested) + "[]";
        }

        if (type is PointerType pointer)
        {
            return CSharpTypeKeyword.Get(GetTypeName(pointer.ElementType, nullabilityMap, NullableMode.Default, disableNested)) + "*";
        }

        if (!type.IsNested || disableNested)
        {
            var name = type is RequiredModifierType modType ? modType.ElementType.Name : type.Name;
            return (!string.IsNullOrEmpty(type.Namespace) ? (type.Namespace + ".") : "") + name;
        }

        return GetTypeName(type.DeclaringType, null, NullableMode.Default, false) + "." + GetTypeName(type, null, NullableMode.Default, true);
    }
}
