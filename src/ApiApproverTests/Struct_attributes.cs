using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Struct_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<StructWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.SimpleAttribute()]
    public struct StructWithSimpleAttribute { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<StructWithAttributeWithStringPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
    public struct StructWithAttributeWithStringPositionalParameters { }
}");
            AssertPublicApi<StructWithAttributeWithIntPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
    public struct StructWithAttributeWithIntPositionalParameters { }
}");
            AssertPublicApi<StructWithAttributeWithMultiplePositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
    public struct StructWithAttributeWithMultiplePositionalParameters { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<StructWithIntNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
    public struct StructWithIntNamedParameterAttribute { }
}");

            AssertPublicApi<StructWithStringNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
    public struct StructWithStringNamedParameterAttribute { }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<StructWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
    public struct StructWithAttributeWithMultipleNamedParameters { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<StructWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
    public struct StructWithAttributeWithBothNamedAndPositionalParameters { }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<StructWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)]
    public struct StructWithAttributeWithEnumFlags { }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<StructWithMultipleAttributes>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.Attribute_AA()]
    [ApiApproverTests.Examples.Attribute_MM()]
    [ApiApproverTests.Examples.Attribute_ZZ()]
    public struct StructWithMultipleAttributes { }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            AssertPublicApi<StructWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    public struct StructWithSimpleAttribute { }
}", excludedAttributes: new[] { "ApiApproverTests.Examples.SimpleAttribute" });
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
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
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}