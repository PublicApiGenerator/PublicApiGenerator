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
                ExcludeAttributes = ["PublicApiGeneratorTests.Examples.SimpleAttribute"]
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

    namespace Examples
    {
        public interface IInterfaceWithEventWithAttribute
        {
            [SimpleAttribute]
            event EventHandler OnClicked;
        }
    }
}
