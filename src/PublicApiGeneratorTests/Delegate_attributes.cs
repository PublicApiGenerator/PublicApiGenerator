using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Delegate_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<DelegateWithSimpleAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.Simple]
    public delegate void DelegateWithSimpleAttribute();
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<DelegateWithAttributeWithStringPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1(""Hello"")]
    public delegate void DelegateWithAttributeWithStringPositionalParameters();
}");
            AssertPublicApi<DelegateWithAttributeWithIntPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2(42)]
    public delegate void DelegateWithAttributeWithIntPositionalParameters();
}");
            AssertPublicApi<DelegateWithAttributeWithMultiplePositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParameters(42, ""Hello world"")]
    public delegate void DelegateWithAttributeWithMultiplePositionalParameters();
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<DelegateWithIntNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42)]
    public delegate void DelegateWithIntNamedParameterAttribute();
}");

            AssertPublicApi<DelegateWithStringNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(StringValue=""Hello"")]
    public delegate void DelegateWithStringNamedParameterAttribute();
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<DelegateWithAttributeWithMultipleNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42, StringValue=""Hello world"")]
    public delegate void DelegateWithAttributeWithMultipleNamedParameters();
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_fields()
        {
            AssertPublicApi<DelegateWithIntNamedFieldAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedField(IntValue=42)]
    public delegate void DelegateWithIntNamedFieldAttribute();
}");

            AssertPublicApi<DelegateWithStringNamedFieldAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedField(StringValue=""Hello"")]
    public delegate void DelegateWithStringNamedFieldAttribute();
}");
        }

        [Fact]
        public void Should_add_multiple_named_fields_in_alphabetical_order()
        {
            AssertPublicApi<DelegateWithAttributeWithMultipleNamedFields>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedField(IntValue=42, StringValue=""Hello world"")]
    public delegate void DelegateWithAttributeWithMultipleNamedFields();
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<DelegateWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedAndPositionalParameter(42, ""Hello"", IntValue=13, StringValue=""World"")]
    public delegate void DelegateWithAttributeWithBothNamedAndPositionalParameters();
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_fields_and_parameters()
        {
            AssertPublicApi<DelegateWithAttributeWithBothNamedParametersAndFields>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAndField(IntField=42, StringField=""Hello"", IntProperty=13, StringProperty=""World"")]
    public delegate void DelegateWithAttributeWithBothNamedParametersAndFields();
}");
        }

        [Fact]
        public void Should_output_enum_value()
        {
            AssertPublicApi<DelegateWithAttributeWithSimpleEnum>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithSimpleEnum(PublicApiGeneratorTests.Examples.SimpleEnum.Blue)]
    public delegate void DelegateWithAttributeWithSimpleEnum();
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<DelegateWithAttributeWithEnumFlags>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithEnumFlags(PublicApiGeneratorTests.Examples.EnumWithFlags.One | PublicApiGeneratorTests.Examples.EnumWithFlags.Two | PublicApiGeneratorTests.Examples.EnumWithFlags.Three)]
    public delegate void DelegateWithAttributeWithEnumFlags();
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<DelegateWithMultipleAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.Attribute_AA]
    [PublicApiGeneratorTests.Examples.Attribute_MM]
    [PublicApiGeneratorTests.Examples.Attribute_ZZ]
    public delegate void DelegateWithMultipleAttributes();
}");
        }

        [Fact]
        public void Should_handle_attribute_with_object_initialiser()
        {
            AssertPublicApi<DelegateWithAttributeWithObjectInitialiser>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithObjectInitialiser(""Hello world"")]
    public delegate void DelegateWithAttributeWithObjectInitialiser();
}");
        }

        [Fact]
        public void Should_handle_attribute_with_object_array_initialiser()
        {
            AssertPublicApi<DelegateWithAttributeWithObjectArrayInitialiser>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithObjectArrayInitialiser(new object[] {
            42,
            ""Hello world""})]
    public delegate void DelegateWithAttributeWithObjectArrayInitialiser();
}");
        }

        [Fact]
        public void Should_handle_attribute_with_string_array_initialiser()
        {
            AssertPublicApi<DelegateWithAttributeWithStringArrayInitialiser>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithStringArrayInitialiser(new string[] {
            ""Hello"",
            ""world""})]
    public delegate void DelegateWithAttributeWithStringArrayInitialiser();
}");
        }

        [Fact]
        public void Should_output_attribute_for_return_type()
        {
            AssertPublicApi<DelegateWithAttributeOnReturnValue>(
@"namespace PublicApiGeneratorTests.Examples
{
    [return: PublicApiGeneratorTests.Examples.Simple]
    public delegate string DelegateWithAttributeOnReturnValue();
}");
        }

        [Fact]
        public void Should_output_attribute_for_parameter()
        {
            AssertPublicApi<DelegateWithAttributeOnParameter>(
@"namespace PublicApiGeneratorTests.Examples
{
    public delegate string DelegateWithAttributeOnParameter([PublicApiGeneratorTests.Examples.Simple] int value);
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            AssertPublicApi<DelegateWithMultipleAttributes>(
                @"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.Attribute_AA]
    [PublicApiGeneratorTests.Examples.Attribute_MM]
    public delegate void DelegateWithMultipleAttributes();
}", opt => opt.ExcludeAttributes = new[] { "PublicApiGeneratorTests.Examples.Attribute_ZZ" });
        }
    }

    namespace Examples
    {
        [SimpleAttribute]
        public delegate void DelegateWithSimpleAttribute();

        [AttributeWithPositionalParameters1("Hello")]
        public delegate void DelegateWithAttributeWithStringPositionalParameters();

        [AttributeWithPositionalParameters2(42)]
        public delegate void DelegateWithAttributeWithIntPositionalParameters();

        [AttributeWithMultiplePositionalParameters(42, "Hello world")]
        public delegate void DelegateWithAttributeWithMultiplePositionalParameters();

        [AttributeWithNamedParameter(IntValue = 42)]
        public delegate void DelegateWithIntNamedParameterAttribute();

        [AttributeWithNamedField(IntValue = 42)]
        public delegate void DelegateWithIntNamedFieldAttribute();

        [AttributeWithNamedParameter(StringValue = "Hello")]
        public delegate void DelegateWithStringNamedParameterAttribute();

        [AttributeWithNamedField(StringValue = "Hello")]
        public delegate void DelegateWithStringNamedFieldAttribute();

        [AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
        public delegate void DelegateWithAttributeWithMultipleNamedParameters();

        [AttributeWithNamedField(StringValue = "Hello world", IntValue = 42)]
        public delegate void DelegateWithAttributeWithMultipleNamedFields();

        [AttributeWithNamedAndPositionalParameter(42, "Hello", StringValue = "World", IntValue = 13)]
        public delegate void DelegateWithAttributeWithBothNamedAndPositionalParameters();

        [AttributeWithNamedParameterAndFieldAttribute(IntField = 42, StringField = "Hello", StringProperty = "World",
            IntProperty = 13)]
        public delegate void DelegateWithAttributeWithBothNamedParametersAndFields();

        [AttributeWithSimpleEnum(SimpleEnum.Blue)]
        public delegate void DelegateWithAttributeWithSimpleEnum();

        [AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
        public delegate void DelegateWithAttributeWithEnumFlags();

        [Attribute_ZZ]
        [Attribute_MM]
        [Attribute_AA]
        public delegate void DelegateWithMultipleAttributes();

        [AttributeWithObjectInitialiser("Hello world")]
        public delegate void DelegateWithAttributeWithObjectInitialiser();

        [AttributeWithObjectArrayInitialiser(42, "Hello world")]
        public delegate void DelegateWithAttributeWithObjectArrayInitialiser();

        [AttributeWithStringArrayInitialiser("Hello", "world")]
        public delegate void DelegateWithAttributeWithStringArrayInitialiser();

        [return: SimpleAttribute]
        public delegate string DelegateWithAttributeOnReturnValue();

        public delegate string DelegateWithAttributeOnParameter([SimpleAttribute] int value);
    }
}
