using System;
using System.CodeDom;
using System.Linq;
using Mono.Cecil;
using static System.String;

namespace PublicApiGenerator
{
    public static class MethodNameBuilder
    {
        public static string AugmentMethodNameWithMethodModifierMarkerTemplate(MethodDefinition methodDefinition,
            MemberAttributes attributes)
        {
            var name = CSharpOperatorKeyword.Get(methodDefinition.Name);

            if (methodDefinition.DeclaringType.IsInterface)
            {
                return name;
            }

            bool? isNew = null;
            var baseType = methodDefinition.DeclaringType.BaseType;
            while (baseType != null)
            {
                var typeDef = baseType as TypeDefinition;
                // too simple, improve
                isNew = typeDef?.Methods.Any(e => e.Name.Equals(methodDefinition.Name, StringComparison.Ordinal) && e.Parameters.Count == methodDefinition.Parameters.Count);
                if (isNew is true)
                {
                    break;
                }
                baseType = typeDef?.BaseType;
            }

            return (attributes & MemberAttributes.ScopeMask, isNew, methodDefinition.IsVirtual,
                    methodDefinition.IsAbstract) switch
                {
                    (MemberAttributes.Static, null, _, _) => Format(CodeNormalizer.MethodModifierMarkerTemplate, "static") + name,
                    (MemberAttributes.Static, true, _, _) => Format(CodeNormalizer.MethodModifierMarkerTemplate, "new static") + name,
                    (MemberAttributes.Override, _, _, _) => Format(CodeNormalizer.MethodModifierMarkerTemplate, "override") + name,
                    (MemberAttributes.Final | MemberAttributes.Override, _, _, _) => Format(CodeNormalizer.MethodModifierMarkerTemplate,"sealed override") + name,
                    (MemberAttributes.Final, true, _, _) => Format(CodeNormalizer.MethodModifierMarkerTemplate, "new") + name,
                    (MemberAttributes.Abstract, null, _, _) => Format(CodeNormalizer.MethodModifierMarkerTemplate, "abstract") + name,
                    (MemberAttributes.Abstract, true, _, _) => Format(CodeNormalizer.MethodModifierMarkerTemplate, "new abstract") + name,
                    (MemberAttributes.Const, _, _, _) => Format(CodeNormalizer.MethodModifierMarkerTemplate, "abstract override") + name,
                    (MemberAttributes.Final, null, true, false) => name,
                    (_, null, true, false) => Format(CodeNormalizer.MethodModifierMarkerTemplate, "virtual") + name,
                    (_, true, true, false) => Format(CodeNormalizer.MethodModifierMarkerTemplate, "new virtual") + name,
                    _ => name
                };
        }
    }
}
