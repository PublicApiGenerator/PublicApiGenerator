using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Method_parameters : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_handle_no_parameters()
        {
            AssertPublicApi<MethodWithNoParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithNoParameters
    {
        public MethodWithNoParameters() { }
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_output_parameter_name()
        {
            AssertPublicApi<MethodWithSingleParameter>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithSingleParameter
    {
        public MethodWithSingleParameter() { }
        public void Method(int value) { }
    }
}");
        }

        [Fact]
        public void Should_output_primitive_parameter()
        {
            AssertPublicApi<MethodWithSingleParameter>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithSingleParameter
    {
        public MethodWithSingleParameter() { }
        public void Method(int value) { }
    }
}");
        }

        [Fact]
        public void Should_use_fully_qualified_type_name_for_parameter()
        {
            AssertPublicApi<MethodWithComplexTypeParameter>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithComplexTypeParameter
    {
        public MethodWithComplexTypeParameter() { }
        public void Method(ApiApproverTests.Examples.ComplexType value) { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_type()
        {
            AssertPublicApi<MethodWithGenericTypeParameter>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithGenericTypeParameter
    {
        public MethodWithGenericTypeParameter() { }
        public void Method(ApiApproverTests.Examples.GenericType<int> value) { }
    }
}");
        }

        [Fact]
        public void Should_output_fully_qualified_type_name_for_generic_parameter()
        {
            AssertPublicApi<MethodWithGenericTypeOfComplexTypeParameter>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithGenericTypeOfComplexTypeParameter
    {
        public MethodWithGenericTypeOfComplexTypeParameter() { }
        public void Method(ApiApproverTests.Examples.GenericType<ApiApproverTests.Examples.ComplexType> value) { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_type_of_generic_type()
        {
            AssertPublicApi<MethodWithGenericTypeOfGenericTypeParameter>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithGenericTypeOfGenericTypeParameter
    {
        public MethodWithGenericTypeOfGenericTypeParameter() { }
        public void Method(ApiApproverTests.Examples.GenericType<ApiApproverTests.Examples.GenericType<int>> value) { }
    }
}");
        }

        [Fact]
        public void Should_output_generic_with_multiple_type_parameters()
        {
            AssertPublicApi<MethodWithGenericTypeWithMultipleTypeArgumentsParameter>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithGenericTypeWithMultipleTypeArgumentsParameter
    {
        public MethodWithGenericTypeWithMultipleTypeArgumentsParameter() { }
        public void Method(ApiApproverTests.Examples.GenericTypeExtra<int, string, ApiApproverTests.Examples.ComplexType> value) { }
    }
}");
        }

        [Fact]
        public void Should_handle_multiple_parameters()
        {
            AssertPublicApi<MethodWithMultipleParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithMultipleParameters
    {
        public MethodWithMultipleParameters() { }
        public void Method(int value1, string value2, ApiApproverTests.Examples.ComplexType value3) { }
    }
}");
        }

        [Fact]
        public void Should_output_default_values()
        {
            AssertPublicApi<MethodWithDefaultValues>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithDefaultValues
    {
        public MethodWithDefaultValues() { }
        public void Method(int value1 = 42, string value2 = ""hello world"") { }
    }
}");
        }

        [Fact]
        public void Should_output_ref_parameters()
        {
            AssertPublicApi<MethodWithRefParameter>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithRefParameter
    {
        public MethodWithRefParameter() { }
        public void Method(ref string value) { }
    }
}");
        }

        [Fact]
        public void Should_output_out_parameters()
        {
            AssertPublicApi<MethodWithOutParameter>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithOutParameter
    {
        public MethodWithOutParameter() { }
        public void Method(out string value) { }
    }
}");
        }

        [Fact]
        public void Should_output_params_keyword()
        {
            AssertPublicApi<MethodWithParams>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithParams
    {
        public MethodWithParams() { }
        public void Method(string format, params object[] values) { }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedParameter.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class MethodWithNoParameters
        {
            public void Method()
            {
            }
        }

        public class MethodWithSingleParameter
        {
            public void Method(int value)
            {
            }
        }

        public class MethodWithComplexTypeParameter
        {
            public void Method(ComplexType value)
            {
            }
        }

        public class MethodWithGenericTypeParameter
        {
            public void Method(GenericType<int> value)
            {
            }
        }

        public class MethodWithGenericTypeOfComplexTypeParameter
        {
            public void Method(GenericType<ComplexType> value)
            {
            }
        }

        public class MethodWithGenericTypeOfGenericTypeParameter
        {
            public void Method(GenericType<GenericType<int>> value)
            {
            }
        }

        public class MethodWithGenericTypeWithMultipleTypeArgumentsParameter
        {
            public void Method(GenericTypeExtra<int, string, ComplexType> value)
            {
            }
        }

        public class MethodWithMultipleParameters
        {
            public void Method(int value1, string value2, ComplexType value3)
            {
            }
        }

        public class MethodWithDefaultValues
        {
            public void Method(int value1 = 42, string value2 = "hello world")
            {
            }
        }

        public class MethodWithRefParameter
        {
            public void Method(ref string value)
            {
            }
        }

        public class MethodWithOutParameter
        {
            public void Method(out string value)
            {
                value = null;
            }
        }

        public class MethodWithParams
        {
            public void Method(string format, params object[] values)
            {
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedParameter.Global
    // ReSharper restore UnusedMember.Global
}