using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Struct_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_public_struct()
        {
            AssertPublicApi<PublicStruct>(
@"namespace PublicApiGeneratorTests.Examples
{
    public struct PublicStruct { }
}");
        }

        [Fact]
        public void Should_not_output_internal_struct()
        {
            AssertPublicApi<InternalStruct>(string.Empty);
        }
    }

    namespace Examples
    {
        public struct PublicStruct
        {
        }

        internal struct InternalStruct
        {
        }
    }
}
