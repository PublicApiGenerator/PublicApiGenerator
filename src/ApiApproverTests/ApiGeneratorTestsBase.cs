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

        protected void AssertPublicApi(Type[] types, string expectedOutput, bool includeAssemblyAttributes = false, string[] whitelistedNamespacePrefixes = default(string[]))
        {
            AssertPublicApi(GetType().Assembly, types, expectedOutput, includeAssemblyAttributes, whitelistedNamespacePrefixes);
        }

        private static void AssertPublicApi(Assembly assembly, Type[] types, string expectedOutput, bool includeAssemblyAttributes, string[] whitelistedNamespacePrefixes)
        {
            var actualOutput = PublicApiGenerator.ApiGenerator.GeneratePublicApi(assembly, types, includeAssemblyAttributes, whitelistedNamespacePrefixes);
            actualOutput = StripEmptyLines.Replace(actualOutput, string.Empty);
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}