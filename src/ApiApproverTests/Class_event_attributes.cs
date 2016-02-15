using System;
using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Class_event_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_to_event()
        {
            AssertPublicApi<ClassWithEventWithAttribute>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithEventWithAttribute
    {
        public ClassWithEventWithAttribute() { }
        [ApiApproverTests.Examples.SimpleAttribute()]
        public event System.EventHandler OnClicked;
    }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            AssertPublicApi<ClassWithEventWithAttribute>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithEventWithAttribute
    {
        public ClassWithEventWithAttribute() { }
        public event System.EventHandler OnClicked;
    }
}", excludedAttributes: new[] { "ApiApproverTests.Examples.SimpleAttribute" });
        }
    }

    // ReSharper disable EventNeverSubscribedTo.Global
    // ReSharper disable EventNeverInvoked
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class ClassWithEventWithAttribute
        {
            [SimpleAttribute]
            public event EventHandler OnClicked;
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore EventNeverInvoked
    // ReSharper restore EventNeverSubscribedTo.Global
}
