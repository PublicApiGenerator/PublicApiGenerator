using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Interface_property_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<IPropertyWithSimpleAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithSimpleAttribute
    {
        [PublicApiGeneratorTests.Examples.Simple]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<IPropertyWithAttributeWithStringPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithAttributeWithStringPositionalParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1(""Hello"")]
        string Value { get; set; }
    }
}");
            AssertPublicApi<IPropertyWithAttributeWithIntPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithAttributeWithIntPositionalParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2(42)]
        string Value { get; set; }
    }
}");
            AssertPublicApi<IPropertyWithAttributeWithMultiplePositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithAttributeWithMultiplePositionalParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParameters(42, ""Hello world"")]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<IPropertyWithIntNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithIntNamedParameterAttribute
    {
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42)]
        string Value { get; set; }
    }
}");

            AssertPublicApi<IPropertyWithStringNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithStringNamedParameterAttribute
    {
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(StringValue=""Hello"")]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<IPropertyWithAttributeWithMultipleNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithAttributeWithMultipleNamedParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42, StringValue=""Hello world"")]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<IPropertyWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithAttributeWithBothNamedAndPositionalParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithNamedAndPositionalParameter(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_enum_value()
        {
            AssertPublicApi<IPropertyWithAttributeWithSimpleEnum>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithAttributeWithSimpleEnum
    {
        [PublicApiGeneratorTests.Examples.AttributeWithSimpleEnum(PublicApiGeneratorTests.Examples.SimpleEnum.Blue)]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<IPropertyWithAttributeWithEnumFlags>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithAttributeWithEnumFlags
    {
        [PublicApiGeneratorTests.Examples.AttributeWithEnumFlags(PublicApiGeneratorTests.Examples.EnumWithFlags.One | PublicApiGeneratorTests.Examples.EnumWithFlags.Two | PublicApiGeneratorTests.Examples.EnumWithFlags.Three)]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<IPropertyWithMultipleAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithMultipleAttributes
    {
        [PublicApiGeneratorTests.Examples.Attribute_AA]
        [PublicApiGeneratorTests.Examples.Attribute_MM]
        [PublicApiGeneratorTests.Examples.Attribute_ZZ]
        string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_add_attributes_on_getters_and_setters()
        {
            // Yes, it's a hack, but the CodeDOM doesn't support it. Sigh
            AssertPublicApi<IPropertyWithSimpleAttributeOnGetterAndSetter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IPropertyWithSimpleAttributeOnGetterAndSetter
    {
        [get: PublicApiGeneratorTests.Examples.Simple]
        [set: PublicApiGeneratorTests.Examples.Simple]
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
