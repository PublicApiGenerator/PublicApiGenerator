using System;
using PublicApiGeneratorTests.Examples;
using Xunit;

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
        public event System.EventHandler OnClicked;
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
        public event System.EventHandler OnClicked;
    }
}", excludeAttributes: new[] { "PublicApiGeneratorTests.Examples.SimpleAttribute" });
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
