using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Property_types : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_primitive_type()
        {
            AssertPublicApi<PropertyWithPrimitiveType>("""
namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithPrimitiveType
    {
        public PropertyWithPrimitiveType() { }
        public string Value { get; set; }
    }
}
""");
        }

        [Fact]
        public void Should_output_fully_qualified_name_of_complex_type()
        {
            AssertPublicApi<PropertyWithComplexType>("""
namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithComplexType
    {
        public PropertyWithComplexType() { }
        public PublicApiGeneratorTests.Examples.ComplexType Value { get; set; }
    }
}
""");
        }

        [Fact]
        public void Should_output_generic_type()
        {
            AssertPublicApi<PropertyWithGenericType>("""
namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithGenericType
    {
        public PropertyWithGenericType() { }
        public PublicApiGeneratorTests.Examples.GenericType<int> Value { get; set; }
    }
}
""");
        }

        [Fact]
        public void Should_output_generic_of_generic_of_complex_type()
        {
            AssertPublicApi<PropertyWithCrazyGenericType>("""
namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithCrazyGenericType
    {
        public PropertyWithCrazyGenericType() { }
        public PublicApiGeneratorTests.Examples.GenericTypeExtra<int, string, PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType>> Value { get; set; }
    }
}
""");
        }

        [Fact]
        public void Should_use_class_generic_type()
        {
            AssertPublicApi(typeof(PropertyWithClassGeneric<>), """
namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithClassGeneric<T>
    {
        public PropertyWithClassGeneric() { }
        public T Value { get; set; }
    }
}
""");
        }
    }

    namespace Examples
    {
        public class PropertyWithPrimitiveType
        {
            public string Value { get; set; }
        }

        public class PropertyWithComplexType
        {
            public ComplexType Value { get; set; }
        }

        public class PropertyWithGenericType
        {
            public GenericType<int> Value { get; set; }
        }

        public class PropertyWithCrazyGenericType
        {
            public GenericTypeExtra<int, string, GenericType<ComplexType>> Value { get; set; }
        }

        public class PropertyWithClassGeneric<T>
        {
            public T Value { get; set; }
        }
    }
}
