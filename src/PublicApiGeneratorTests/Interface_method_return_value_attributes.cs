using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Interface_method_return_value_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<IMethodReturnValueWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodReturnValueWithSimpleAttribute
    {
        [return: ApiApproverTests.Examples.SimpleAttribute()]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<IMethodReturnValuesWithAttributeWithPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodReturnValuesWithAttributeWithPositionalParameters
    {
        [return: ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
        void Method1();
        [return: ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
        void Method2();
        [return: ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
        void Method3();
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<IMethodReturnValuesWithAttributeWithNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodReturnValuesWithAttributeWithNamedParameters
    {
        [return: ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
        void Method1();
        [return: ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
        void Method2();
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<IMethodReturnValueWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodReturnValueWithAttributeWithMultipleNamedParameters
    {
        [return: ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<IMethodReturnValueWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodReturnValueWithAttributeWithBothNamedAndPositionalParameters
    {
        [return: ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<IMethodReturnValueWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodReturnValueWithAttributeWithEnumFlags
    {
        [return: ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<IMethodReturnValueWithMultipleAttributes>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodReturnValueWithMultipleAttributes
    {
        [return: ApiApproverTests.Examples.Attribute_AA()]
        [return: ApiApproverTests.Examples.Attribute_MM()]
        [return: ApiApproverTests.Examples.Attribute_ZZ()]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_order_return_and_method_attributes_correctly()
        {
            AssertPublicApi<IMethodWithAttributesOnMethodAndReturnValue>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodWithAttributesOnMethodAndReturnValue
    {
        [ApiApproverTests.Examples.SimpleAttribute()]
        [return: ApiApproverTests.Examples.SimpleAttribute()]
        void Method();
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public interface IMethodReturnValueWithSimpleAttribute
        {
            [return: SimpleAttribute]
            void Method();
        }

        public interface IMethodReturnValuesWithAttributeWithPositionalParameters
        {
            [return: AttributeWithPositionalParameters1("Hello")]
            void Method1();

            [return: AttributeWithPositionalParameters2(42)]
            void Method2();

            [return: AttributeWithMultiplePositionalParameters(42, "Hello world")]
            void Method3();
        }

        public interface IMethodReturnValuesWithAttributeWithNamedParameters
        {
            [return: AttributeWithNamedParameter(StringValue = "Hello")]
            void Method1();

            [return: AttributeWithNamedParameter(IntValue = 42)]
            void Method2();
        }

        public interface IMethodReturnValueWithAttributeWithMultipleNamedParameters
        {
            [return: AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
            void Method();
        }

        public interface IMethodReturnValueWithAttributeWithBothNamedAndPositionalParameters
        {
            [return: AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)]
            void Method();
        }

        public interface IMethodReturnValueWithAttributeWithEnumFlags
        {
            [return: AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
            void Method();
        }

        public interface IMethodReturnValueWithMultipleAttributes
        {
            [return: Attribute_ZZ]
            [return: Attribute_MM]
            [return: Attribute_AA]
            void Method();
        }

        public interface IMethodWithAttributesOnMethodAndReturnValue
        {
            [SimpleAttribute]
            [return: SimpleAttribute]
            void Method();
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}