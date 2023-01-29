using System.CodeDom;
using Mono.Cecil;
using static System.String;

namespace PublicApiGenerator;

public static class EventNameBuilder
{
    public static string AugmentEventNameWithEventModifierMarkerTemplate(EventDefinition eventDefinition,
        MemberAttributes addAccessorAttributes, MemberAttributes removeAccessorAttributes)
    {
        string name = eventDefinition.Name;
        if (addAccessorAttributes != removeAccessorAttributes)
        {
            return name;
        }

        if (eventDefinition.DeclaringType.IsInterface)
        {
            return (addAccessorAttributes & MemberAttributes.VTableMask) == MemberAttributes.New
                ? Format(CodeNormalizer.EVENT_MODIFIER_MARKER_TEMPLATE, $"new{CodeNormalizer.EVENT_REMOVE_PUBLIC_MARKER}") + name
                : Format(CodeNormalizer.EVENT_MODIFIER_MARKER_TEMPLATE, CodeNormalizer.EVENT_REMOVE_PUBLIC_MARKER) + name;
        }

        var isNew = eventDefinition.IsNew(typeDef => typeDef?.Events, e =>
            e.Name.Equals(eventDefinition.Name, StringComparison.Ordinal));

        return ModifierMarkerNameBuilder.Build(eventDefinition.AddMethod, addAccessorAttributes, isNew, name,
            CodeNormalizer.EVENT_MODIFIER_MARKER_TEMPLATE);
    }
}
