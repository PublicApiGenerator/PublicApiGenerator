using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ApiApprover;
using Mono.Cecil;
using Xunit;

namespace ApiApproverTests
{
    public abstract class ApiGeneratorTestsBase
    {
        private static readonly Regex StripEmptyLines = new Regex(@"^\s+$[\r\n]*", RegexOptions.Multiline | RegexOptions.Compiled);

        protected void AssertPublicApi<T>(string expectedOutput)
        {
            AssertPublicApi(typeof(T), expectedOutput);
        }

        protected void AssertPublicApi(Type type, string expectedOutput)
        {
            AssertPublicApi(new[] { type }, expectedOutput);
        }

        protected void AssertPublicApi(Type[] types, string expectedOutput)
        {
            var assemblyDefinition = AssemblyDefinition.ReadAssembly(GetType().Assembly.Location);
            AssertPublicApi(assemblyDefinition, types, expectedOutput);
        }

        private static void AssertPublicApi(AssemblyDefinition assemblyDefinition, Type[] types, string expectedOutput)
        {
            var typesNames = new HashSet<string>(types.Select(t => t.FullName));
            var actualOutput = PublicApiGenerator.CreatePublicApiForAssembly(assemblyDefinition, t => typesNames.Contains(t.FullName));
            actualOutput = StripEmptyLines.Replace(actualOutput, string.Empty);
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}