using System;
using System.CodeDom;
using Mono.Cecil;

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

            var isNew = methodDefinition.IsNew(typeDef => typeDef?.Methods, e =>
                e.Name.Equals(methodDefinition.Name, StringComparison.Ordinal) &&
                e.Parameters.Count == methodDefinition.Parameters.Count);

            return ModifierMarkerNameBuilder.Build(methodDefinition, attributes, isNew, name,
                CodeNormalizer.MethodModifierMarkerTemplate);
        }
    }
}
