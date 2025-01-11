using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Constants : ApiGeneratorTestsBase
    {
        // https://github.com/PublicApiGenerator/PublicApiGenerator/issues/82
        [Fact]
        public void Should_escape_literals_in_public_constant_string()
        {
            AssertPublicApi<MyConstants>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MyConstants
    {
        public const string MULTILINE1 = "This\\r\\nIs\\r\\nMultiline\\r\\n";
        public const string MULTILINE2 = "This\\nIs\\nMultiline\\n";
        public const string SINGLELINE = "ABC";
        public MyConstants() { }
    }
}
""");
        }
    }

    namespace Examples
    {
        public class MyConstants
        {
            public const string SINGLELINE = "ABC";
            public const string MULTILINE1 = "This\r\nIs\r\nMultiline\r\n";
            public const string MULTILINE2 = "This\nIs\nMultiline\n";
        }
    }
}
