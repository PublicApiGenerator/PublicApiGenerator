using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Xunit;

namespace PublicApiGeneratorTests
{
    public abstract class ApiGeneratorTestsBase
    {
        private static readonly Regex StripEmptyLines = new Regex(@"^\s+$[\r\n]*", RegexOptions.Multiline | RegexOptions.Compiled);

        protected void AssertPublicApi<T>(string expectedOutput, bool includeAssemblyAttributes = false, string[] excludeAttributes = null)
        {
            AssertPublicApi(typeof(T), expectedOutput, includeAssemblyAttributes, excludeAttributes);
        }

        protected void AssertPublicApi(Type type, string expectedOutput, bool includeAssemblyAttributes = false, string[] excludeAttributes = null)
        {
            AssertPublicApi(new[] { type }, expectedOutput, includeAssemblyAttributes, excludeAttributes: excludeAttributes);
        }

        protected void AssertPublicApi(Type[] types, string expectedOutput, bool includeAssemblyAttributes = false, string[] whitelistedNamespacePrefixes = default, string[] excludeAttributes = null)
        {
            AssertPublicApi(GetType().Assembly, types, expectedOutput, includeAssemblyAttributes, whitelistedNamespacePrefixes, excludeAttributes);
        }

        private static void AssertPublicApi(Assembly assembly, Type[] types, string expectedOutput, bool includeAssemblyAttributes, string[] whitelistedNamespacePrefixes, string[] excludeAttributes)
        {
            var actualOutput = PublicApiGenerator.ApiGenerator.GeneratePublicApi(assembly, types, includeAssemblyAttributes, whitelistedNamespacePrefixes, excludeAttributes);
            actualOutput = StripEmptyLines.Replace(actualOutput, string.Empty);
            Assert.Equal(expectedOutput, actualOutput, ignoreCase: false, ignoreLineEndingDifferences: true,
                ignoreWhiteSpaceDifferences: true);
        }
    }
}