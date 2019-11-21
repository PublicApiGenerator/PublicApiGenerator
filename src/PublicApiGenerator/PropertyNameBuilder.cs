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

            return ModifierMarkerNameBuilder.Build(propertyDefinition.GetMethod, getAccessorAttributes, isNew, name,
                CodeNormalizer.PropertyModifierMarkerTemplate);
        }
    }
}
