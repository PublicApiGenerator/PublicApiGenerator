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
        // TODO: Interfaces + attributes. What about generics?
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
                    .Where(t => t.IsPublic && t.Name != "GeneratedInternalTypeHelper") //GeneratedInternalTypeHelper seems to be a r# runner side effect
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

        private static bool ShouldIncludeMember(IMemberDefinition m)
        {
            return !m.IsSpecialName && !m.IsRuntimeSpecialName && !IsDotNetTypeMember(m);
        }

        private static bool IsDotNetTypeMember(IMemberDefinition m)
        {
            if (m.DeclaringType == null || m.DeclaringType.FullName == null)
                return false;
            return m.DeclaringType.FullName.StartsWith("System") || m.DeclaringType.FullName.StartsWith("Microsoft");
        }

        static void AddMemberToClassDefinition(CodeTypeDeclaration genClass, IMemberDefinition memberInfo)
        {
            if (memberInfo is MethodDefinition)
            {
                var method = (MethodDefinition)memberInfo;
                if (method.IsSpecialName) return;
                if (method.IsConstructor)
                    genClass.Members.Add(GenerateCtor((MethodDefinition)memberInfo));
                else
                    genClass.Members.Add(GenerateMethod((MethodDefinition)memberInfo));
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
            var declaration = new CodeTypeDeclaration(publicType.Name)
            {
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
            foreach (var customAttribute in type.CustomAttributes)
            {
                // TODO: Attribute parameters
                var attribute = new CodeAttributeDeclaration(CreateCodeTypeReference(customAttribute.AttributeType));
                attributes.Add(attribute);
            }
            return attributes;
        }

        // ReSharper disable BitwiseOperatorOnEnumWihtoutFlags
        public static CodeConstructor GenerateCtor(MethodDefinition member)
        {
            var method = new CodeConstructor
            {
                Name = member.Name,
                Attributes = MemberAttributes.Public | MemberAttributes.Final
            };

            foreach (var parameterInfo in member.Parameters)
            {
                method.Parameters.Add(new CodeParameterDeclarationExpression(CreateCodeTypeReference(parameterInfo.ParameterType),
                                                                             parameterInfo.Name));
            }
            return method;
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

        public static CodeMemberMethod GenerateMethod(MethodDefinition member)
        {
            var method = new CodeMemberMethod
            {
                Name = member.Name,
                Attributes = MemberAttributes.Public | MemberAttributes.Final
                // ReSharper restore BitwiseOperatorOnEnumWithoutFlags
            };
            var methodTypeRef = CreateCodeTypeReference(member.ReturnType);
            method.ReturnType = methodTypeRef;

            var methodParameters = member.Parameters.ToList();
            var parameterCollection = new CodeParameterDeclarationExpressionCollection();
            foreach (var info in methodParameters)
            {
                var expresion = new CodeParameterDeclarationExpression(CreateCodeTypeReference(info.ParameterType), info.Name);
                parameterCollection.Add(expresion);
            }
            method.Parameters.AddRange(parameterCollection);
            return method;
        }

        private static CodeTypeReference CreateCodeTypeReference(TypeReference type)
        {
            return new CodeTypeReference(type.Namespace + "." + type.Name, CreateGenericArguments(type));
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

        public static CodeMemberProperty GenerateProperty(PropertyDefinition member)
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
    }
}
// ReSharper restore BitwiseOperatorOnEnumWithoutFlags
// ReSharper restore BitwiseOperatorOnEnumWihtoutFlags
// ReSharper restore CheckNamespace
