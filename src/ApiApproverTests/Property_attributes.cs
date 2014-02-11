using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Property_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<PropertyWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithSimpleAttribute
    {
        [ApiApproverTests.Examples.SimpleAttribute()]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<PropertyWithAttributeWithStringPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithAttributeWithStringPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
        public string Value { get; set; }
    }
}");
            AssertPublicApi<PropertyWithAttributeWithIntPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithAttributeWithIntPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
        public string Value { get; set; }
    }
}");
            AssertPublicApi<PropertyWithAttributeWithMultiplePositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithAttributeWithMultiplePositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<PropertyWithIntNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithIntNamedParameterAttribute
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
        public string Value { get; set; }
    }
}");

            AssertPublicApi<PropertyWithStringNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithStringNamedParameterAttribute
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<PropertyWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithAttributeWithMultipleNamedParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<PropertyWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithAttributeWithBothNamedAndPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_enum_value()
        {
            AssertPublicApi<PropertyWithAttributeWithSimpleEnum>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithAttributeWithSimpleEnum
    {
        [ApiApproverTests.Examples.AttributeWithSimpleEnumAttribute(ApiApproverTests.Examples.SimpleEnum.Blue)]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<PropertyWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithAttributeWithEnumFlags
    {
        [ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<PropertyWithMultipleAttributes>(
@"namespace ApiApproverTests.Examples
{
    public class PropertyWithMultipleAttributes
    {
        [ApiApproverTests.Examples.Attribute_AA()]
        [ApiApproverTests.Examples.Attribute_MM()]
        [ApiApproverTests.Examples.Attribute_ZZ()]
        public string Value { get; set; }
    }
}");
        }

//        [Fact]
//        public void Should_add_attributes_on_getters_and_setters()
//        {
//            AssertPublicApi<PropertyWithSimpleAttributeOnGetterAndSetter>(
//@"namespace ApiApproverTests.Examples
//{
//    public class PropertyWithSimpleAttributeOnGetterAndSetter
//    {
//        public string Value { [return: SimpleAttribute] get; [return: SimpleAttribute] set; }
//    }
//}");
//        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class PropertyWithSimpleAttribute
        {
            [SimpleAttribute]
            public string Value { get; set; }
        }

        public class PropertyWithAttributeWithStringPositionalParameters
        {
            [AttributeWithPositionalParameters1("Hello")]
            public string Value { get; set; }
        }

        public class PropertyWithAttributeWithIntPositionalParameters
        {
            [AttributeWithPositionalParameters2(42)]
            public string Value { get; set; }
        }

        public class PropertyWithAttributeWithMultiplePositionalParameters
        {
            [AttributeWithMultiplePositionalParameters(42, "Hello world")]
            public string Value { get; set; }
        }

        public class PropertyWithIntNamedParameterAttribute
        {
            [AttributeWithNamedParameter(IntValue = 42)]
            public string Value { get; set; }
        }

        public class PropertyWithStringNamedParameterAttribute
        {
            [AttributeWithNamedParameter(StringValue = "Hello")]
            public string Value { get; set; }
        }

        public class PropertyWithAttributeWithMultipleNamedParameters
        {
            [AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
            public string Value { get; set; }
        }

        public class PropertyWithAttributeWithBothNamedAndPositionalParameters
        {
            [AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)]
            public string Value { get; set; }
        }

        public class PropertyWithAttributeWithSimpleEnum
        {
            [AttributeWithSimpleEnum(SimpleEnum.Blue)]
            public string Value { get; set; }
        }

        public class PropertyWithAttributeWithEnumFlags
        {
            [AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
            public string Value { get; set; }
        }

        public class PropertyWithMultipleAttributes
        {
            [Attribute_ZZ]
            [Attribute_MM]
            [Attribute_AA]
            public string Value { get; set; }
        }

        public class PropertyWithSimpleAttributeOnGetterAndSetter
        {
            public string Value
            {
                [SimpleAttribute] get; 
                [SimpleAttribute] set;
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}