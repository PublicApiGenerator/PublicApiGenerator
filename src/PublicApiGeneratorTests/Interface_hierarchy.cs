using System;
using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Interface_hierarchy : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_implemented_interfaces_in_alphabetical_order()
        {
            AssertPublicApi<IInterfaceWithImplementedList>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IInterfaceWithImplementedList : System.ICloneable, System.IDisposable
    {
        void Method();
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public interface IInterfaceWithImplementedList : IDisposable, ICloneable
        {
            void Method();
        }
    }
    // ReSharper restore UnusedMember.Global
}