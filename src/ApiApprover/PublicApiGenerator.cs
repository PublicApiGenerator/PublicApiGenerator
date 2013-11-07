using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using TypeAttributes = System.Reflection.TypeAttributes;

// ReSharper disable CheckNamespace
// ReSharper disable BitwiseOperatorOnEnumWithoutFlags
namespace ApiApprover
{
    public static class CecilEx
    {
        public static IEnumerable<IMemberDefinition> GetMembers(this TypeDefinition type)
        {
            return type.Fields.Cast<IMemberDefinition>()
                .Concat(type.Methods)
                .Concat(type.Properties)
                .Concat(type.Events);
        }
    }


    public static class PublicApiGenerator
    {
        // TODO: Assembly attributes
        // TODO: Assembly references?
        // TODO: Better handle namespaces - using statements?
        // TODO: Constant values for fields
        // TODO: Default values for parameters
        // TODO: Constructor parameters for attributes
        // TODO: Generic methods
        public static string CreatePublicApiForAssembly(AssemblyDefinition assembly)
        {
            return CreatePublicApiForAssembly(assembly, t => true);
        }

        public static string CreatePublicApiForAssembly(AssemblyDefinition assembly, Func<TypeDefinition, bool> shouldIncludeType)
        {
            var publicApiBuilder = new StringBuilder();
            var cgo = new CodeGeneratorOptions
            {
                BracingStyle = "C",
                BlankLinesBetweenMembers = false
            };

            using (var provider = new CSharpCodeProvider())
            {
                var publicTypes = assembly.Modules.SelectMany(m => m.GetTypes())
                    .Where(t => ShouldIncludeType(t) && shouldIncludeType(t))
                    .OrderBy(t => t.FullName);
                foreach (var publicType in publicTypes)
                {
                    var writer = new StringWriter();
                    var typeDeclaration = CreateTypeDeclaration(publicType);

                    var @namespace = new CodeNamespace(publicType.Namespace);
                    @namespace.Types.Add(typeDeclaration);
                    provider.GenerateCodeFromNamespace(@namespace, writer, cgo);
                    var typeDeclarationText = NormaliseGeneratedCode(writer);
                    publicApiBuilder.AppendLine(typeDeclarationText);
                }
            }
            return publicApiBuilder.ToString().Trim();
        }

        private static bool IsDelegate(TypeDefinition publicType)
        {
            return publicType.BaseType != null && publicType.BaseType.FullName == "System.MulticastDelegate";
        }

        private static bool ShouldIncludeType(TypeDefinition t)
        {
            return t.IsPublic && !IsCompilerGenerated(t);
        }

        private static bool ShouldIncludeMember(IMemberDefinition m)
        {
            return !IsCompilerGenerated(m) && !IsDotNetTypeMember(m) && !(m is FieldDefinition);
        }

        private static bool IsCompilerGenerated(IMemberDefinition m)
        {
            return m.CustomAttributes.Any(a => a.AttributeType.FullName == "System.Runtime.CompilerServices.CompilerGeneratedAttribute");
        }

        private static bool IsDotNetTypeMember(IMemberDefinition m)
        {
            if (m.DeclaringType == null || m.DeclaringType.FullName == null)
                return false;
            return m.DeclaringType.FullName.StartsWith("System") || m.DeclaringType.FullName.StartsWith("Microsoft");
        }

        static void AddMemberToTypeDeclaration(CodeTypeDeclaration typeDeclaration, IMemberDefinition memberInfo)
        {
            var methodDefinition = memberInfo as MethodDefinition;
            if (methodDefinition != null)
            {
                if (methodDefinition.IsConstructor)
                    AddCtorToTypeDeclaration(typeDeclaration, methodDefinition);
                else
                    AddMethodToTypeDeclaration(typeDeclaration, methodDefinition);
            }
            else if (memberInfo is PropertyDefinition)
            {
                AddPropertyToTypeDeclaration(typeDeclaration, (PropertyDefinition) memberInfo);
            }
            else if (memberInfo is EventDefinition)
            {
                typeDeclaration.Members.Add(GenerateEvent((EventDefinition)memberInfo));
            }
            else if (memberInfo is FieldDefinition)
            {
                AddFieldToTypeDeclaration(typeDeclaration, (FieldDefinition) memberInfo);
            }
        }

        static string NormaliseGeneratedCode(StringWriter writer)
        {
            var gennedClass = writer.ToString();
            const string emptyGetSet = @"\s+{\s+get\s+{\s+}\s+set\s+{\s+}\s+}";
            const string emptyGet = @"\s+{\s+get\s+{\s+}\s+}";
            const string emptySet = @"\s+{\s+set\s+{\s+}\s+}";
            const string getSet = @"\s+{\s+get;\s+set;\s+}";
            const string get = @"\s+{\s+get;\s+}";
            gennedClass = Regex.Replace(gennedClass, emptyGetSet, " { get; set; }", RegexOptions.IgnorePatternWhitespace);
            gennedClass = Regex.Replace(gennedClass, getSet, " { get; set; }", RegexOptions.IgnorePatternWhitespace);
            gennedClass = Regex.Replace(gennedClass, emptyGet, " { get; }", RegexOptions.IgnorePatternWhitespace);
            gennedClass = Regex.Replace(gennedClass, emptySet, " { set; }", RegexOptions.IgnorePatternWhitespace);
            gennedClass = Regex.Replace(gennedClass, get, " { get; }", RegexOptions.IgnorePatternWhitespace);
            gennedClass = Regex.Replace(gennedClass, @"\s+{\s+}", " { }", RegexOptions.IgnorePatternWhitespace);
            gennedClass = Regex.Replace(gennedClass, @"\)\s+;", ");", RegexOptions.IgnorePatternWhitespace);
            return gennedClass;
        }

        static CodeTypeDeclaration CreateTypeDeclaration(TypeDefinition publicType)
        {
            if (IsDelegate(publicType))
                return CreateDelegateDeclaration(publicType);

            bool @static = false;
            TypeAttributes attributes = 0;
            if (publicType.IsPublic)
                attributes |= TypeAttributes.Public;
            if (publicType.IsSealed && !publicType.IsAbstract)
                attributes |= TypeAttributes.Sealed;
            else if (!publicType.IsSealed && publicType.IsAbstract && !publicType.IsInterface)
                attributes |= TypeAttributes.Abstract;
            else if (publicType.IsSealed && publicType.IsAbstract)
                @static = true;

            // Static support is a hack. CodeDOM does support it, and this isn't
            // correct C#, but it's good enough for our API outline
            var name = publicType.Name;

            var index = name.IndexOf('`');
            if (index != -1)
                name = name.Substring(0, index);
            var declaration = new CodeTypeDeclaration(@static ? "static " + name : name)
            {
                CustomAttributes = CreateCustomAttributes(publicType),
                // TypeAttributes must be specified before the IsXXX as they manipulate TypeAttributes!
                TypeAttributes = attributes,
                IsClass = publicType.IsClass,
                IsEnum = publicType.IsEnum,
                IsInterface = publicType.IsInterface,
                IsStruct = publicType.IsValueType && !publicType.IsPrimitive && !publicType.IsEnum,
            };

            PopulateGenericParameters(publicType, declaration.TypeParameters);

            if (publicType.BaseType != null && ShouldOutputBaseType(publicType))
            {
                if (publicType.BaseType.FullName == "System.Enum")
                {
                    var underlyingType = publicType.GetEnumUnderlyingType();
                    if (underlyingType.FullName != "System.Int32")
                        declaration.BaseTypes.Add(CreateCodeTypeReference(underlyingType));
                }
                else
                    declaration.BaseTypes.Add(CreateCodeTypeReference(publicType.BaseType));
            }
            foreach (var @interface in publicType.Interfaces.OrderBy(i => i.FullName))
                declaration.BaseTypes.Add(CreateCodeTypeReference(@interface));

            foreach (var memberInfo in publicType.GetMembers().Where(ShouldIncludeMember).OrderBy(m => m.Name))
                AddMemberToTypeDeclaration(declaration, memberInfo);

            // Fields should be in defined order for an enum
            var fields = !publicType.IsEnum
                ? publicType.Fields.OrderBy(f => f.Name)
                : (IEnumerable<FieldDefinition>)publicType.Fields;
            foreach (var field in fields)
                AddMemberToTypeDeclaration(declaration, field);

            return declaration;
        }

        private static CodeTypeDeclaration CreateDelegateDeclaration(TypeDefinition publicType)
        {
            var invokeMethod = publicType.Methods.Single(m => m.Name == "Invoke");
            var declaration = new CodeTypeDelegate(publicType.Name)
            {
                Attributes = MemberAttributes.Public,
                CustomAttributes = CreateCustomAttributes(publicType),
                ReturnType = CreateCodeTypeReference(invokeMethod.ReturnType),
            };

            foreach (var parameter in invokeMethod.Parameters)
            {
                declaration.Parameters.Add(
                    new CodeParameterDeclarationExpression(CreateCodeTypeReference(parameter.ParameterType),
                        parameter.Name));
            }

            return declaration;
        }

        private static bool ShouldOutputBaseType(TypeDefinition publicType)
        {
            return publicType.BaseType.FullName != "System.Object" && publicType.BaseType.FullName != "System.ValueType";
        }

        private static void PopulateGenericParameters(IGenericParameterProvider publicType, CodeTypeParameterCollection parameters)
        {
            foreach (var parameter in publicType.GenericParameters)
            {
                var typeParameter = new CodeTypeParameter(parameter.Name)
                {
                    HasConstructorConstraint =
                        parameter.HasDefaultConstructorConstraint && !parameter.HasNotNullableValueTypeConstraint
                };
                if (parameter.HasNotNullableValueTypeConstraint)
                    typeParameter.Constraints.Add(" struct"); // Extra space is a hack!
                if (parameter.HasReferenceTypeConstraint)
                    typeParameter.Constraints.Add(" class");
                foreach (var constraint in parameter.Constraints.Where(t => t.FullName != "System.ValueType"))
                {
                    typeParameter.Constraints.Add(CreateCodeTypeReference(constraint.GetElementType()));
                }
                parameters.Add(typeParameter);
            }
        }

        private static CodeAttributeDeclarationCollection CreateCustomAttributes(ICustomAttributeProvider type)
        {
            var attributes = new CodeAttributeDeclarationCollection();
            PopulateCustomAttributes(type, attributes);
            return attributes;
        }

        private static void PopulateCustomAttributes(ICustomAttributeProvider type,
            CodeAttributeDeclarationCollection attributes)
        {
            foreach (var customAttribute in type.CustomAttributes.Where(ShouldIncludeAttribute).OrderBy(a => a.AttributeType.FullName))
            {
                var attribute = new CodeAttributeDeclaration(CreateCodeTypeReference(customAttribute.AttributeType));
                foreach (var arg in customAttribute.ConstructorArguments)
                    attribute.Arguments.Add(new CodeAttributeArgument(CreateInitialiserExpression(arg.Type, arg.Value)));
                foreach (var property in customAttribute.Properties.OrderBy(p => p.Name))
                {
                    attribute.Arguments.Add(new CodeAttributeArgument(property.Name,
                        CreateInitialiserExpression(property.Argument.Type, property.Argument.Value)));
                }
                attributes.Add(attribute);
            }
        }

        private static bool ShouldIncludeAttribute(CustomAttribute attribute)
        {
            return attribute.AttributeType.FullName != "System.Runtime.CompilerServices.AsyncStateMachineAttribute"
                && attribute.AttributeType.FullName != "System.Reflection.DefaultMemberAttribute";
        }

        private static CodeExpression CreateInitialiserExpression(TypeReference typeReference, object value)
        {
            var type = typeReference.Resolve();
            if (type.BaseType.FullName == "System.Enum"
                && type.CustomAttributes.Any(a => a.AttributeType.FullName == "System.FlagsAttribute"))
            {
                var originalValue = Convert.ToInt64(value);

                //var allFlags = from f in type.Fields
                //    where f.Constant != null
                //    let v = Convert.ToInt64(f.Constant)
                //    where v == 0 || (originalValue & v) != 0
                //    select (CodeExpression)new CodeFieldReferenceExpression(typeExpression, f.Name);
                //return allFlags.Aggregate((current, next) => new CodeBinaryOperatorExpression(current, CodeBinaryOperatorType.BitwiseOr, next));

                // I'd rather use the above, as it's just using the CodeDOM, but it puts
                // brackets around each CodeBinaryOperatorExpression
                var flags = from f in type.Fields
                    where f.Constant != null
                    let v = Convert.ToInt64(f.Constant)
                    where v == 0 || (originalValue & v) != 0
                    select typeReference.FullName + "." + f.Name;
                return new CodeSnippetExpression(flags.Aggregate((current, next) => current + " | " + next));
            }
            if (typeReference.FullName == "System.Type" && value is TypeReference)
            {
                return new CodeTypeOfExpression(CreateCodeTypeReference((TypeReference)value));
            }
            return new CodePrimitiveExpression(value);
        }

        private static void AddCtorToTypeDeclaration(CodeTypeDeclaration typeDeclaration, MethodDefinition member)
        {
            if (member.IsAssembly || member.IsPrivate || IsEmptyDefaultConstructor(member))
                return;

            var method = new CodeConstructor
            {
                CustomAttributes = CreateCustomAttributes(member),
                Name = member.Name,
                Attributes = GetMethodAttributes(member)
            };

            AddParametersToMethodDefinition(member, method);

            typeDeclaration.Members.Add(method);
        }

        private static bool IsEmptyDefaultConstructor(MethodDefinition member)
        {
            if (member.Parameters.Count == 0 && member.Body.Instructions.Count == 3 &&
                member.Body.Instructions[0].OpCode == OpCodes.Ldarg_0 &&
                member.Body.Instructions[1].OpCode == OpCodes.Call &&
                (member.Body.Instructions[1].Operand == null || 
                (member.Body.Instructions[1].Operand is MethodReference &&
                 ((MethodReference)member.Body.Instructions[1].Operand).Name == ".ctor")) &&
                member.Body.Instructions[2].OpCode == OpCodes.Ret)
            {
                return true;
            }
            return false;
        }

        private static void AddMethodToTypeDeclaration(CodeTypeDeclaration typeDeclaration, MethodDefinition member)
        {
            if (member.IsAssembly || member.IsPrivate || member.IsSpecialName)
                return;

            var returnType = CreateCodeTypeReference(member.ReturnType);
            if (IsAsync(member))
                returnType = MakeAsync(returnType);

            var method = new CodeMemberMethod
            {
                Name = member.Name,
                Attributes = GetMethodAttributes(member),
                CustomAttributes = CreateCustomAttributes(member),
                ReturnType = returnType,
            };
            PopulateCustomAttributes(member.MethodReturnType, method.ReturnTypeCustomAttributes);
            PopulateGenericParameters(member, method.TypeParameters);
            AddParametersToMethodDefinition(member, method);

            typeDeclaration.Members.Add(method);
        }

        private static bool IsAsync(ICustomAttributeProvider method)
        {
            return method.CustomAttributes.Any(a => a.AttributeType.FullName == "System.Runtime.CompilerServices.AsyncStateMachineAttribute");
        }

        private static void AddParametersToMethodDefinition(IMethodSignature member, CodeMemberMethod method)
        {
            foreach (var parameter in member.Parameters)
            {
                FieldDirection direction = 0;
                if (parameter.IsOut)
                    direction |= FieldDirection.Out;
                else if (parameter.ParameterType.IsByReference)
                    direction |= FieldDirection.Ref;

                var parameterType = parameter.ParameterType.IsByReference
                    ? parameter.ParameterType.GetElementType()
                    : parameter.ParameterType;

                var type = parameter.ParameterType.IsGenericParameter
                    ? new CodeTypeReference(parameter.ParameterType.Name)
                    : CreateCodeTypeReference(parameterType);

                var name = parameter.HasConstant
                    ? string.Format("{0} = {1}", parameter.Name, FormatParameterConstant(parameter))
                    : parameter.Name;
                var expresion = new CodeParameterDeclarationExpression(type, name)
                {
                    Direction = direction,
                    CustomAttributes = CreateCustomAttributes(parameter)
                };
                method.Parameters.Add(expresion);
            }
        }

        private static object FormatParameterConstant(IConstantProvider parameter)
        {
            return parameter.Constant is string ? string.Format("\"{0}\"", parameter.Constant) : (parameter.Constant ?? "null");
        }

        static MemberAttributes GetMethodAttributes(MethodDefinition method)
        {
            MemberAttributes attributes = 0;

            if (method.IsFamily)
                attributes |= MemberAttributes.Family;
            if (method.IsPublic)
                attributes |= MemberAttributes.Public;
            if (method.IsStatic)
                attributes |= MemberAttributes.Static;

            if (method.IsFinal || !method.IsVirtual)
                attributes |= MemberAttributes.Final;
            if (method.IsAbstract)
                attributes |= MemberAttributes.Abstract;
            if (method.IsVirtual && !method.IsNewSlot)
                attributes |= MemberAttributes.Override;

            if (IsHidingMethod(method))
                attributes |= MemberAttributes.New;

            return attributes;
        }

        private static bool IsHidingMethod(MethodDefinition method)
        {
            var typeDefinition = method.DeclaringType;

            // If we're an interface, just try and find any method with the same signature
            // in any of the interfaces that we implement
            if (typeDefinition.IsInterface)
            {
                var interfaceMethods = from @interfaceReference in typeDefinition.Interfaces
                    let interfaceDefinition = @interfaceReference.Resolve()
                    where interfaceDefinition != null
                    select interfaceDefinition.Methods;

                return interfaceMethods.Any(ms => MetadataResolver.GetMethod(ms, method) != null);
            }

            // If we're not an interface, find a base method that isn't virtual
            return !method.IsVirtual && GetBaseTypes(typeDefinition).Any(d => MetadataResolver.GetMethod(d.Methods, method) != null);
        }

        private static IEnumerable<TypeDefinition> GetBaseTypes(TypeDefinition type)
        {
            var baseType = type.BaseType;
            while (baseType != null)
            {
                var definition = baseType.Resolve();
                if (definition != null)
                {
                    yield return definition;

                    baseType = baseType.DeclaringType;
                }
            }
        }

        private static void AddPropertyToTypeDeclaration(CodeTypeDeclaration typeDeclaration, PropertyDefinition member)
        {
            var getterAttributes = member.GetMethod != null ? GetMethodAttributes(member.GetMethod) : 0;
            var setterAttributes = member.SetMethod != null ? GetMethodAttributes(member.SetMethod) : 0;

            var propertyAttributes = getterAttributes | setterAttributes;

            var property = new CodeMemberProperty
            {
                Name = member.Name,
                Type = CreateCodeTypeReference(member.PropertyType),
                Attributes = propertyAttributes,
                HasGet = member.GetMethod != null,
                HasSet = member.SetMethod != null
            };

            foreach (var parameter in member.Parameters)
            {
                property.Parameters.Add(
                    new CodeParameterDeclarationExpression(CreateCodeTypeReference(parameter.ParameterType),
                        parameter.Name));
            }

            typeDeclaration.Members.Add(property);
        }

        static CodeTypeMember GenerateEvent(EventDefinition eventDefinition)
        {
            var @event = new CodeMemberEvent
            {
                Name = eventDefinition.Name,
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Type = CreateCodeTypeReference(eventDefinition.EventType)
            };

            return @event;
        }

        static void AddFieldToTypeDeclaration(CodeTypeDeclaration typeDeclaration, FieldDefinition memberInfo)
        {
            if (memberInfo.IsPrivate || memberInfo.IsAssembly || memberInfo.IsSpecialName)
                return;

            MemberAttributes attributes = 0;
            if (memberInfo.HasConstant)
                attributes |= MemberAttributes.Const;
            if (memberInfo.IsFamily)
                attributes |= MemberAttributes.Family;
            if (memberInfo.IsPublic)
                attributes |= MemberAttributes.Public;
            if (memberInfo.IsStatic)
                attributes |= MemberAttributes.Static;

            // TODO: Costant value
            var codeTypeReference = CreateCodeTypeReference(memberInfo.FieldType);
            if (memberInfo.IsInitOnly)
                codeTypeReference = MakeReadonly(codeTypeReference);
            var field = new CodeMemberField(codeTypeReference, memberInfo.Name)
            {
                Attributes = attributes,
                CustomAttributes = CreateCustomAttributes(memberInfo)
            };

            if (memberInfo.HasConstant)
                field.InitExpression = new CodePrimitiveExpression(memberInfo.Constant);

            typeDeclaration.Members.Add(field);
        }

        private static CodeTypeReference MakeReadonly(CodeTypeReference typeReference)
        {
            return ModifyCodeTypeReference(typeReference, "readonly");
        }

        private static CodeTypeReference MakeAsync(CodeTypeReference typeReference)
        {
            return ModifyCodeTypeReference(typeReference, "async");   
        }

        private static CodeTypeReference ModifyCodeTypeReference(CodeTypeReference typeReference, string modifier)
        {
            using (var provider = new CSharpCodeProvider())
                return new CodeTypeReference(modifier + " " + provider.GetTypeOutput(typeReference));
        }

        private static CodeTypeReference CreateCodeTypeReference(TypeReference type)
        {
            var typeName = GetTypeName(type);
            return new CodeTypeReference(typeName, CreateGenericArguments(type));
        }

        private static string GetTypeName(TypeReference type)
        {
            if (!type.IsNested)
            {
                return (!string.IsNullOrEmpty(type.Namespace) ? (type.Namespace + ".") : "") + type.Name;
            }

            return GetTypeName(type.DeclaringType) + "." + type.Name;
        }

        private static CodeTypeReference[] CreateGenericArguments(TypeReference type)
        {
            var genericInstance = type as IGenericInstance;
            if (genericInstance == null) return null;

            var genericArguments = new List<CodeTypeReference>();
            foreach (var argument in genericInstance.GenericArguments)
            {
                genericArguments.Add(CreateCodeTypeReference(argument));
            }
            return genericArguments.ToArray();
        }
    }
}
// ReSharper restore BitwiseOperatorOnEnumWihtoutFlags
// ReSharper restore CheckNamespace
