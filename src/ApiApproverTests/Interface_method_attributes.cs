using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Interface_method_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<IMethodWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodWithSimpleAttribute
    {
        [ApiApproverTests.Examples.SimpleAttribute()]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<IMethodsWithAttributeWithPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodsWithAttributeWithPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
        void Method1();
        [ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
        void Method2();
        [ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
        void Method3();
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<IMethodsWithAttributeWithNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodsWithAttributeWithNamedParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
        void Method1();
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
        void Method2();
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<IMethodWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodWithAttributeWithMultipleNamedParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<IMethodWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodWithAttributeWithBothNamedAndPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<IMethodWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodWithAttributeWithEnumFlags
    {
        [ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_handle_typeof_argument()
        {
            AssertPublicApi<IMethodWithAttributeWithType>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodWithAttributeWithType
    {
        [ApiApproverTests.Examples.AttributeWithTypeParameterAttribute(typeof(string))]
        void Method1();
        [ApiApproverTests.Examples.AttributeWithTypeParameterAttribute(typeof(ApiApproverTests.Examples.ComplexType))]
        void Method2();
        [ApiApproverTests.Examples.AttributeWithTypeParameterAttribute(typeof(ApiApproverTests.Examples.GenericType<ApiApproverTests.Examples.ComplexType>))]
        void Method3();
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<IMethodWithMultipleAttributes>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodWithMultipleAttributes
    {
        [ApiApproverTests.Examples.Attribute_AA()]
        [ApiApproverTests.Examples.Attribute_MM()]
        [ApiApproverTests.Examples.Attribute_ZZ()]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            AssertPublicApi<IMethodsWithAttributeWithNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IMethodsWithAttributeWithNamedParameters
    {
        void Method1();
        void Method2();
    }
}", excludedAttributes: new[] { "ApiApproverTests.Examples.AttributeWithNamedParameterAttribute" });
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public interface IMethodWithSimpleAttribute
        {
            [SimpleAttribute]
            void Method();
        }

        public interface IMethodsWithAttributeWithPositionalParameters
        {
            [AttributeWithPositionalParameters1("Hello")]
            void Method1();

            [AttributeWithPositionalParameters2(42)]
            void Method2();

            [AttributeWithMultiplePositionalParameters(42, "Hello world")]
            void Method3();
        }

        public interface IMethodsWithAttributeWithNamedParameters
        {
            [AttributeWithNamedParameter(StringValue = "Hello")]
            void Method1();

            [AttributeWithNamedParameter(IntValue = 42)]
            void Method2();
        }

        public interface IMethodWithAttributeWithMultipleNamedParameters
        {
            [AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
            void Method();
        }

        public interface IMethodWithAttributeWithBothNamedAndPositionalParameters
        {
            [AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)]
            void Method();
        }

        public interface IMethodWithAttributeWithEnumFlags
        {
            [AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
            void Method();
        }

        public interface IMethodWithAttributeWithType
        {
            [AttributeWithTypeParameterAttribute(typeof(string))]
            void Method1();
            [AttributeWithTypeParameterAttribute(typeof(ComplexType))]
            void Method2();
            [AttributeWithTypeParameterAttribute(typeof(GenericType<ComplexType>))]
            void Method3();
        }

        public interface IMethodWithMultipleAttributes
        {
            [Attribute_ZZ]
            [Attribute_MM]
            [Attribute_AA]
            void Method();
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}