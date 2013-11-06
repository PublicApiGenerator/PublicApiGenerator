using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Interface_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_public_interface()
        {
            AssertPublicApi<IPublicInterface>(
@"namespace ApiApproverTests.Examples
{
    public interface IPublicInterface { }
}");
        }

        [Fact]
        public void Should_not_output_internal_interface()
        {
            AssertPublicApi<IInternalInterface>(string.Empty);
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public interface IPublicInterface
        {
        }

        internal interface IInternalInterface
        {
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
}