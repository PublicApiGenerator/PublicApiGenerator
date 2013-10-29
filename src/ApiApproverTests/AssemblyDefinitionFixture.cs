using System;
using System.Diagnostics;
using System.Linq;
using Mono.Cecil;

namespace ApiApproverTests
{
    public class AssemblyDefinitionFixture
    {
        public AssemblyDefinition GetAssemblyDefinitionForType<T>()
        {
            return GetAssemblyDefinitionForTypes(typeof (T));
        }
        public AssemblyDefinition GetAssemblyDefinitionForTypes(params Type[] types)
        {
            var names = types.Select(t => t.FullName).ToList();
            var assemblyDefinition = AssemblyDefinition.ReadAssembly(GetType().Assembly.Location);
            var typeDefinitions = assemblyDefinition.MainModule.Types.Where(t => !names.Contains(t.FullName)).ToArray();
            foreach (var typeDefinition in typeDefinitions)
                assemblyDefinition.MainModule.Types.Remove(typeDefinition);

            return assemblyDefinition;
        }
    }
}