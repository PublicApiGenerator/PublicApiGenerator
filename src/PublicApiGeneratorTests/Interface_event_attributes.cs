using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Interface_event_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_to_event()
        {
            AssertPublicApi<IInterfaceWithEventWithAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IInterfaceWithEventWithAttribute
    {
        [PublicApiGeneratorTests.Examples.Simple]
        event System.EventHandler OnClicked;
    }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            var options = new DefaultApiGeneratorOptions
            {
                ExcludeAttributes = new[] { "PublicApiGeneratorTests.Examples.SimpleAttribute" }
            };

            AssertPublicApi<IInterfaceWithEventWithAttribute>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public interface IInterfaceWithEventWithAttribute
    {
        event System.EventHandler OnClicked;
    }
}", options);
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
