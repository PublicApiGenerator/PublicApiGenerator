using System;
using System.CodeDom;
using System.Linq;
using Mono.Cecil;
using static System.String;

namespace PublicApiGenerator
{
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
                    ? Format(CodeNormalizer.EventModifierMarkerTemplate, $"new{CodeNormalizer.EventRemovePublicMarker}") + name
                    : Format(CodeNormalizer.EventModifierMarkerTemplate, CodeNormalizer.EventRemovePublicMarker) + name;
            }

            bool? isNew = null;
            TypeReference? baseType = eventDefinition.DeclaringType.BaseType;
            while (baseType is TypeDefinition typeDef)
            {
                isNew = typeDef?.Events.Any(e => e.Name.Equals(eventDefinition.Name, StringComparison.Ordinal));
                if (isNew is true)
                {
                    break;
                }
                baseType = typeDef?.BaseType;
            }

            return ModifierMarkerNameBuilder.Build(eventDefinition.AddMethod, addAccessorAttributes, isNew, name,
                CodeNormalizer.EventModifierMarkerTemplate);
        }
    }
}
