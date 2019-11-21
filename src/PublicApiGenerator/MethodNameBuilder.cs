using System;
using System.CodeDom;
using System.Linq;
using Mono.Cecil;
using static System.String;

namespace PublicApiGenerator
{
    public static class MethodNameBuilder
    {
        public static string AugmentMethodNameWithMethodModifierMarkerTemplate(MethodDefinition methodDefinition,
            MemberAttributes attributes)
        {
            var name = CSharpOperatorKeyword.Get(methodDefinition.Name);

            if (methodDefinition.DeclaringType.IsInterface)
            {
                return name;
            }

            bool? isNew = null;
            TypeReference? baseType = methodDefinition.DeclaringType.BaseType;
            while (baseType is TypeDefinition typeDef)
            {
                // too simple, improve
                isNew = typeDef?.Methods.Any(e => e.Name.Equals(methodDefinition.Name, StringComparison.Ordinal) && e.Parameters.Count == methodDefinition.Parameters.Count);
                if (isNew is true)
                {
                    break;
                }
                baseType = typeDef?.BaseType;
            }

            return ModifierMarkerNameBuilder.Build(methodDefinition, attributes, isNew, name,
                CodeNormalizer.MethodModifierMarkerTemplate);
        }
    }
}
