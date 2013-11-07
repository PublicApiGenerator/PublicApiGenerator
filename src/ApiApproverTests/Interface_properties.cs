using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Interface_properties : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_primitive_property()
        {
            AssertPublicApi<IPropertyWithPrimitiveType>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithPrimitiveType
    {
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_read_only_property()
        {
            AssertPublicApi<IPropertyWithReadOnlyType>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithReadOnlyType
    {
        string Value { get; }
    }
}");
        }

        [Fact]
        public void Should_output_write_only_property()
        {
            AssertPublicApi<IPropertyWithWriteOnlyType>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithWriteOnlyType
    {
        string Value { set; }
    }
}");
        }

        [Fact]
        public void Should_output_property_with_complex_type()
        {
            AssertPublicApi<IPropertyWithComplexType>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithComplexType
    {
        ApiApproverTests.Examples.ComplexType Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_property_with_generic_type()
        {
            AssertPublicApi<IPropertyWithGenericType>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithGenericType
    {
        ApiApproverTests.Examples.GenericType<int> Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_property_with_complex_generic_type()
        {
            AssertPublicApi<IPropertyWithComplexGenericType>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithComplexGenericType
    {
        ApiApproverTests.Examples.GenericTypeExtra<int, string, ApiApproverTests.Examples.ComplexType> Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_use_interface_generic_type()
        {
            AssertPublicApi(typeof (IPropertyWithInterfaceGeneric<>),
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithInterfaceGeneric<T>
    {
        T Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_new_modifier()
        {
            AssertPublicApi<IPropertyHiding>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyHiding : ApiApproverTests.Examples.IPropertyWithPrimitiveType
    {
        new string Value { get; set; }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public interface IPropertyWithPrimitiveType
        {
            string Value { get; set; }
        }

        public interface IPropertyHiding : IPropertyWithPrimitiveType
        {
            new string Value { get; set; }
        }

        public interface IPropertyWithReadOnlyType
        {
            string Value { get; }
        }

        public interface IPropertyWithWriteOnlyType
        {
            string Value { set; }
        }

        public interface IPropertyWithComplexType
        {
            ComplexType Value { get; set; }
        }

        public interface IPropertyWithGenericType
        {
            GenericType<int> Value { get; set; }
        }

        public interface IPropertyWithComplexGenericType
        {
            GenericTypeExtra<int, string, ComplexType> Value { get; set; }
        }

        public interface IPropertyWithInterfaceGeneric<T>
        {
            T Value { get; set; }
        }
    }
    // ReSharper restore UnusedMember.Global
}