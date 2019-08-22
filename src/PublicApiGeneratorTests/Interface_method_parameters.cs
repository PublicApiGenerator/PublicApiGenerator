using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Interface_method_parameters : ApiGeneratorTestsBase
    {
        [Fact]
        void Should_handle_no_parameters()
        {
            AssertPublicApi<IMethodWithNoParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithNoParameters
    {
        void Method();
    }
}");
        }

        [Fact]
        void Should_output_parameter_name()
        {
            AssertPublicApi<IMethodWithSingleParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithSingleParameter
    {
        void Method(int value);
    }
}");
        }

        [Fact]
        void Should_output_primitive_parameter()
        {
            AssertPublicApi<IMethodWithSingleParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithSingleParameter
    {
        void Method(int value);
    }
}");
        }

        [Fact]
        void Should_use_fully_qualified_type_name_for_parameter()
        {
            AssertPublicApi<IMethodWithComplexTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithComplexTypeParameter
    {
        void Method(PublicApiGeneratorTests.Examples.ComplexType value);
    }
}");
        }

        [Fact]
        void Should_output_generic_type()
        {
            AssertPublicApi<IMethodWithGenericTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithGenericTypeParameter
    {
        void Method(PublicApiGeneratorTests.Examples.GenericType<int> value);
    }
}");
        }

        [Fact]
        void Should_output_fully_qualified_type_name_for_generic_parameter()
        {
            AssertPublicApi<IMethodWithGenericTypeOfComplexTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithGenericTypeOfComplexTypeParameter
    {
        void Method(PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType> value);
    }
}");
        }

        [Fact]
        void Should_output_generic_type_of_generic_type()
        {
            AssertPublicApi<IMethodWithGenericTypeOfGenericTypeParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithGenericTypeOfGenericTypeParameter
    {
        void Method(PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.GenericType<int>> value);
    }
}");
        }

        [Fact]
        void Should_output_generic_with_multiple_type_parameters()
        {
            AssertPublicApi<IMethodWithGenericTypeWithMultipleTypeArgumentsParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithGenericTypeWithMultipleTypeArgumentsParameter
    {
        void Method(PublicApiGeneratorTests.Examples.GenericTypeExtra<int, string, PublicApiGeneratorTests.Examples.ComplexType> value);
    }
}");
        }

        [Fact]
        void Should_handle_multiple_parameters()
        {
            AssertPublicApi<IMethodWithMultipleParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithMultipleParameters
    {
        void Method(int value1, string value2, PublicApiGeneratorTests.Examples.ComplexType value3);
    }
}");
        }

        [Fact]
        void Should_output_default_values()
        {
            AssertPublicApi<IMethodWithDefaultValues>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithDefaultValues
    {
        void Method(int value1 = 42, string value2 = ""hello world"");
    }
}");
        }

        [Fact]
        void Should_output_ref_parameters()
        {
            AssertPublicApi<IMethodWithRefParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithRefParameter
    {
        void Method(ref string value);
    }
}");
        }

        [Fact]
        void Should_output_in_parameters()
        {
            AssertPublicApi<IMethodWithInParameter>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithInParameter
    {
        void Method(in string value);
    }
}");
        }

        [Fact]
        void Should_output_out_parameters()
        {
            AssertPublicApi<IMethodWithOutParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithOutParameter
    {
        void Method(out string value);
    }
}");
        }

        [Fact]
        void Should_output_params_keyword()
        {
            AssertPublicApi<IMethodWithParams>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithParams
    {
        void Method(string format, params object[] values);
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedParameter.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public interface IMethodWithNoParameters
        {
            void Method();
        }

        public interface IMethodWithSingleParameter
        {
            void Method(int value);
        }

        public interface IMethodWithComplexTypeParameter
        {
            void Method(ComplexType value);
        }

        public interface IMethodWithGenericTypeParameter
        {
            void Method(GenericType<int> value);
        }

        public interface IMethodWithGenericTypeOfComplexTypeParameter
        {
            void Method(GenericType<ComplexType> value);
        }

        public interface IMethodWithGenericTypeOfGenericTypeParameter
        {
            void Method(GenericType<GenericType<int>> value);
        }

        public interface IMethodWithGenericTypeWithMultipleTypeArgumentsParameter
        {
            void Method(GenericTypeExtra<int, string, ComplexType> value);
        }

        public interface IMethodWithMultipleParameters
        {
            void Method(int value1, string value2, ComplexType value3);
        }

        public interface IMethodWithDefaultValues
        {
            void Method(int value1 = 42, string value2 = "hello world");
        }

        public interface IMethodWithRefParameter
        {
            void Method(ref string value);
        }

        public interface IMethodWithInParameter
        {
            void Method(in string value);
        }

        public interface IMethodWithOutParameter
        {
            void Method(out string value);
        }

        public interface IMethodWithParams
        {
            void Method(string format, params object[] values);
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedParameter.Global
    // ReSharper restore UnusedMember.Global
}