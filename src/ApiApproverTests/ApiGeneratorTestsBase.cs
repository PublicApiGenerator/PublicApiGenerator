using System.Text.RegularExpressions;
using ApiApprover;
using Xunit;

namespace ApiApproverTests
{
    public abstract class ApiGeneratorTestsBase : IUseFixture<AssemblyDefinitionFixture>
    {
        private static readonly Regex StripEmptyLines = new Regex(@"^\s+$[\r\n]*", RegexOptions.Multiline | RegexOptions.Compiled);

        private AssemblyDefinitionFixture fixtureData;

        protected void AssertPublicApi<T>(string expectedOutput)
        {
            var output = PublicApiGenerator.CreatePublicApiForAssembly(fixtureData.GetAssemblyDefinitionForType<T>());
            output = StripEmptyLines.Replace(output, string.Empty);
            Assert.Equal(expectedOutput, output);
        }

        public void SetFixture(AssemblyDefinitionFixture data)
        {
            fixtureData = data;
        }
    }
}