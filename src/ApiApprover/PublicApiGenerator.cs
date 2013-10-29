using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using Mono.Cecil;

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
            MemberAttributes attributes = 0;
            if (publicType.IsAbstract)
                attributes |= MemberAttributes.Abstract;
            if (publicType.IsPublic)
                attributes |= MemberAttributes.Public;
            if (publicType.IsSealed)
                attributes |= MemberAttributes.Static;

            var declaration = new CodeTypeDeclaration(publicType.Name)
            {
                Attributes = attributes,
                CustomAttributes = CreateCustomAttributes(publicType),
                IsClass = publicType.IsClass,
                IsEnum = publicType.IsEnum,
                IsInterface = publicType.IsInterface,
                IsStruct = publicType.IsValueType && !publicType.IsPrimitive && !publicType.IsEnum
            };

            if (publicType.BaseType != null && publicType.BaseType.FullName != "System.Object")
                declaration.BaseTypes.Add(CreateCodeTypeReference(publicType.BaseType));
            foreach (var @interface in publicType.Interfaces)
                declaration.BaseTypes.Add(CreateCodeTypeReference(@interface));

            return declaration;
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
            if (member.IsAssembly || member.IsPrivate)
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
                var expresion = new CodeParameterDeclarationExpression(CreateCodeTypeReference(parameter.ParameterType),
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
            if (memberInfo.IsStatic)
                attributes |= MemberAttributes.Static;
            if (memberInfo.HasConstant)
                attributes |= MemberAttributes.Const;
            if (memberInfo.IsFamily)
                attributes |= MemberAttributes.Family;

            // TODO: Costant value
            var field = new CodeMemberField(CreateCodeTypeReference(memberInfo.FieldType), memberInfo.Name)
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
