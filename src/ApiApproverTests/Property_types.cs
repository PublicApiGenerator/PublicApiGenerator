using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Property_types : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_primitive_type()
        {
            AssertPublicApi<PropertyWithPrimitiveType>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithPrimitiveType
    {
        public PropertyWithPrimitiveType() { }
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_fully_qualified_name_of_complex_type()
        {
            AssertPublicApi<PropertyWithComplexType>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithComplexType
    {
        public PropertyWithComplexType() { }
        public ApiApproverTests.Examples.ComplexType Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_generic_type()
        {
            AssertPublicApi<PropertyWithGenericType>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithGenericType
    {
        public PropertyWithGenericType() { }
        public ApiApproverTests.Examples.GenericType<int> Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_generic_of_generic_of_complex_type()
        {
            AssertPublicApi<PropertyWithCrazyGenericType>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithCrazyGenericType
    {
        public PropertyWithCrazyGenericType() { }
        public ApiApproverTests.Examples.GenericTypeExtra<int, string, ApiApproverTests.Examples.GenericType<ApiApproverTests.Examples.ComplexType>> Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_use_class_generic_type()
        {
            AssertPublicApi(typeof(PropertyWithClassGeneric<>),
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithClassGeneric<T>
    {
        public PropertyWithClassGeneric() { }
        public T Value { get; set; }
    }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
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
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}