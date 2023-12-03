using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Property_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<PropertyWithSimpleAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithSimpleAttribute
    {
        public PropertyWithSimpleAttribute() { }
        [PublicApiGeneratorTests.Examples.Simple]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<PropertyWithAttributeWithStringPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithAttributeWithStringPositionalParameters
    {
        public PropertyWithAttributeWithStringPositionalParameters() { }
        [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1(""Hello"")]
        public string Value { get; set; }
    }
}");
            AssertPublicApi<PropertyWithAttributeWithIntPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithAttributeWithIntPositionalParameters
    {
        public PropertyWithAttributeWithIntPositionalParameters() { }
        [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2(42)]
        public string Value { get; set; }
    }
}");
            AssertPublicApi<PropertyWithAttributeWithMultiplePositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithAttributeWithMultiplePositionalParameters
    {
        public PropertyWithAttributeWithMultiplePositionalParameters() { }
        [PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParameters(42, ""Hello world"")]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<PropertyWithIntNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithIntNamedParameterAttribute
    {
        public PropertyWithIntNamedParameterAttribute() { }
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42)]
        public string Value { get; set; }
    }
}");

            AssertPublicApi<PropertyWithStringNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithStringNamedParameterAttribute
    {
        public PropertyWithStringNamedParameterAttribute() { }
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(StringValue=""Hello"")]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<PropertyWithAttributeWithMultipleNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithAttributeWithMultipleNamedParameters
    {
        public PropertyWithAttributeWithMultipleNamedParameters() { }
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42, StringValue=""Hello world"")]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<PropertyWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithAttributeWithBothNamedAndPositionalParameters
    {
        public PropertyWithAttributeWithBothNamedAndPositionalParameters() { }
        [PublicApiGeneratorTests.Examples.AttributeWithNamedAndPositionalParameter(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_enum_value()
        {
            AssertPublicApi<PropertyWithAttributeWithSimpleEnum>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithAttributeWithSimpleEnum
    {
        public PropertyWithAttributeWithSimpleEnum() { }
        [PublicApiGeneratorTests.Examples.AttributeWithSimpleEnum(PublicApiGeneratorTests.Examples.SimpleEnum.Blue)]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<PropertyWithAttributeWithEnumFlags>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithAttributeWithEnumFlags
    {
        public PropertyWithAttributeWithEnumFlags() { }
        [PublicApiGeneratorTests.Examples.AttributeWithEnumFlags(PublicApiGeneratorTests.Examples.EnumWithFlags.One | PublicApiGeneratorTests.Examples.EnumWithFlags.Two | PublicApiGeneratorTests.Examples.EnumWithFlags.Three)]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<PropertyWithMultipleAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithMultipleAttributes
    {
        public PropertyWithMultipleAttributes() { }
        [PublicApiGeneratorTests.Examples.Attribute_AA]
        [PublicApiGeneratorTests.Examples.Attribute_MM]
        [PublicApiGeneratorTests.Examples.Attribute_ZZ]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attributes_on_getters_and_setters()
        {
            // Yes, it's a hack, but the CodeDOM doesn't support it. Sigh
            AssertPublicApi<PropertyWithSimpleAttributeOnGetterAndSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithSimpleAttributeOnGetterAndSetter
    {
        public PropertyWithSimpleAttributeOnGetterAndSetter() { }
        [get: PublicApiGeneratorTests.Examples.Simple]
        [set: PublicApiGeneratorTests.Examples.Simple]
        public string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_skip_excluded_attributes()
        {
            AssertPublicApi<PropertyWithSimpleAttributeOnGetterAndSetter>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class PropertyWithSimpleAttributeOnGetterAndSetter
    {
        public PropertyWithSimpleAttributeOnGetterAndSetter() { }
        public string Value { get; set; }
    }
}", opt => opt.ExcludeAttributes = ["PublicApiGeneratorTests.Examples.SimpleAttribute"]);
        }
    }

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
                [Simple] get;
                [Simple] set;
            }
        }
    }
}
