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
    public class ClassWithSimpleAttribute
    {
        public ClassWithSimpleAttribute() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<ClassWithAttributeWithStringPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
    public class ClassWithAttributeWithStringPositionalParameters
    {
        public ClassWithAttributeWithStringPositionalParameters() { }
    }
}");
            AssertPublicApi<ClassWithAttributeWithIntPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
    public class ClassWithAttributeWithIntPositionalParameters
    {
        public ClassWithAttributeWithIntPositionalParameters() { }
    }
}");
            AssertPublicApi<ClassWithAttributeWithMultiplePositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
    public class ClassWithAttributeWithMultiplePositionalParameters
    {
        public ClassWithAttributeWithMultiplePositionalParameters() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<ClassWithIntNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
    public class ClassWithIntNamedParameterAttribute
    {
        public ClassWithIntNamedParameterAttribute() { }
    }
}");

            AssertPublicApi<ClassWithStringNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
    public class ClassWithStringNamedParameterAttribute
    {
        public ClassWithStringNamedParameterAttribute() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<ClassWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
    public class ClassWithAttributeWithMultipleNamedParameters
    {
        public ClassWithAttributeWithMultipleNamedParameters() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_fields()
        {
            AssertPublicApi<ClassWithIntNamedFieldAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedFieldAttribute(IntValue=42)]
    public class ClassWithIntNamedFieldAttribute
    {
        public ClassWithIntNamedFieldAttribute() { }
    }
}");

            AssertPublicApi<ClassWithStringNamedFieldAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedFieldAttribute(StringValue=""Hello"")]
    public class ClassWithStringNamedFieldAttribute
    {
        public ClassWithStringNamedFieldAttribute() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_fields_in_alphabetical_order()
        {
            AssertPublicApi<ClassWithAttributeWithMultipleNamedFields>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedFieldAttribute(IntValue=42, StringValue=""Hello world"")]
    public class ClassWithAttributeWithMultipleNamedFields
    {
        public ClassWithAttributeWithMultipleNamedFields() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<ClassWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello"", IntValue=13, StringValue=""World"")]
    public class ClassWithAttributeWithBothNamedAndPositionalParameters
    {
        public ClassWithAttributeWithBothNamedAndPositionalParameters() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_fields_and_parameters()
        {
            AssertPublicApi<ClassWithAttributeWithBothNamedParametersAndFields>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAndFieldAttribute(IntField=42, StringField=""Hello"", IntProperty=13, StringProperty=""World"")]
    public class ClassWithAttributeWithBothNamedParametersAndFields
    {
        public ClassWithAttributeWithBothNamedParametersAndFields() { }
    }
}");
        }

        [Fact]
        public void Should_output_enum_value()
        {
            AssertPublicApi<ClassWithAttributeWithSimpleEnum>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithSimpleEnumAttribute(ApiApproverTests.Examples.SimpleEnum.Blue)]
    public class ClassWithAttributeWithSimpleEnum
    {
        public ClassWithAttributeWithSimpleEnum() { }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<ClassWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)]
    public class ClassWithAttributeWithEnumFlags
    {
        public ClassWithAttributeWithEnumFlags() { }
    }
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
    public class ClassWithMultipleAttributes
    {
        public ClassWithMultipleAttributes() { }
    }
}");
        }

        [Fact]
        public void Should_handle_attribute_with_multiple_usages_support()
        {
            AssertPublicApi<ClassWithAttributeWithMultipleUsagesSupport>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithMultipleUsagesSupport(IntValue=0, StringValue=""ZZZ"")]
    [ApiApproverTests.Examples.AttributeWithMultipleUsagesSupport(IntValue=1, StringValue=""MMM"")]
    [ApiApproverTests.Examples.AttributeWithMultipleUsagesSupport(IntValue=2, StringValue=""AAA"")]
    public class ClassWithAttributeWithMultipleUsagesSupport { }
}");
        }

        [Fact]
        public void Should_handle_attribute_with_object_initialiser()
        {
            AssertPublicApi<ClassWithAttributeWithObjectInitialiser>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithObjectInitialiser(""Hello world"")]
    public class ClassWithAttributeWithObjectInitialiser
    {
        public ClassWithAttributeWithObjectInitialiser() { }
    }
}");
        }

        [Fact]
        public void Should_handle_attribute_with_object_array_initialiser()
        {
            AssertPublicApi<ClassWithAttributeWithObjectArrayInitialiser>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithObjectArrayInitialiser(new object[] {
            42,
            ""Hello world""})]
    public class ClassWithAttributeWithObjectArrayInitialiser
    {
        public ClassWithAttributeWithObjectArrayInitialiser() { }
    }
}");
        }

        [Fact]
        public void Should_handle_attribute_with_string_array_initialiser()
        {
            AssertPublicApi<ClassWithAttributeWithStringArrayInitialiser>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithStringArrayInitialiser(new string[] {
            ""Hello"",
            ""world""})]
    public class ClassWithAttributeWithStringArrayInitialiser
    {
        public ClassWithAttributeWithStringArrayInitialiser() { }
    }
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

        [AttributeWithNamedField(IntValue = 42)]
        public class ClassWithIntNamedFieldAttribute
        {
        }

        [AttributeWithNamedParameter(StringValue = "Hello")]
        public class ClassWithStringNamedParameterAttribute
        {
        }

        [AttributeWithNamedField(StringValue = "Hello")]
        public class ClassWithStringNamedFieldAttribute
        {
        }

        [AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
        public class ClassWithAttributeWithMultipleNamedParameters
        {
        }

        [AttributeWithNamedField(StringValue = "Hello world", IntValue = 42)]
        public class ClassWithAttributeWithMultipleNamedFields
        {
        }

        [AttributeWithNamedAndPositionalParameter(42, "Hello", StringValue = "World", IntValue = 13)]
        public class ClassWithAttributeWithBothNamedAndPositionalParameters
        {
        }

        [AttributeWithNamedParameterAndFieldAttribute(IntField = 42, StringField = "Hello", StringProperty = "World", IntProperty = 13)]
        public class ClassWithAttributeWithBothNamedParametersAndFields
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

        [AttributeWithObjectInitialiser("Hello world")]
        public class ClassWithAttributeWithObjectInitialiser
        {
        }

        [AttributeWithObjectArrayInitialiser(42, "Hello world")]
        public class ClassWithAttributeWithObjectArrayInitialiser
        {
        }

        [AttributeWithStringArrayInitialiser("Hello", "world")]
        public class ClassWithAttributeWithStringArrayInitialiser
        {
        }

        [AttributeWithMultipleUsagesSupport(IntValue = 0, StringValue = "ZZZ")]
        [AttributeWithMultipleUsagesSupport(IntValue = 1, StringValue = "MMM")]
        [AttributeWithMultipleUsagesSupport(IntValue = 2, StringValue = "AAA")]
        public class ClassWithAttributeWithMultipleUsagesSupport
        {
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}