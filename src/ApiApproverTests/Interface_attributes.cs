using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Interface_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<IInterfaceWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.SimpleAttribute()]
    public interface IInterfaceWithSimpleAttribute { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<IInterfaceWithAttributeWithStringPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
    public interface IInterfaceWithAttributeWithStringPositionalParameters { }
}");
            AssertPublicApi<IInterfaceWithAttributeWithIntPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
    public interface IInterfaceWithAttributeWithIntPositionalParameters { }
}");
            AssertPublicApi<IInterfaceWithAttributeWithMultiplePositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
    public interface IInterfaceWithAttributeWithMultiplePositionalParameters { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<IInterfaceWithIntNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
    public interface IInterfaceWithIntNamedParameterAttribute { }
}");

            AssertPublicApi<IInterfaceWithStringNamedParameterAttribute>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
    public interface IInterfaceWithStringNamedParameterAttribute { }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<IInterfaceWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
    public interface IInterfaceWithAttributeWithMultipleNamedParameters { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<IInterfaceWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
    public interface IInterfaceWithAttributeWithBothNamedAndPositionalParameters { }
}");
        }

        [Fact]
        public void Should_output_enum_value()
        {
            AssertPublicApi<IInterfaceWithAttributeWithSimpleEnum>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithSimpleEnumAttribute(ApiApproverTests.Examples.SimpleEnum.Blue)]
    public interface IInterfaceWithAttributeWithSimpleEnum { }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<IInterfaceWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)]
    public interface IInterfaceWithAttributeWithEnumFlags { }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<IInterfaceWithMultipleAttributes>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.Attribute_AA()]
    [ApiApproverTests.Examples.Attribute_MM()]
    [ApiApproverTests.Examples.Attribute_ZZ()]
    public interface IInterfaceWithMultipleAttributes { }
}");
        }

        [Fact]
        public void Should_handle_attribute_with_object_initialiser()
        {
            AssertPublicApi<IInterfaceWithAttributeWithObjectInitialiser>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithObjectInitialiser(""Hello world"")]
    public interface IInterfaceWithAttributeWithObjectInitialiser { }
}");
        }

        [Fact]
        public void Should_handle_attribute_with_object_array_initialiser()
        {
            AssertPublicApi<IInterfaceWithAttributeWithObjectArrayInitialiser>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithObjectArrayInitialiser(new object[] {
            42,
            ""Hello world""})]
    public interface IInterfaceWithAttributeWithObjectArrayInitialiser { }
}");
        }

        [Fact]
        public void Should_handle_attribute_with_string_array_initialiser()
        {
            AssertPublicApi<IInterfaceWithAttributeWithStringArrayInitialiser>(
@"namespace ApiApproverTests.Examples
{
    [ApiApproverTests.Examples.AttributeWithStringArrayInitialiser(new string[] {
            ""Hello"",
            ""world""})]
    public interface IInterfaceWithAttributeWithStringArrayInitialiser { }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            AssertPublicApi<IInterfaceWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    public interface IInterfaceWithSimpleAttribute { }
}", excludedAttributes: new[] { "ApiApproverTests.Examples.SimpleAttribute" });
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        [SimpleAttribute]
        public interface IInterfaceWithSimpleAttribute
        {
        }

        [AttributeWithPositionalParameters1("Hello")]
        public interface IInterfaceWithAttributeWithStringPositionalParameters
        {
        }

        [AttributeWithPositionalParameters2(42)]
        public interface IInterfaceWithAttributeWithIntPositionalParameters
        {
        }

        [AttributeWithMultiplePositionalParameters(42, "Hello world")]
        public interface IInterfaceWithAttributeWithMultiplePositionalParameters
        {
        }

        [AttributeWithNamedParameter(IntValue = 42)]
        public interface IInterfaceWithIntNamedParameterAttribute
        {
        }

        [AttributeWithNamedParameter(StringValue = "Hello")]
        public interface IInterfaceWithStringNamedParameterAttribute
        {
        }

        [AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
        public interface IInterfaceWithAttributeWithMultipleNamedParameters
        {
        }

        [AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)]
        public interface IInterfaceWithAttributeWithBothNamedAndPositionalParameters
        {
        }

        [AttributeWithSimpleEnum(SimpleEnum.Blue)]
        public interface IInterfaceWithAttributeWithSimpleEnum
        {
        }

        [AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
        public interface IInterfaceWithAttributeWithEnumFlags
        {
        }

        [Attribute_ZZ]
        [Attribute_MM]
        [Attribute_AA]
        public interface IInterfaceWithMultipleAttributes
        {
        }

        [AttributeWithObjectInitialiser("Hello world")]
        public interface IInterfaceWithAttributeWithObjectInitialiser
        {
        }

        [AttributeWithObjectArrayInitialiser(42, "Hello world")]
        public interface IInterfaceWithAttributeWithObjectArrayInitialiser
        {
        }

        [AttributeWithStringArrayInitialiser("Hello", "world")]
        public interface IInterfaceWithAttributeWithStringArrayInitialiser
        {
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}