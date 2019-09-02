using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Struct_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_public_struct()
        {
            AssertRoslynPublicApi<PublicStruct>(
@"namespace PublicApiGeneratorTests.Examples
{
    public struct PublicStruct { }
}");
        }

        [Fact]
        public void Should_not_output_internal_struct()
        {
            AssertRoslynPublicApi<InternalStruct>(string.Empty);
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public struct PublicStruct
        {
        }

        internal struct InternalStruct
        {
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
}