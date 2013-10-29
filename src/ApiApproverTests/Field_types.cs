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
        public ApiApproverTests.Examples.ComplexTypeForField Field;
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
        public ApiApproverTests.Examples.GenericTypeForField<int> Field;
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
        public ApiApproverTests.Examples.GenericTypeForField<ApiApproverTests.Examples.ComplexTypeForField> Field;
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
        public ApiApproverTests.Examples.GenericTypeForField<ApiApproverTests.Examples.GenericTypeForField<ApiApproverTests.Examples.ComplexTypeForField>> Field;
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
        public ApiApproverTests.Examples.OtherGenericTypeForField<int, string, ApiApproverTests.Examples.ComplexTypeForField> Field;
    }
}");
        }
    }

    namespace Examples
    {
        #region helper types
        public class ComplexTypeForField
        {
        }

        public class GenericTypeForField<T>
        {
        }

        public class OtherGenericTypeForField<T, T2, T3>
        {
        }
        #endregion

        public class FieldWithComplexType
        {
            public ComplexTypeForField Field;
        }

        public class FieldWithGenericType
        {
            public GenericTypeForField<int> Field;
        }

        public class FieldWithGenericComplexType
        {
            public GenericTypeForField<ComplexTypeForField> Field;
        }

        public class FieldWithGenericTypeParametersOfGenericTypeParameters
        {
            public GenericTypeForField<GenericTypeForField<ComplexTypeForField>> Field;
        }

        public class FieldWithMultipleGenericTypeParameters
        {
            public OtherGenericTypeForField<int, string, ComplexTypeForField> Field;
        }
    }
}