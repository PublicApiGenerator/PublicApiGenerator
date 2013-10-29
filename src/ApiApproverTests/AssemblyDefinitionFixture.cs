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
            var typeDefinition = originalAssemblyDefinition.MainModule.GetType(fullName);
            Assert.NotNull(typeDefinition);
            typeDefinition.Module.Types.Remove(typeDefinition);

            var newDefinition = AssemblyDefinition.CreateAssembly(originalAssemblyDefinition.Name,
                originalAssemblyDefinition.MainModule.Name, originalAssemblyDefinition.MainModule.Kind);
            newDefinition.MainModule.Types.Add(typeDefinition);

            return newDefinition;
        }
    }
}