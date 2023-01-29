using System.CodeDom;
using Mono.Cecil;

namespace PublicApiGenerator;

public static class PropertyNameBuilder
{
    public static string AugmentPropertyNameWithPropertyModifierMarkerTemplate(PropertyDefinition propertyDefinition,
        MemberAttributes getAccessorAttributes, MemberAttributes setAccessorAttributes)
    {
        string name = propertyDefinition.Name;
        if (getAccessorAttributes != setAccessorAttributes || propertyDefinition.DeclaringType.IsInterface || propertyDefinition.HasParameters)
        {
            return name;
        }

        var isNew = propertyDefinition.IsNew(typeDef => typeDef?.Properties, e =>
            e.Name.Equals(propertyDefinition.Name, StringComparison.Ordinal));

        return ModifierMarkerNameBuilder.Build(propertyDefinition.GetMethod, getAccessorAttributes, isNew, name,
            CodeNormalizer.PROPERTY_MODIFIER_MARKER_TEMPLATE);
    }
}
