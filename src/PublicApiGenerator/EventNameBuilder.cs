using System;
using System.CodeDom;
using System.Linq;
using Mono.Cecil;

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
                    ? string.Format(CodeNormalizer.EventModifierMarkerTemplate, $"new{CodeNormalizer.EventRemovePublicMarker}") + name
                    : string.Format(CodeNormalizer.EventModifierMarkerTemplate, CodeNormalizer.EventRemovePublicMarker) + name;
            }

            bool? isNew = null;
            var baseType = eventDefinition.DeclaringType.BaseType;
            while (baseType != null)
            {
                var typeDef = baseType as TypeDefinition;
                isNew = typeDef?.Methods.Any(e => e.Name.Equals(eventDefinition.AddMethod.Name, StringComparison.Ordinal));
                if (isNew.HasValue && isNew.Value)
                {
                    break;
                }
                baseType = typeDef?.BaseType;
            }

            var addScopeAttributes = addAccessorAttributes & MemberAttributes.ScopeMask;
            switch (addScopeAttributes)
            {
                case MemberAttributes.Static when addScopeAttributes == MemberAttributes.Static && !isNew.HasValue:
                    return string.Format(CodeNormalizer.EventModifierMarkerTemplate, "static")+ name;
                case MemberAttributes.Static when addScopeAttributes == MemberAttributes.Static && isNew.HasValue && isNew.Value:
                    return string.Format(CodeNormalizer.EventModifierMarkerTemplate, "static new")+ name;
                case MemberAttributes.Override when addScopeAttributes == MemberAttributes.Override:
                    return string.Format(CodeNormalizer.EventModifierMarkerTemplate, "override")+ name;
                case MemberAttributes.Final | MemberAttributes.Override when addScopeAttributes == (MemberAttributes.Final | MemberAttributes.Override):
                    return string.Format(CodeNormalizer.EventModifierMarkerTemplate, "sealed override")+ name;
                case MemberAttributes.Final when addScopeAttributes == MemberAttributes.Final && isNew.HasValue && isNew.Value:
                    return string.Format(CodeNormalizer.EventModifierMarkerTemplate, "new")+ name;
                case MemberAttributes.Abstract when addScopeAttributes == MemberAttributes.Abstract && !isNew.HasValue:
                    return string.Format(CodeNormalizer.EventModifierMarkerTemplate, "abstract")+ name;
                case MemberAttributes.Abstract when addScopeAttributes == MemberAttributes.Abstract && isNew.HasValue && isNew.Value:
                    return string.Format(CodeNormalizer.EventModifierMarkerTemplate, "new abstract")+ name;
            }

            if (eventDefinition.AddMethod.IsVirtual && !eventDefinition.AddMethod.IsAbstract)
            {
                return string.Format(CodeNormalizer.EventModifierMarkerTemplate, "virtual")+ name;
            }

            return name;
        }
    }
}
