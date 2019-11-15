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
            var baseType = eventDefinition.DeclaringType.BaseType;
            while (baseType != null)
            {
                var typeDef = baseType as TypeDefinition;
                isNew = typeDef?.Methods.Any(e => e.Name.Equals(eventDefinition.AddMethod.Name, StringComparison.Ordinal));
                if (isNew is true)
                {
                    break;
                }
                baseType = typeDef?.BaseType;
            }

            return (addAccessorAttributes & MemberAttributes.ScopeMask, isNew, eventDefinition.AddMethod.IsVirtual,
                    eventDefinition.AddMethod.IsAbstract) switch
                {
                    (MemberAttributes.Static, null, _, _) => Format(CodeNormalizer.EventModifierMarkerTemplate, "static") + name,
                    (MemberAttributes.Static, true, _, _) => Format(CodeNormalizer.EventModifierMarkerTemplate, "static new") + name,
                    (MemberAttributes.Override, _, _, _) => Format(CodeNormalizer.EventModifierMarkerTemplate, "override") + name,
                    (MemberAttributes.Final | MemberAttributes.Override, _, _, _) => Format(CodeNormalizer.EventModifierMarkerTemplate,"sealed override") + name,
                    (MemberAttributes.Final, true, _, _) => Format(CodeNormalizer.EventModifierMarkerTemplate, "new") + name,
                    (MemberAttributes.Abstract, null, _, _) => Format(CodeNormalizer.EventModifierMarkerTemplate, "abstract") + name,
                    (MemberAttributes.Abstract, true, _, _) => Format(CodeNormalizer.EventModifierMarkerTemplate, "new abstract") + name,
                    (MemberAttributes.Const, _, _, _) => Format(CodeNormalizer.EventModifierMarkerTemplate, "abstract override") + name,
                    (MemberAttributes.Final, null, true, false) => name,
                    (_, null, true, false) => Format(CodeNormalizer.EventModifierMarkerTemplate, "virtual") + name,
                    (_, true, true, false) => Format(CodeNormalizer.EventModifierMarkerTemplate, "new virtual") + name,
                    _ => name
                };
        }
    }
}
