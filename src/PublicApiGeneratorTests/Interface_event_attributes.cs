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
            AssertPublicApi<IInterfaceWithEventWithAttribute>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public interface IInterfaceWithEventWithAttribute
    {
        event System.EventHandler OnClicked;
    }
}", opt => opt.ExcludeAttributes = ["PublicApiGeneratorTests.Examples.SimpleAttribute"]);
        }
    }

    namespace Examples
    {
        public interface IInterfaceWithEventWithAttribute
        {
            [SimpleAttribute]
            event EventHandler OnClicked;
        }
    }
}
