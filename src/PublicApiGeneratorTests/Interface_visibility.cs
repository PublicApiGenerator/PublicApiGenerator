using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Interface_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_public_interface()
        {
            AssertPublicApi<IPublicInterface>(
@"namespace PublicApiGeneratorTests.Examples
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

    namespace Examples
    {
        public interface IPublicInterface
        {
        }

        internal interface IInternalInterface
        {
        }
    }
}
