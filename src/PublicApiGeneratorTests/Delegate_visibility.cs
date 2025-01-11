using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Delegate_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_not_output_internal_delegate()
        {
            AssertPublicApi<InternalDelegate>(string.Empty);
        }

        [Fact]
        public void Should_output_public_delegate()
        {
            AssertPublicApi<PublicDelegate>("""
namespace PublicApiGeneratorTests.Examples
{
    public delegate void PublicDelegate();
}
""");
        }
    }

    namespace Examples
    {
        internal delegate void InternalDelegate();
        public delegate void PublicDelegate();
    }
}