using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

namespace ApiApproverTests
{
    public abstract class ApiGeneratorTestsBase
    {
        private static readonly Regex StripEmptyLines = new Regex(@"^\s+$[\r\n]*", RegexOptions.Multiline | RegexOptions.Compiled);

        protected void AssertPublicApi<T>(string expectedOutput, bool includeAssemblyAttributes = false)
        {
            AssertPublicApi(typeof(T), expectedOutput, includeAssemblyAttributes);
        }

        protected void AssertPublicApi(Type type, string expectedOutput, bool includeAssemblyAttributes = false)
        {
            AssertPublicApi(new[] { type }, expectedOutput, includeAssemblyAttributes);
        }

        protected void AssertPublicApi(Type[] types, string expectedOutput, bool includeAssemblyAttributes = false)
        {
            AssertPublicApi(GetType().Assembly, types, expectedOutput, includeAssemblyAttributes);
        }

        private static void AssertPublicApi(Assembly assembly, Type[] types, string expectedOutput, bool includeAssemblyAttributes)
        {
            var actualOutput = PublicApiGenerator.PublicApiGenerator.GetPublicApi(assembly, types, includeAssemblyAttributes);
            actualOutput = StripEmptyLines.Replace(actualOutput, string.Empty);
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}