using System.Linq;
using Mono.Cecil;
using Xunit;

namespace ApiApproverTests
{
    public class AssemblyDefinitionFixture
    {
        private readonly AssemblyDefinition originalAssemblyDefinition;

        public AssemblyDefinitionFixture()
        {
            originalAssemblyDefinition = AssemblyDefinition.ReadAssembly(GetType().Assembly.Location);
        }

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