using System;
using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Interface_event_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_to_event()
        {
            AssertPublicApi<IInterfaceWithEventWithAttribute>(
@"namespace ApiApproverTests.Examples
{
    public interface IInterfaceWithEventWithAttribute
    {
        [ApiApproverTests.Examples.SimpleAttribute()]
        public event System.EventHandler OnClicked;
    }
}");
        }
    }

    // ReSharper disable EventNeverSubscribedTo.Global
    // ReSharper disable EventNeverInvoked
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public interface IInterfaceWithEventWithAttribute
        {
            [SimpleAttribute]
            event EventHandler OnClicked;
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore EventNeverInvoked
    // ReSharper restore EventNeverSubscribedTo.Global
}