using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Interface_property_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<IPropertyWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithSimpleAttribute
    {
        [ApiApproverTests.Examples.SimpleAttribute()]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<IPropertyWithAttributeWithStringPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithAttributeWithStringPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
        string Value { get; set; }
    }
}");
            AssertPublicApi<IPropertyWithAttributeWithIntPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithAttributeWithIntPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
        string Value { get; set; }
    }
}");
            AssertPublicApi<IPropertyWithAttributeWithMultiplePositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithAttributeWithMultiplePositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<IPropertyWithIntNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithIntNamedParameterAttribute
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
        string Value { get; set; }
    }
}");

            AssertPublicApi<IPropertyWithStringNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithStringNamedParameterAttribute
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<IPropertyWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithAttributeWithMultipleNamedParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<IPropertyWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithAttributeWithBothNamedAndPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_enum_value()
        {
            AssertPublicApi<IPropertyWithAttributeWithSimpleEnum>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithAttributeWithSimpleEnum
    {
        [ApiApproverTests.Examples.AttributeWithSimpleEnumAttribute(ApiApproverTests.Examples.SimpleEnum.Blue)]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<IPropertyWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithAttributeWithEnumFlags
    {
        [ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<IPropertyWithMultipleAttributes>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithMultipleAttributes
    {
        [ApiApproverTests.Examples.Attribute_AA()]
        [ApiApproverTests.Examples.Attribute_MM()]
        [ApiApproverTests.Examples.Attribute_ZZ()]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attributes_on_getters_and_setters()
        {
            // Yes, it's a hack, but the CodeDOM doesn't support it. Sigh
            AssertPublicApi<IPropertyWithSimpleAttributeOnGetterAndSetter>(
@"namespace ApiApproverTests.Examples
{
    public interface IPropertyWithSimpleAttributeOnGetterAndSetter
    {
        [get: ApiApproverTests.Examples.SimpleAttribute()]
        [set: ApiApproverTests.Examples.SimpleAttribute()]
        string Value { get; set; }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public interface IPropertyWithSimpleAttribute
        {
            [SimpleAttribute]
            string Value { get; set; }
        }

        public interface IPropertyWithAttributeWithStringPositionalParameters
        {
            [AttributeWithPositionalParameters1("Hello")]
            string Value { get; set; }
        }

        public interface IPropertyWithAttributeWithIntPositionalParameters
        {
            [AttributeWithPositionalParameters2(42)]
            string Value { get; set; }
        }

        public interface IPropertyWithAttributeWithMultiplePositionalParameters
        {
            [AttributeWithMultiplePositionalParameters(42, "Hello world")]
            string Value { get; set; }
        }

        public interface IPropertyWithIntNamedParameterAttribute
        {
            [AttributeWithNamedParameter(IntValue = 42)]
            string Value { get; set; }
        }

        public interface IPropertyWithStringNamedParameterAttribute
        {
            [AttributeWithNamedParameter(StringValue = "Hello")]
            string Value { get; set; }
        }

        public interface IPropertyWithAttributeWithMultipleNamedParameters
        {
            [AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
            string Value { get; set; }
        }

        public interface IPropertyWithAttributeWithBothNamedAndPositionalParameters
        {
            [AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)]
            string Value { get; set; }
        }

        public interface IPropertyWithAttributeWithSimpleEnum
        {
            [AttributeWithSimpleEnum(SimpleEnum.Blue)]
            string Value { get; set; }
        }

        public interface IPropertyWithAttributeWithEnumFlags
        {
            [AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
            string Value { get; set; }
        }

        public interface IPropertyWithMultipleAttributes
        {
            [Attribute_ZZ]
            [Attribute_MM]
            [Attribute_AA]
            string Value { get; set; }
        }

        public interface IPropertyWithSimpleAttributeOnGetterAndSetter
        {
            string Value
            {
                [SimpleAttribute]
                get;
                [SimpleAttribute]
                set;
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}