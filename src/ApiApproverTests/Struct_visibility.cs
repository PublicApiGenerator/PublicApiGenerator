using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Struct_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_public_struct()
        {
            AssertPublicApi<PublicStruct>(
@"namespace ApiApproverTests.Examples
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