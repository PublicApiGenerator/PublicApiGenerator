using System.Linq;
using Mono.Cecil;

namespace ApiApproverTests
{
    public class AssemblyDefinitionFixture
    {
        public AssemblyDefinition GetAssemblyDefinitionForType<T>()
        {
            return GetAssemblyDefinitionForType(typeof (T).FullName);
        }

        private AssemblyDefinition GetAssemblyDefinitionForType(string fullName)
        {
            var assemblyDefinition = AssemblyDefinition.ReadAssembly(GetType().Assembly.Location);
            var typeDefinitions = assemblyDefinition.MainModule.Types.Where(t => t.FullName != fullName).ToArray();
            foreach (var typeDefinition in typeDefinitions)
                assemblyDefinition.MainModule.Types.Remove(typeDefinition);

            return assemblyDefinition;
        }
    }
}