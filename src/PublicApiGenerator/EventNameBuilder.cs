using System.CodeDom;
using Mono.Cecil;

namespace PublicApiGenerator
{
    public static class EventNameBuilder
    {
        public static string AugmentEventNameWithEventModifierMarkerTemplate(EventDefinition eventDefinition, string name,
            MemberAttributes addAccessorAttributes, MemberAttributes removeAccessorAttributes)
        {
            var addVTableAttributes = (addAccessorAttributes & MemberAttributes.VTableMask);
            var removeVTableAttributes = (removeAccessorAttributes & MemberAttributes.VTableMask);

            if (eventDefinition.DeclaringType.IsInterface)
            {
                if (addVTableAttributes == MemberAttributes.New && removeVTableAttributes == MemberAttributes.New)
                {
                    return $"{string.Format(CodeNormalizer.EventModifierMarkerTemplate, $"new{CodeNormalizer.EventRemovePublicMarker}")}{name}";
                }

                return $"{string.Format(CodeNormalizer.EventModifierMarkerTemplate, CodeNormalizer.EventRemovePublicMarker)}{name}";;
            }

            var addScopeAttributes = (addAccessorAttributes & MemberAttributes.ScopeMask);
            var removeScopeAttributes = (removeAccessorAttributes & MemberAttributes.ScopeMask);

            if (addScopeAttributes == MemberAttributes.Static && removeScopeAttributes == MemberAttributes.Static)
            {
                return $"{string.Format(CodeNormalizer.EventModifierMarkerTemplate, "static")}{name}";
            }


            if (addScopeAttributes == MemberAttributes.Override && removeScopeAttributes == MemberAttributes.Override)
            {
                return $"{string.Format(CodeNormalizer.EventModifierMarkerTemplate, "override")}{name}";
            }

            if (addScopeAttributes == (MemberAttributes.Final | MemberAttributes.Override) && removeScopeAttributes == (MemberAttributes.Final | MemberAttributes.Override))
            {
                return $"{string.Format(CodeNormalizer.EventModifierMarkerTemplate, "sealed override")}{name}";
            }

            if (addScopeAttributes == MemberAttributes.Abstract && removeScopeAttributes == MemberAttributes.Abstract)
            {
                return $"{string.Format(CodeNormalizer.EventModifierMarkerTemplate, "abstract")}{name}";
            }

            if (addVTableAttributes == MemberAttributes.New && removeVTableAttributes == MemberAttributes.New)
            {
                return $"{string.Format(CodeNormalizer.EventModifierMarkerTemplate, "new")}{name}";
            }

            if (eventDefinition.AddMethod.IsVirtual && eventDefinition.RemoveMethod.IsVirtual &&
                !eventDefinition.AddMethod.IsAbstract && !eventDefinition.RemoveMethod.IsAbstract)
            {
                return $"{string.Format(CodeNormalizer.EventModifierMarkerTemplate, "virtual")}{name}";
            }

            return name;
        }
    }
}
