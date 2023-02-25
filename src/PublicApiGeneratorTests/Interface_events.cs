using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Interface_events : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_event()
        {
            AssertPublicApi<ISimpleEvent>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface ISimpleEvent
    {
        event System.EventHandler Event;
    }
}");
        }

        [Fact]
        public void Should_output_event_redeclaration()
        {
            AssertPublicApi<IInheritedEvent>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public interface IInheritedEvent : PublicApiGeneratorTests.Examples.ISimpleEvent
    {
        new event System.EventHandler Event;
    }
}");
        }

        [Fact]
        public void Should_output_event_with_generics()
        {
            AssertPublicApi<IGenericEventHandler>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IGenericEventHandler
    {
        event System.EventHandler<System.EventArgs> Event;
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

        public interface IInheritedEvent : ISimpleEvent
        {
            new event EventHandler Event;
        }
    }
    // ReSharper restore EventNeverSubscribedTo.Global
}
