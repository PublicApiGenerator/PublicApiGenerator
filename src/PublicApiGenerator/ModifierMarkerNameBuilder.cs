using System.CodeDom;
using Mono.Cecil;
using static System.String;

namespace PublicApiGenerator
{
    static class ModifierMarkerNameBuilder
    {
        public static string Build(MethodDefinition methodDefinition, MemberAttributes getAccessorAttributes, bool? isNew, string name, string markerTemplate)
        {
            return (getAccessorAttributes & MemberAttributes.ScopeMask, isNew, methodDefinition.IsVirtual,
                    methodDefinition.IsAbstract) switch
                {
                    (MemberAttributes.Static, null, _, _) => Format(markerTemplate, " static ") + name,
                    (MemberAttributes.Static, true, _, _) => Format(markerTemplate, " new static ") + name,
                    (MemberAttributes.Override, _, _, _) => Format(markerTemplate, " override ") + name,
                    (MemberAttributes.Final | MemberAttributes.Override, _, _, _) => Format(markerTemplate," override sealed ") + name,
                    (MemberAttributes.Final, true, _, _) => Format(markerTemplate, " new ") + name,
                    (MemberAttributes.Abstract, null, _, _) => Format(markerTemplate, " abstract ") + name,
                    (MemberAttributes.Abstract, true, _, _) => Format(markerTemplate, " new abstract ") + name,
                    (MemberAttributes.Const, _, _, _) => Format(markerTemplate, " abstract override ") + name,
                    (MemberAttributes.Final, null, true, false) => name,
                    (_, null, true, false) => Format(markerTemplate, " virtual ") + name,
                    (_, true, true, false) => Format(markerTemplate, " new virtual ") + name,
                    _ => name
                };
        }
    }
}
