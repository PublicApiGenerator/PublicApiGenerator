using System;
using System.Reflection;
using System.Text.RegularExpressions;
using PublicApiGenerator;
using Xunit;

namespace PublicApiGeneratorTests
{
    public abstract class ApiGeneratorTestsBase
    {
        private static readonly Regex StripEmptyLines = new Regex(@"^\s+$[\r\n]*", RegexOptions.Multiline | RegexOptions.Compiled);

        protected void AssertPublicApi<T>(string expectedOutput, ApiGeneratorOptions? options = null)
        {
            AssertPublicApi(typeof(T), expectedOutput, options);
        }

        protected void AssertPublicApi(Type type, string expectedOutput, ApiGeneratorOptions? options = null)
        {
            AssertPublicApi(new[] { type }, expectedOutput, options);
        }

        protected void AssertPublicApi(Type[] types, string expectedOutput, ApiGeneratorOptions? options = null)
        {
            options ??= new DefaultApiGeneratorOptions();
            options.IncludeTypes = types;

            AssertPublicApi(types[0].Assembly, expectedOutput, options);
        }

        private static void AssertPublicApi(Assembly assembly, string expectedOutput,  ApiGeneratorOptions? options = null)
        {
            options ??= new DefaultApiGeneratorOptions();

            var actualOutput = assembly.GeneratePublicApi(options);
            actualOutput = StripEmptyLines.Replace(actualOutput, string.Empty);
            try
            {
                Assert.Equal(expectedOutput, actualOutput, ignoreCase: false, ignoreLineEndingDifferences: true,
                    ignoreWhiteSpaceDifferences: true);
            }
            catch
            {
                Console.WriteLine("Full expected output:");
                Console.WriteLine(expectedOutput);
                Console.WriteLine("Full actual output:");
                Console.WriteLine(actualOutput);
                throw;
            }
        }
    }
}
