using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Delegate_types : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_void_delegate()
        {
            AssertPublicApi<VoidDelegate>(
@"namespace PublicApiGeneratorTests.Examples
{
    public delegate void VoidDelegate();
}");
        }

        [Fact]
        public void Should_output_primitive_types()
        {
            AssertPublicApi<DelegateWithPrimitiveParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public delegate int DelegateWithPrimitiveParameters(int v1, string v2);
}");
        }

        [Fact]
        public void Should_output_fully_qualified_name_for_complex_types()
        {
            AssertPublicApi<DelegateWithComplexParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public delegate PublicApiGeneratorTests.Examples.ComplexType DelegateWithComplexParameters(PublicApiGeneratorTests.Examples.ComplexType v1);
}");
        }

        [Fact]
        public void Should_output_fully_qualified_names_for_generic_types()
        {
            AssertPublicApi(typeof(DelegateWithClosedGenericParameters),
@"namespace PublicApiGeneratorTests.Examples
{
    public delegate PublicApiGeneratorTests.Examples.GenericType<int> DelegateWithClosedGenericParameters(PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType> value);
}");
        }

        [Fact]
        public void Should_output_generic_type_parameters()
        {
            AssertPublicApi(typeof(DelegateWithGenericParameters<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public delegate PublicApiGeneratorTests.Examples.GenericType<T> DelegateWithGenericParameters<T>(PublicApiGeneratorTests.Examples.GenericType<T> value);
}");
        }
    }

    namespace Examples
    {
        public delegate void VoidDelegate();
        public delegate int DelegateWithPrimitiveParameters(int v1, string v2);
        public delegate ComplexType DelegateWithComplexParameters(ComplexType v1);
        public delegate GenericType<int> DelegateWithClosedGenericParameters(GenericType<ComplexType> value);
        public delegate GenericType<T> DelegateWithGenericParameters<T>(GenericType<T> value);
    }
}