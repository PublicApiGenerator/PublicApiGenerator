using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Class_event_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_to_event()
        {
            AssertPublicApi<ClassWithEventWithAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithEventWithAttribute
    {
        public ClassWithEventWithAttribute() { }
        [PublicApiGeneratorTests.Examples.Simple]
        public event System.EventHandler OnClicked;
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

            AssertPublicApi<ClassWithEventWithAttribute>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithEventWithAttribute
    {
        public ClassWithEventWithAttribute() { }
        public event System.EventHandler OnClicked;
    }
}", options);
        }
    }

    namespace Examples
    {
        public class ClassWithEventWithAttribute
        {
            [SimpleAttribute]
            public event EventHandler OnClicked;
        }
    }
}
