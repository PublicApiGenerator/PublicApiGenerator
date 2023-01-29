using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Interface_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<IInterfaceWithSimpleAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.Simple]
    public interface IInterfaceWithSimpleAttribute { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<IInterfaceWithAttributeWithStringPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1(""Hello"")]
    public interface IInterfaceWithAttributeWithStringPositionalParameters { }
}");
            AssertPublicApi<IInterfaceWithAttributeWithIntPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2(42)]
    public interface IInterfaceWithAttributeWithIntPositionalParameters { }
}");
            AssertPublicApi<IInterfaceWithAttributeWithMultiplePositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParameters(42, ""Hello world"")]
    public interface IInterfaceWithAttributeWithMultiplePositionalParameters { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<IInterfaceWithIntNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42)]
    public interface IInterfaceWithIntNamedParameterAttribute { }
}");

            AssertPublicApi<IInterfaceWithStringNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(StringValue=""Hello"")]
    public interface IInterfaceWithStringNamedParameterAttribute { }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<IInterfaceWithAttributeWithMultipleNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42, StringValue=""Hello world"")]
    public interface IInterfaceWithAttributeWithMultipleNamedParameters { }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<IInterfaceWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedAndPositionalParameter(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
    public interface IInterfaceWithAttributeWithBothNamedAndPositionalParameters { }
}");
        }

        [Fact]
        public void Should_output_enum_value()
        {
            AssertPublicApi<IInterfaceWithAttributeWithSimpleEnum>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithSimpleEnum(PublicApiGeneratorTests.Examples.SimpleEnum.Blue)]
    public interface IInterfaceWithAttributeWithSimpleEnum { }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<IInterfaceWithAttributeWithEnumFlags>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithEnumFlags(PublicApiGeneratorTests.Examples.EnumWithFlags.One | PublicApiGeneratorTests.Examples.EnumWithFlags.Two | PublicApiGeneratorTests.Examples.EnumWithFlags.Three)]
    public interface IInterfaceWithAttributeWithEnumFlags { }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<IInterfaceWithMultipleAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.Attribute_AA]
    [PublicApiGeneratorTests.Examples.Attribute_MM]
    [PublicApiGeneratorTests.Examples.Attribute_ZZ]
    public interface IInterfaceWithMultipleAttributes { }
}");
        }

        [Fact]
        public void Should_handle_attribute_with_object_initialiser()
        {
            AssertPublicApi<IInterfaceWithAttributeWithObjectInitialiser>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithObjectInitialiser(""Hello world"")]
    public interface IInterfaceWithAttributeWithObjectInitialiser { }
}");
        }

        [Fact]
        public void Should_handle_attribute_with_object_array_initialiser()
        {
            AssertPublicApi<IInterfaceWithAttributeWithObjectArrayInitialiser>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithObjectArrayInitialiser(new object[] {
            42,
            ""Hello world""})]
    public interface IInterfaceWithAttributeWithObjectArrayInitialiser { }
}");
        }

        [Fact]
        public void Should_handle_attribute_with_string_array_initialiser()
        {
            AssertPublicApi<IInterfaceWithAttributeWithStringArrayInitialiser>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithStringArrayInitialiser(new string[] {
            ""Hello"",
            ""world""})]
    public interface IInterfaceWithAttributeWithStringArrayInitialiser { }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            var options = new DefaultApiGeneratorOptions
            {
                ExcludeAttributes = new[] { "PublicApiGeneratorTests.Examples.SimpleAttribute" }
            };

            AssertPublicApi<IInterfaceWithSimpleAttribute>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public interface IInterfaceWithSimpleAttribute { }
}", options);
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
