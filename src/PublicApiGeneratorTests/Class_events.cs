using PublicApiGeneratorTests.Examples;

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

    namespace Examples
    {
        public class ClassWithEvent
        {
            public event EventHandler<EventArgs> OnClicked;
        }
    }
}
