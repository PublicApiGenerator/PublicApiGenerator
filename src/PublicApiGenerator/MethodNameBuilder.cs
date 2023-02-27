using System.CodeDom;
using Mono.Cecil;

namespace PublicApiGenerator;

internal static class MethodNameBuilder
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
            CodeNormalizer.METHOD_MODIFIER_MARKER_TEMPLATE);
    }

    private sealed class ParameterTypeComparer : IEqualityComparer<ParameterDefinition>
    {
        public bool Equals(ParameterDefinition x, ParameterDefinition y) => x?.ParameterType == y?.ParameterType;

        public int GetHashCode(ParameterDefinition obj) => obj.GetHashCode();
    }
}
