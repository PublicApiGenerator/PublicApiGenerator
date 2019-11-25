using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
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
                e.Parameters.Count == methodDefinition.Parameters.Count &&
                e.Parameters.SequenceEqual(methodDefinition.Parameters, new ParameterTypeComparer()));

            return ModifierMarkerNameBuilder.Build(methodDefinition, attributes, isNew, name,
                CodeNormalizer.MethodModifierMarkerTemplate);
        }

        class ParameterTypeComparer : IEqualityComparer<ParameterDefinition>
        {
            public bool Equals(ParameterDefinition x, ParameterDefinition y)
            {
                return x?.ParameterType == y?.ParameterType;
            }

            public int GetHashCode(ParameterDefinition obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
