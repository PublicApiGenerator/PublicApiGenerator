using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Method_extensions : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_extension_methods()
        {
            // Note the clas static reverse order hack
            AssertPublicApi(typeof(StringExtensions),
@"namespace ApiApproverTests.Examples
{
    public class static StringExtensions
    {
        public static bool CheckLength(this string value, int length) { }
    }
}");
        }
    }

    namespace Examples
    {
        public static class StringExtensions
        {
            public static bool CheckLength(this string value, int length)
            {
                return value.Length == length;
            }
        }
    }
}