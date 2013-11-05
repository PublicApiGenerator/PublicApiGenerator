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


    public class PublicApiGenerator
    {
        // TODO: Assembly attributes
        // TODO: Assembly references?
        // TODO: Better handle namespaces - using statements?
        // TODO: Constant values for fields
        // TODO: Default values for parameters
        // TODO: Constructor parameters for attributes
        // TODO: Generic methods
        // TODO: Ref parameters?
        public static string CreatePublicApiForAssembly(AssemblyDefinition assembly)
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
                    .Where(ShouldIncludeType)
                    .OrderBy(t => t.FullName);
                foreach (var publicType in publicTypes)
                {
                    var writer = new StringWriter();
                    var @namespace = new CodeNamespace(publicType.Namespace);
                    var genClass = CreateClassDeclaration(publicType);
                    foreach (var memberInfo in publicType.GetMembers().Where(ShouldIncludeMember).OrderBy(m => m.Name))
                    {
                        AddMemberToClassDefinition(genClass, memberInfo);
                    }
                    @namespace.Types.Add(genClass);
                    provider.GenerateCodeFromNamespace(@namespace, writer, cgo);
                    var gennedClass = GenerateClassCode(writer);
                    publicApiBuilder.AppendLine(gennedClass);
                }
            }
            var publicApi = publicApiBuilder.ToString();
            return publicApi.Trim();
        }

        private static bool ShouldIncludeType(TypeDefinition t)
        {
            return t.IsPublic && !IsCompilerGenerated(t);
        }

        private static bool ShouldIncludeMember(IMemberDefinition m)
        {
            return !IsCompilerGenerated(m) && !IsDotNetTypeMember(m);
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

        static void AddMemberToClassDefinition(CodeTypeDeclaration genClass, IMemberDefinition memberInfo)
        {
            var methodDefinition = memberInfo as MethodDefinition;
            if (methodDefinition != null)
            {
                if (methodDefinition.IsConstructor)
                    AddCtorToClassDefinition(genClass, methodDefinition);
                else
                    AddMethodToClassDefinition(genClass, methodDefinition);
            }
            else if (memberInfo is PropertyDefinition)
            {
                genClass.Members.Add(GenerateProperty((PropertyDefinition)memberInfo));
            }
            else if (memberInfo is EventDefinition)
            {
                genClass.Members.Add(GenerateEvent((EventDefinition)memberInfo));
            }
            else if (memberInfo is FieldDefinition)
            {
                AddFieldToClassDefinition(genClass, (FieldDefinition) memberInfo);
            }
        }

        static string GenerateClassCode(StringWriter writer)
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
            return gennedClass;
        }

        static CodeTypeDeclaration CreateClassDeclaration(TypeDefinition publicType)
        {
            bool @static = false;
            System.Reflection.TypeAttributes attributes = 0;
            if (publicType.IsPublic)
                attributes |= System.Reflection.TypeAttributes.Public;
            if (publicType.IsSealed && !publicType.IsAbstract)
                attributes |= System.Reflection.TypeAttributes.Sealed;
            else if (!publicType.IsSealed && publicType.IsAbstract)
                attributes |= System.Reflection.TypeAttributes.Abstract;
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
                IsClass = publicType.IsClass,
                IsEnum = publicType.IsEnum,
                IsInterface = publicType.IsInterface,
                IsStruct = publicType.IsValueType && !publicType.IsPrimitive && !publicType.IsEnum,
                TypeAttributes = attributes
            };

            PopulateGenericParameters(publicType, declaration.TypeParameters);

            if (publicType.BaseType != null && publicType.BaseType.FullName != "System.Object")
                declaration.BaseTypes.Add(CreateCodeTypeReference(publicType.BaseType));
            foreach (var @interface in publicType.Interfaces.OrderBy(i => i.FullName))
                declaration.BaseTypes.Add(CreateCodeTypeReference(@interface));

            return declaration;
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
            foreach (var customAttribute in type.CustomAttributes)
            {
                // TODO: Attribute parameters
                var attribute = new CodeAttributeDeclaration(CreateCodeTypeReference(customAttribute.AttributeType));
                attributes.Add(attribute);
            }
        }

        private static void AddCtorToClassDefinition(CodeTypeDeclaration genClass, MethodDefinition member)
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

            genClass.Members.Add(method);
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

        private static void AddMethodToClassDefinition(CodeTypeDeclaration genClass, MethodDefinition member)
        {
            if (member.IsAssembly || member.IsPrivate)
                return;

            // TODO: Type parameters
            var method = new CodeMemberMethod
            {
                Name = member.Name,
                Attributes = GetMethodAttributes(member),
                CustomAttributes = CreateCustomAttributes(member),
                ReturnType = CreateCodeTypeReference(member.ReturnType),
            };
            PopulateCustomAttributes(member.MethodReturnType, method.ReturnTypeCustomAttributes);
            PopulateGenericParameters(member, method.TypeParameters);
            AddParametersToMethodDefinition(member, method);

            genClass.Members.Add(method);
        }

        private static void AddParametersToMethodDefinition(IMethodSignature member, CodeMemberMethod method)
        {
            var parameterCollection = new CodeParameterDeclarationExpressionCollection();
            foreach (var parameter in member.Parameters)
            {
                FieldDirection direction = 0;
                if (parameter.IsOut)
                    direction |= FieldDirection.Out;
                CodeTypeReference codeTypeReference = CreateCodeTypeReference(parameter.ParameterType);
                if (!parameter.ParameterType.IsGenericInstance)
                    codeTypeReference = new CodeTypeReference(parameter.ParameterType.Name);
                var expresion = new CodeParameterDeclarationExpression(codeTypeReference,
                    parameter.Name)
                {
                    Direction = direction,
                    CustomAttributes = CreateCustomAttributes(parameter)
                };
                parameterCollection.Add(expresion);
            }
            method.Parameters.AddRange(parameterCollection);
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

            if (method.IsAbstract)
                attributes |= MemberAttributes.Abstract;

            // I think this is right. Fingers crossed!
            if (method.IsHideBySig)
            {
                if (method.IsFinal || (!method.IsNewSlot && !method.IsVirtual))
                {
                    attributes |= MemberAttributes.Final;
                }
                else if (method.IsVirtual && !method.IsNewSlot)
                {
                    attributes |= MemberAttributes.Override;
                }
                else if (!method.IsVirtual)
                {
                    attributes |= MemberAttributes.New;
                }
            }

            return attributes;
        }

        private static CodeMemberProperty GenerateProperty(PropertyDefinition member)
        {
            var property = new CodeMemberProperty
            {
                Name = member.Name,
                Type = CreateCodeTypeReference(member.PropertyType),
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                HasGet = member.GetMethod != null,
                HasSet = member.SetMethod != null
            };

            return property;
        }

        static CodeTypeMember GenerateEvent(EventDefinition memberInfo)
        {
            var @event = new CodeMemberEvent
            {
                Name = memberInfo.Name,
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                Type = CreateCodeTypeReference(memberInfo.EventType)
            };

            return @event;
        }

        static void AddFieldToClassDefinition(CodeTypeDeclaration classDefinition, FieldDefinition memberInfo)
        {
            if (memberInfo.IsPrivate || memberInfo.IsAssembly)
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
            {
                using (var provider = new CSharpCodeProvider())
                {
                    codeTypeReference = new CodeTypeReference("readonly " + provider.GetTypeOutput(codeTypeReference));
                }
            }
            var field = new CodeMemberField(codeTypeReference, memberInfo.Name)
            {
                Attributes = attributes,
                CustomAttributes = CreateCustomAttributes(memberInfo)
            };

            classDefinition.Members.Add(field);
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
