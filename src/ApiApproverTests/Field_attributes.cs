using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Field_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<FieldWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithSimpleAttribute
    {
        [ApiApproverTests.Examples.SimpleAttribute()]
        public string Value;
        public FieldWithSimpleAttribute() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<FieldWithAttributeWithStringPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithAttributeWithStringPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
        public string Value;
        public FieldWithAttributeWithStringPositionalParameters() { }
    }
}");
            AssertPublicApi<FieldWithAttributeWithIntPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithAttributeWithIntPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
        public string Value;
        public FieldWithAttributeWithIntPositionalParameters() { }
    }
}");
            AssertPublicApi<FieldWithAttributeWithMultiplePositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithAttributeWithMultiplePositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
        public string Value;
        public FieldWithAttributeWithMultiplePositionalParameters() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<FieldWithIntNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithIntNamedParameterAttribute
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
        public string Value;
        public FieldWithIntNamedParameterAttribute() { }
    }
}");

            AssertPublicApi<FieldWithStringNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithStringNamedParameterAttribute
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
        public string Value;
        public FieldWithStringNamedParameterAttribute() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<FieldWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithAttributeWithMultipleNamedParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
        public string Value;
        public FieldWithAttributeWithMultipleNamedParameters() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<FieldWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithAttributeWithBothNamedAndPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        public string Value;
        public FieldWithAttributeWithBothNamedAndPositionalParameters() { }
    }
}");
        }

        [Fact]
        public void Should_output_enum_value()
        {
            AssertPublicApi<FieldWithAttributeWithSimpleEnum>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithAttributeWithSimpleEnum
    {
        [ApiApproverTests.Examples.AttributeWithSimpleEnumAttribute(ApiApproverTests.Examples.SimpleEnum.Blue)]
        public string Value;
        public FieldWithAttributeWithSimpleEnum() { }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<FieldWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithAttributeWithEnumFlags
    {
        [ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)]
        public string Value;
        public FieldWithAttributeWithEnumFlags() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<FieldWithMultipleAttributes>(
@"namespace ApiApproverTests.Examples
{
    public class FieldWithMultipleAttributes
    {
        [ApiApproverTests.Examples.Attribute_AA()]
        [ApiApproverTests.Examples.Attribute_MM()]
        [ApiApproverTests.Examples.Attribute_ZZ()]
        public string Value;
        public FieldWithMultipleAttributes() { }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class FieldWithSimpleAttribute
        {
            [SimpleAttribute]
            public string Value;
        }

        public class FieldWithAttributeWithStringPositionalParameters
        {
            [AttributeWithPositionalParameters1("Hello")]
            public string Value;
        }

        public class FieldWithAttributeWithIntPositionalParameters
        {
            [AttributeWithPositionalParameters2(42)]
            public string Value;
        }

        public class FieldWithAttributeWithMultiplePositionalParameters
        {
            [AttributeWithMultiplePositionalParameters(42, "Hello world")]
            public string Value;
        }

        public class FieldWithIntNamedParameterAttribute
        {
            [AttributeWithNamedParameter(IntValue = 42)]
            public string Value;
        }

        public class FieldWithStringNamedParameterAttribute
        {
            [AttributeWithNamedParameter(StringValue = "Hello")]
            public string Value;
        }

        public class FieldWithAttributeWithMultipleNamedParameters
        {
            [AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
            public string Value;
        }

        public class FieldWithAttributeWithBothNamedAndPositionalParameters
        {
            [AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)]
            public string Value;
        }

        public class FieldWithAttributeWithSimpleEnum
        {
            [AttributeWithSimpleEnum(SimpleEnum.Blue)]
            public string Value;
        }

        public class FieldWithAttributeWithEnumFlags
        {
            [AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
            public string Value;
        }

        public class FieldWithMultipleAttributes
        {
            [Attribute_ZZ] [Attribute_MM] [Attribute_AA] public string Value;
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}