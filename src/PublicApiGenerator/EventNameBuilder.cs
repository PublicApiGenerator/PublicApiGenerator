using System;
using System.CodeDom;
using Mono.Cecil;

namespace PublicApiGenerator
{
    public static class EventNameBuilder
    {
        public static string AugmentEventNameWithEventModifierMarkerTemplate(EventDefinition eventDefinition, string name,
            MemberAttributes addAccessorAttributes, MemberAttributes removeAccessorAttributes)
        {
            if (eventDefinition.AddMethod.IsStatic && eventDefinition.RemoveMethod.IsStatic)
            {
                return $"{String.Format(CodeNormalizer.EventModifierMarkerTemplate, "static")}{name}";
            }

            var addScopeAttributes = (addAccessorAttributes & MemberAttributes.ScopeMask);
            var removeScopeAttributes = (removeAccessorAttributes & MemberAttributes.ScopeMask);
            if (eventDefinition.AddMethod.IsVirtual && eventDefinition.RemoveMethod.IsVirtual &&
                addScopeAttributes == MemberAttributes.Override && removeScopeAttributes == MemberAttributes.Override)
            {
                return $"{String.Format(CodeNormalizer.EventModifierMarkerTemplate, "override")}{name}";
            }

            if (eventDefinition.AddMethod.IsVirtual && eventDefinition.RemoveMethod.IsVirtual &&
                !eventDefinition.AddMethod.IsAbstract && !eventDefinition.RemoveMethod.IsAbstract)
            {
                return $"{String.Format(CodeNormalizer.EventModifierMarkerTemplate, "virtual")}{name}";
            }

            if (eventDefinition.AddMethod.IsAbstract && eventDefinition.RemoveMethod.IsAbstract &&
                eventDefinition.AddMethod.IsVirtual && eventDefinition.RemoveMethod.IsVirtual)
            {
                return $"{String.Format(CodeNormalizer.EventModifierMarkerTemplate, "abstract")}{name}";
            }

            if (eventDefinition.AddMethod.IsFinal && eventDefinition.RemoveMethod.IsFinal)
            {
                return $"{String.Format(CodeNormalizer.EventModifierMarkerTemplate, "sealed")}{name}";
            }

            return name;
        }
    }
}
