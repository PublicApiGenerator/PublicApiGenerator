using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Interface_hierarchy : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_implemented_interfaces_in_alphabetical_order()
        {
            AssertPublicApi<IInterfaceWithImplementedList>("""
namespace PublicApiGeneratorTests.Examples
{
    public interface IInterfaceWithImplementedList : System.ICloneable, System.IDisposable
    {
        void Method();
    }
}
""");
        }
    }

    namespace Examples
    {
        public interface IInterfaceWithImplementedList : IDisposable, ICloneable
        {
            void Method();
        }
    }
}
