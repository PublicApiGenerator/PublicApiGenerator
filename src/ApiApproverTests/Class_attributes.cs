using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Class_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<ClassWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.SimpleAttribute()]
    public class ClassWithSimpleAttribute { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<ClassWithAttributeWithStringPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
    public class ClassWithAttributeWithStringPositionalParameters { }
}");
            AssertPublicApi<ClassWithAttributeWithIntPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
    public class ClassWithAttributeWithIntPositionalParameters { }
}");
            AssertPublicApi<ClassWithAttributeWithMultiplePositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
    public class ClassWithAttributeWithMultiplePositionalParameters { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<ClassWithIntNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
    public class ClassWithIntNamedParameterAttribute { }
}");

            AssertPublicApi<ClassWithStringNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
    public class ClassWithStringNamedParameterAttribute { }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<ClassWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
    public class ClassWithAttributeWithMultipleNamedParameters { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<ClassWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
    public class ClassWithAttributeWithBothNamedAndPositionalParameters { }
}");
        }

        [Fact]
        public void Should_output_enum_value()
        {
            AssertPublicApi<ClassWithAttributeWithSimpleEnum>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithSimpleEnumAttribute(ApiApproverTests.Examples.SimpleEnum.Blue)]
    public class ClassWithAttributeWithSimpleEnum { }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<ClassWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)]
    public class ClassWithAttributeWithEnumFlags { }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<ClassWithMultipleAttributes>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.Attribute_AA()]
    [ApiApproverTests.Examples.Attribute_MM()]
    [ApiApproverTests.Examples.Attribute_ZZ()]
    public class ClassWithMultipleAttributes { }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        [SimpleAttribute]
        public class ClassWithSimpleAttribute
        {
        }

        [AttributeWithPositionalParameters1("Hello")]
        public class ClassWithAttributeWithStringPositionalParameters
        {
        }

        [AttributeWithPositionalParameters2(42)]
        public class ClassWithAttributeWithIntPositionalParameters
        {
        }

        [AttributeWithMultiplePositionalParameters(42, "Hello world")]
        public class ClassWithAttributeWithMultiplePositionalParameters
        {
        }

        [AttributeWithNamedParameter(IntValue = 42)]
        public class ClassWithIntNamedParameterAttribute
        {
        }

        [AttributeWithNamedParameter(StringValue = "Hello")]
        public class ClassWithStringNamedParameterAttribute
        {
        }

        [AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
        public class ClassWithAttributeWithMultipleNamedParameters
        {
        }

        [AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)]
        public class ClassWithAttributeWithBothNamedAndPositionalParameters
        {
        }

        [AttributeWithSimpleEnum(SimpleEnum.Blue)]
        public class ClassWithAttributeWithSimpleEnum
        {
        }

        [AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
        public class ClassWithAttributeWithEnumFlags
        {
        }

        [Attribute_ZZ]
        [Attribute_MM]
        [Attribute_AA]
        public class ClassWithMultipleAttributes
        {
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}