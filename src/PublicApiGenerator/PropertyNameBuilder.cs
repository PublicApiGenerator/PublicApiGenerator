using System;
using System.CodeDom;
using System.Linq;
using Mono.Cecil;

namespace PublicApiGenerator
{
    public static class PropertyNameBuilder
    {
        public static string AugmentPropertyNameWithPropertyModifierMarkerTemplate(PropertyDefinition propertyDefinition,
            MemberAttributes getAccessorAttributes, MemberAttributes setAccessorAttributes)
        {
            string name = propertyDefinition.Name;
            if (getAccessorAttributes != setAccessorAttributes || propertyDefinition.DeclaringType.IsInterface)
            {
                return name;
            }

            bool? isNew = null;
            TypeReference? baseType = propertyDefinition.DeclaringType.BaseType;
            while (baseType is TypeDefinition typeDef)
            {
                isNew = typeDef?.Properties.Any(e => e.Name.Equals(propertyDefinition.Name, StringComparison.Ordinal) && e.PropertyType.Equals(propertyDefinition.PropertyType));
                if (isNew is true)
                {
                    break;
                }
                baseType = typeDef?.BaseType;
            }

            return (getAccessorAttributes & MemberAttributes.ScopeMask, isNew, propertyDefinition.GetMethod.IsVirtual,
                    propertyDefinition.GetMethod.IsAbstract) switch
                {
                    (MemberAttributes.Static, null, _, _) => String.Format(CodeNormalizer.PropertyModifierMarkerTemplate, "static") + name,
                    (MemberAttributes.Static, true, _, _) => String.Format(CodeNormalizer.PropertyModifierMarkerTemplate, "new static") + name,
                    (MemberAttributes.Override, _, _, _) => String.Format(CodeNormalizer.PropertyModifierMarkerTemplate, "override") + name,
                    (MemberAttributes.Final | MemberAttributes.Override, _, _, _) => String.Format(CodeNormalizer.PropertyModifierMarkerTemplate,"override sealed") + name,
                    (MemberAttributes.Final, true, _, _) => String.Format(CodeNormalizer.PropertyModifierMarkerTemplate, "new") + name,
                    (MemberAttributes.Abstract, null, _, _) => String.Format(CodeNormalizer.PropertyModifierMarkerTemplate, "abstract") + name,
                    (MemberAttributes.Abstract, true, _, _) => String.Format(CodeNormalizer.PropertyModifierMarkerTemplate, "new abstract") + name,
                    (MemberAttributes.Const, _, _, _) => String.Format(CodeNormalizer.PropertyModifierMarkerTemplate, "abstract override") + name,
                    (MemberAttributes.Final, null, true, false) => name,
                    (_, null, true, false) => String.Format(CodeNormalizer.PropertyModifierMarkerTemplate, "virtual") + name,
                    (_, true, true, false) => String.Format(CodeNormalizer.PropertyModifierMarkerTemplate, "new virtual") + name,
                    _ => name
                };
        }
    }
}
