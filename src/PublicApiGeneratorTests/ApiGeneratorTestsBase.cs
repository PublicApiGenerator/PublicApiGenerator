using System.Reflection;
using System.Text.RegularExpressions;
using PublicApiGenerator;

namespace PublicApiGeneratorTests;

public abstract class ApiGeneratorTestsBase
{
    private static readonly Regex StripEmptyLines = new(@"^\s+$[\r\n]*", RegexOptions.Multiline | RegexOptions.Compiled);

    protected void AssertPublicApi<T>(string expectedOutput) => AssertPublicApi<T>(expectedOutput, _ => { });

    protected void AssertPublicApi<T>(string expectedOutput, Action<ApiGeneratorOptions> configure)
    {
        AssertPublicApi(typeof(T), expectedOutput, configure);
    }

    protected void AssertPublicApi(Type type, string expectedOutput)
    {
        AssertPublicApi(type, expectedOutput, _ => { });
    }

    protected void AssertPublicApi(Type type, string expectedOutput, Action<ApiGeneratorOptions> configure)
    {
        AssertPublicApi(new[] { type }, expectedOutput, configure);
    }

    protected void AssertPublicApi(Type[] types, string expectedOutput)
    {
        AssertPublicApi(types, expectedOutput, _ => { });
    }

    protected void AssertPublicApi(Type[] types, string expectedOutput, Action<ApiGeneratorOptions> configure)
    {
        var options = new ApiGeneratorOptions
        {
            IncludeAssemblyAttributes = false,
            IncludeTypes = types
        };
        configure(options);

        AssertPublicApi(types[0].Assembly, expectedOutput, options);
    }

    private static void AssertPublicApi(Assembly assembly, string expectedOutput, ApiGeneratorOptions options)
    {
        string actualOutput = assembly.GeneratePublicApi(options);
        actualOutput = StripEmptyLines.Replace(actualOutput, string.Empty);
        try
        {
            Assert.Equal(expectedOutput, actualOutput, ignoreCase: false, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
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
