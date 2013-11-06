using System;
using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Interace_events : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_event()
        {
            // TODO: CodeDOM outputs "public" in event declarations in interfaces
            // This looks like a bug? That's what the implementation does, but it's
            // not valid C#...
            AssertPublicApi<ISimpleEvent>(
@"namespace ApiApproverTests.Examples
{
    public interface ISimpleEvent
    {
        public event System.EventHandler Event;
    }
}");
        }

        [Fact]
        public void Should_output_event_with_generics()
        {
            AssertPublicApi<IGenericEventHandler>(
@"namespace ApiApproverTests.Examples
{
    public interface IGenericEventHandler
    {
        public event System.EventHandler<System.EventArgs> Event;
    }
}");
        }
    }

    // ReSharper disable EventNeverSubscribedTo.Global
    namespace Examples
    {
        public interface ISimpleEvent
        {
            event EventHandler Event;
        }

        public interface IGenericEventHandler
        {
            event EventHandler<EventArgs> Event;
        }
    }
    // ReSharper restore EventNeverSubscribedTo.Global
}