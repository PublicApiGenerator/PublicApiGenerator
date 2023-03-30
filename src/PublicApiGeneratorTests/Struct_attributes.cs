using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Struct_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<StructWithSimpleAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.Simple]
    public struct StructWithSimpleAttribute { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<StructWithAttributeWithStringPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1(""Hello"")]
    public struct StructWithAttributeWithStringPositionalParameters { }
}");
            AssertPublicApi<StructWithAttributeWithIntPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2(42)]
    public struct StructWithAttributeWithIntPositionalParameters { }
}");
            AssertPublicApi<StructWithAttributeWithMultiplePositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParameters(42, ""Hello world"")]
    public struct StructWithAttributeWithMultiplePositionalParameters { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<StructWithIntNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42)]
    public struct StructWithIntNamedParameterAttribute { }
}");

            AssertPublicApi<StructWithStringNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(StringValue=""Hello"")]
    public struct StructWithStringNamedParameterAttribute { }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<StructWithAttributeWithMultipleNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42, StringValue=""Hello world"")]
    public struct StructWithAttributeWithMultipleNamedParameters { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<StructWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedAndPositionalParameter(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
    public struct StructWithAttributeWithBothNamedAndPositionalParameters { }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<StructWithAttributeWithEnumFlags>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithEnumFlags(PublicApiGeneratorTests.Examples.EnumWithFlags.One | PublicApiGeneratorTests.Examples.EnumWithFlags.Two | PublicApiGeneratorTests.Examples.EnumWithFlags.Three)]
    public struct StructWithAttributeWithEnumFlags { }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<StructWithMultipleAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.Attribute_AA]
    [PublicApiGeneratorTests.Examples.Attribute_MM]
    [PublicApiGeneratorTests.Examples.Attribute_ZZ]
    public struct StructWithMultipleAttributes { }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            AssertPublicApi<StructWithSimpleAttribute>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public struct StructWithSimpleAttribute { }
}", opt => opt.ExcludeAttributes = new[] { "PublicApiGeneratorTests.Examples.SimpleAttribute" });
        }
    }

    namespace Examples
    {
        [SimpleAttribute]
        public struct StructWithSimpleAttribute
        {
        }

        [AttributeWithPositionalParameters1("Hello")]
        public struct StructWithAttributeWithStringPositionalParameters
        {
        }

        [AttributeWithPositionalParameters2(42)]
        public struct StructWithAttributeWithIntPositionalParameters
        {
        }

        [AttributeWithMultiplePositionalParameters(42, "Hello world")]
        public struct StructWithAttributeWithMultiplePositionalParameters
        {
        }

        [AttributeWithNamedParameter(IntValue = 42)]
        public struct StructWithIntNamedParameterAttribute
        {
        }

        [AttributeWithNamedParameter(StringValue = "Hello")]
        public struct StructWithStringNamedParameterAttribute
        {
        }

        [AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
        public struct StructWithAttributeWithMultipleNamedParameters
        {
        }

        [AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)]
        public struct StructWithAttributeWithBothNamedAndPositionalParameters
        {
        }

        [AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
        public struct StructWithAttributeWithEnumFlags
        {
        }

        [Attribute_ZZ]
        [Attribute_MM]
        [Attribute_AA]
        public struct StructWithMultipleAttributes
        {
        }
    }
}
