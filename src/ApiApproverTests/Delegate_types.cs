using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Delegate_types : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_void_delegate()
        {
            AssertPublicApi<VoidDelegate>(
@"namespace ApiApproverTests.Examples
{
    public delegate void VoidDelegate();
}");
        }

        [Fact]
        public void Should_output_primitive_types()
        {
            AssertPublicApi<DelegateWithPrimitveParameters>(
@"namespace ApiApproverTests.Examples
{
    public delegate int DelegateWithPrimitveParameters(int v1, string v2);
}");
        }

        [Fact]
        public void Should_output_fully_qualified_name_for_complex_types()
        {
            AssertPublicApi<DelegateWithComplexParameters>(
@"namespace ApiApproverTests.Examples
{
    public delegate ApiApproverTests.Examples.ComplexType DelegateWithComplexParameters(ApiApproverTests.Examples.ComplexType v1);
}");
        }

        [Fact]
        public void Should_output_fully_qualified_names_for_generic_types()
        {
            AssertPublicApi(typeof(DelegateWithClosedGenericParameters),
@"namespace ApiApproverTests.Examples
{
    public delegate ApiApproverTests.Examples.GenericType<int> DelegateWithClosedGenericParameters(ApiApproverTests.Examples.GenericType<ApiApproverTests.Examples.ComplexType> value);
}");
        }

        [Fact]
        public void Should_output_generic_type_parameters()
        {
            AssertPublicApi(typeof(DelegateWithGenericParameters<>),
@"namespace ApiApproverTests.Examples
{
    public delegate ApiApproverTests.Examples.GenericType<T> DelegateWithGenericParameters<T>(ApiApproverTests.Examples.GenericType<T> value);
}");
        }
    }

    namespace Examples
    {
        public delegate void VoidDelegate();
        public delegate int DelegateWithPrimitveParameters(int v1, string v2);
        public delegate ComplexType DelegateWithComplexParameters(ComplexType v1);
        public delegate GenericType<int> DelegateWithClosedGenericParameters(GenericType<ComplexType> value);
        public delegate GenericType<T> DelegateWithGenericParameters<T>(GenericType<T> value);
    }
}