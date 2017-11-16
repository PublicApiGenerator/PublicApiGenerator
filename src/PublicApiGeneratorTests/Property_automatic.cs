using PublicApiGeneratorTests.Examples;
using Xunit;

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

    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable ValueParameterNotUsed
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
                get { return string.Empty; }
                set { }
            }
        }
    }
    // ReSharper restore ValueParameterNotUsed
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}