using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Method_extensions : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_extension_methods()
        {
            // Note the class static reverse order hack
            AssertPublicApi(typeof(StringExtensions),
@"namespace PublicApiGeneratorTests.Examples
{
    public class static StringExtensions
    {
        public static bool CheckLength(this string value, int length) { }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
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
    // ReSharper restore UnusedMember.Global
}