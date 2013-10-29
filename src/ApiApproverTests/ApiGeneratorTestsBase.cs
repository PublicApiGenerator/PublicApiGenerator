using System;
using System.Text.RegularExpressions;
using ApiApprover;
using Mono.Cecil;
using Xunit;

namespace ApiApproverTests
{
    public abstract class ApiGeneratorTestsBase : IUseFixture<AssemblyDefinitionFixture>
    {
        private static readonly Regex StripEmptyLines = new Regex(@"^\s+$[\r\n]*", RegexOptions.Multiline | RegexOptions.Compiled);

        protected AssemblyDefinitionFixture FixtureData;

        protected void AssertPublicApi<T>(string expectedOutput)
        {
            AssertPublicApi(typeof(T), expectedOutput);
        }

        protected void AssertPublicApi(Type type, string expectedOutput)
        {
            var assemblyDefinition = FixtureData.GetAssemblyDefinitionForTypes(type);
            AssertPublicApi(assemblyDefinition, expectedOutput);
        }

        protected void AssertPublicApi(AssemblyDefinition assemblyDefinition, string expectedOutput)
        {
            var output = PublicApiGenerator.CreatePublicApiForAssembly(assemblyDefinition);
            output = StripEmptyLines.Replace(output, string.Empty);
            Assert.Equal(expectedOutput, output);
        }

        public void SetFixture(AssemblyDefinitionFixture data)
        {
            FixtureData = data;
        }
    }
}