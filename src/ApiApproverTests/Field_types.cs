using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Field_types : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_use_fully_qualified_type_name()
        {
            AssertPublicApi<FieldWithComplexType>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithComplexType
    {
        public ApiApproverTests.Examples.ComplexType Field;
    }
}");
        }

        [Fact]
        public void Should_output_generic_parameters()
        {
            AssertPublicApi<FieldWithGenericType>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithGenericType
    {
        public ApiApproverTests.Examples.GenericType<int> Field;
    }
}");
        }

        [Fact]
        public void Should_use_fully_qualified_type_name_for_generic_parameters()
        {
            AssertPublicApi<FieldWithGenericComplexType>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithGenericComplexType
    {
        public ApiApproverTests.Examples.GenericType<ApiApproverTests.Examples.ComplexType> Field;
    }
}");
        }

        [Fact]
        public void Should_output_generic_parameters_with_generic_parameters()
        {
            AssertPublicApi<FieldWithGenericTypeParametersOfGenericTypeParameters>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithGenericTypeParametersOfGenericTypeParameters
    {
        public ApiApproverTests.Examples.GenericType<ApiApproverTests.Examples.GenericType<ApiApproverTests.Examples.ComplexType>> Field;
    }
}");
        }

        [Fact]
        public void Should_output_multiple_generic_parameters()
        {
            AssertPublicApi<FieldWithMultipleGenericTypeParameters>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithMultipleGenericTypeParameters
    {
        public ApiApproverTests.Examples.GenericTypeExtra<int, string, ApiApproverTests.Examples.ComplexType> Field;
    }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public class FieldWithComplexType
        {
            public ComplexType Field;
        }

        public class FieldWithGenericType
        {
            public GenericType<int> Field;
        }

        public class FieldWithGenericComplexType
        {
            public GenericType<ComplexType> Field;
        }

        public class FieldWithGenericTypeParametersOfGenericTypeParameters
        {
            public GenericType<GenericType<ComplexType>> Field;
        }

        public class FieldWithMultipleGenericTypeParameters
        {
            public GenericTypeExtra<int, string, ComplexType> Field;
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}