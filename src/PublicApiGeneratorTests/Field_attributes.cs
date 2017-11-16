using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Field_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<FieldWithSimpleAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithSimpleAttribute
    {
        [PublicApiGeneratorTests.Examples.SimpleAttribute()]
        public string Value;
        public FieldWithSimpleAttribute() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<FieldWithAttributeWithStringPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithAttributeWithStringPositionalParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
        public string Value;
        public FieldWithAttributeWithStringPositionalParameters() { }
    }
}");
            AssertPublicApi<FieldWithAttributeWithIntPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithAttributeWithIntPositionalParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
        public string Value;
        public FieldWithAttributeWithIntPositionalParameters() { }
    }
}");
            AssertPublicApi<FieldWithAttributeWithMultiplePositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithAttributeWithMultiplePositionalParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
        public string Value;
        public FieldWithAttributeWithMultiplePositionalParameters() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<FieldWithIntNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithIntNamedParameterAttribute
    {
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
        public string Value;
        public FieldWithIntNamedParameterAttribute() { }
    }
}");

            AssertPublicApi<FieldWithStringNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithStringNamedParameterAttribute
    {
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
        public string Value;
        public FieldWithStringNamedParameterAttribute() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<FieldWithAttributeWithMultipleNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithAttributeWithMultipleNamedParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
        public string Value;
        public FieldWithAttributeWithMultipleNamedParameters() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<FieldWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithAttributeWithBothNamedAndPositionalParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        public string Value;
        public FieldWithAttributeWithBothNamedAndPositionalParameters() { }
    }
}");
        }

        [Fact]
        public void Should_output_enum_value()
        {
            AssertPublicApi<FieldWithAttributeWithSimpleEnum>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithAttributeWithSimpleEnum
    {
        [PublicApiGeneratorTests.Examples.AttributeWithSimpleEnumAttribute(PublicApiGeneratorTests.Examples.SimpleEnum.Blue)]
        public string Value;
        public FieldWithAttributeWithSimpleEnum() { }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<FieldWithAttributeWithEnumFlags>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithAttributeWithEnumFlags
    {
        [PublicApiGeneratorTests.Examples.AttributeWithEnumFlagsAttribute(PublicApiGeneratorTests.Examples.EnumWithFlags.One | PublicApiGeneratorTests.Examples.EnumWithFlags.Two | PublicApiGeneratorTests.Examples.EnumWithFlags.Three)]
        public string Value;
        public FieldWithAttributeWithEnumFlags() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<FieldWithMultipleAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class FieldWithMultipleAttributes
    {
        [PublicApiGeneratorTests.Examples.Attribute_AA()]
        [PublicApiGeneratorTests.Examples.Attribute_MM()]
        [PublicApiGeneratorTests.Examples.Attribute_ZZ()]
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