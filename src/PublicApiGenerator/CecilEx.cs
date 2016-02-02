using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace PublicApiGenerator
{
    public static class CecilEx
    {
        public static IEnumerable<IMemberDefinition> GetMembers(this TypeDefinition type)
        {
            return type.Fields.Cast<IMemberDefinition>()
                .Concat(type.Methods)
                .Concat(type.Properties)
                .Concat(type.Events);
        }
    }
}