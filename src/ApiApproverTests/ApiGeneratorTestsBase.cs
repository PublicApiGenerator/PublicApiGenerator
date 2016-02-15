using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

namespace ApiApproverTests
{
    public abstract class ApiGeneratorTestsBase
    {
        private static readonly Regex StripEmptyLines = new Regex(@"^\s+$[\r\n]*", RegexOptions.Multiline | RegexOptions.Compiled);

        protected void AssertPublicApi<T>(string expectedOutput, bool includeAssemblyAttributes = false, string[] excludedAttributes = null)
        {
            AssertPublicApi(typeof(T), expectedOutput, includeAssemblyAttributes, excludedAttributes);
        }

        protected void AssertPublicApi(Type type, string expectedOutput, bool includeAssemblyAttributes = false, string[] excludedAttributes = null)
        {
            AssertPublicApi(new[] { type }, expectedOutput, includeAssemblyAttributes, excludedAttributes);
        }

        protected void AssertPublicApi(Type[] types, string expectedOutput, bool includeAssemblyAttributes = false, string[] excludedAttributes = null)
        {
            AssertPublicApi(GetType().Assembly, types, expectedOutput, includeAssemblyAttributes, excludedAttributes);
        }

        private static void AssertPublicApi(Assembly assembly, Type[] types, string expectedOutput, bool includeAssemblyAttributes,
            string[] excludedAttributes)
        {
            var actualOutput = PublicApiGenerator.PublicApiGenerator.GetPublicApi(assembly, types, includeAssemblyAttributes, excludedAttributes);
            actualOutput = StripEmptyLines.Replace(actualOutput, string.Empty);
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}