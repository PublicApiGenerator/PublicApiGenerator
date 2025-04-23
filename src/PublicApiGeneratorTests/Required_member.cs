using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    // https://github.com/PublicApiGenerator/PublicApiGenerator/issues/472
    public class Required_member : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_required_member()
        {
            AssertPublicApi<PropertyRequiredInit>("""
namespace PublicApiGeneratorTests.Examples
{
    public class PropertyRequiredInit
    {
        public PropertyRequiredInit() { }
        public required string Value { init; }
    }
}
""");
        }
    }

    namespace Examples
    {
        public class PropertyRequiredInit
        {
            public required string Value { init { } }
        }
    }
}
