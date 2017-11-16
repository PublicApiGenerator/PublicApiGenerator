using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Method_return_types : ApiGeneratorTestsBase
    {

        [Fact]
        public void Should_handle_void_methods()
        {
            AssertPublicApi<VoidMethod>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class VoidMethod
    {
        public VoidMethod() { }
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_output_primitive_types()
        {
            AssertPublicApi<MethodWithPrimitiveReturnTypes>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithPrimitiveReturnTypes
    {
        public MethodWithPrimitiveReturnTypes() { }
        public int Method() { }
        public string Method2() { }
    }
}");
        }

        [Fact]
        public void Should_use_fully_qualified_type_name()
        {
            AssertPublicApi<MethodWithComplexReturnType>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithComplexReturnType
    {
        public MethodWithComplexReturnType() { }
        public PublicApiGeneratorTests.Examples.ComplexType Method() { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_parameters()
        {
            AssertPublicApi<MethodWithGenericReturnType>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithGenericReturnType
    {
        public MethodWithGenericReturnType() { }
        public PublicApiGeneratorTests.Examples.GenericType<int> Method() { }
    }
}");
        }

        [Fact]
        public void Should_use_fully_qualified_type_name_for_generic_parameters()
        {
            AssertPublicApi<MethodWithGenericComplexReturnType>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithGenericComplexReturnType
    {
        public MethodWithGenericComplexReturnType() { }
        public PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType> Method() { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_parameters_with_generic_parameters()
        {
            AssertPublicApi<MethodWithGenericTypeParameterOfGenericType>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithGenericTypeParameterOfGenericType
    {
        public MethodWithGenericTypeParameterOfGenericType() { }
        public PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType>> Method() { }
    }
}");
        }

        [Fact]
        public void Should_output_multiple_generic_parameters()
        {
            AssertPublicApi<MethodWithMultipleGenericTypeParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithMultipleGenericTypeParameters
    {
        public MethodWithMultipleGenericTypeParameters() { }
        public PublicApiGeneratorTests.Examples.GenericTypeExtra<int, string, PublicApiGeneratorTests.Examples.ComplexType> Method() { }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class VoidMethod
        {
            public void Method()
            {
            }
        }

        public class MethodWithPrimitiveReturnTypes
        {
            public int Method()
            {
                return 0;
            }

            public string Method2()
            {
                return null;
            }
        }

        public class MethodWithComplexReturnType
        {
            public ComplexType Method()
            {
                return null;
            }
        }

        public class MethodWithGenericReturnType
        {
            public GenericType<int> Method()
            {
                return null;
            }
        }

        public class MethodWithGenericComplexReturnType
        {
            public GenericType<ComplexType> Method()
            {
                return null;
            }
        }

        public class MethodWithGenericTypeParameterOfGenericType
        {
            public GenericType<GenericType<ComplexType>> Method()
            {
                return null;
            }
        }

        public class MethodWithMultipleGenericTypeParameters
        {
            public GenericTypeExtra<int, string, ComplexType> Method()
            {
                return null;
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}