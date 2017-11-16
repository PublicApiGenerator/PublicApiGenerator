using System;
using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Class_events : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_event()
        {
            AssertPublicApi<ClassWithEvent>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithEvent
    {
        public ClassWithEvent() { }
        public event System.EventHandler<System.EventArgs> OnClicked;
    }
}");
        }
    }

    // ReSharper disable EventNeverSubscribedTo.Global
    // ReSharper disable EventNeverInvoked
    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class ClassWithEvent
        {
            public event EventHandler<EventArgs> OnClicked;
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
    // ReSharper restore EventNeverInvoked
    // ReSharper restore EventNeverSubscribedTo.Global
}