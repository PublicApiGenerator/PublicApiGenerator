using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Property_automatic : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_automatic_property()
        {
            AssertPublicApi<ClassWithAutomaticProperty>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithAutomaticProperty
    {
        public ClassWithAutomaticProperty() { }
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_manually_implemented_property()
        {
            AssertPublicApi<ClassWithManualProperty>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithManualProperty
    {
        public ClassWithManualProperty() { }
        public string Value { get; set; }
    }
}");
        }
    }

    namespace Examples
    {
        public class ClassWithAutomaticProperty
        {
            public string Value { get; set; }
        }

        public class ClassWithManualProperty
        {
            public string Value
            {
                get => string.Empty;
                set { }
            }
        }
    }
}
