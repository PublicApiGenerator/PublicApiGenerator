using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Class_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<ClassWithSimpleAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.Simple]
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1(""Hello"")]
    public class ClassWithAttributeWithStringPositionalParameters
    {
        public ClassWithAttributeWithStringPositionalParameters() { }
    }
}");
            AssertPublicApi<ClassWithAttributeWithIntPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2(42)]
    public class ClassWithAttributeWithIntPositionalParameters
    {
        public ClassWithAttributeWithIntPositionalParameters() { }
    }
}");
            AssertPublicApi<ClassWithAttributeWithMultiplePositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParameters(42, ""Hello world"")]
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42)]
    public class ClassWithIntNamedParameterAttribute
    {
        public ClassWithIntNamedParameterAttribute() { }
    }
}");

            AssertPublicApi<ClassWithStringNamedParameterAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(StringValue=""Hello"")]
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42, StringValue=""Hello world"")]
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedField(IntValue=42)]
    public class ClassWithIntNamedFieldAttribute
    {
        public ClassWithIntNamedFieldAttribute() { }
    }
}");

            AssertPublicApi<ClassWithStringNamedFieldAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedField(StringValue=""Hello"")]
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedField(IntValue=42, StringValue=""Hello world"")]
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedAndPositionalParameter(42, ""Hello"", IntValue=13, StringValue=""World"")]
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAndField(IntField=42, StringField=""Hello"", IntProperty=13, StringProperty=""World"")]
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithSimpleEnum(PublicApiGeneratorTests.Examples.SimpleEnum.Blue)]
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithEnumFlags(PublicApiGeneratorTests.Examples.EnumWithFlags.One | PublicApiGeneratorTests.Examples.EnumWithFlags.Two | PublicApiGeneratorTests.Examples.EnumWithFlags.Three)]
    public class ClassWithAttributeWithEnumFlags
    {
        public ClassWithAttributeWithEnumFlags() { }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_special_flags()
        {
            AssertPublicApi<ClassWithAttributeWithEnumWithSomeSpecialFlags>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithEnumWithSomeSpecialFlags(PublicApiGeneratorTests.Examples.EnumWithSomeSpecialFlags.PublicConstructors)]
    public class ClassWithAttributeWithEnumWithSomeSpecialFlags
    {
        public ClassWithAttributeWithEnumWithSomeSpecialFlags() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<ClassWithMultipleAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.Attribute_AA]
    [PublicApiGeneratorTests.Examples.Attribute_MM]
    [PublicApiGeneratorTests.Examples.Attribute_ZZ]
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithMultipleUsagesSupport(IntValue=0, StringValue=""ZZZ"")]
    [PublicApiGeneratorTests.Examples.AttributeWithMultipleUsagesSupport(IntValue=1, StringValue=""MMM"")]
    [PublicApiGeneratorTests.Examples.AttributeWithMultipleUsagesSupport(IntValue=2, StringValue=""AAA"")]
    public class ClassWithAttributeWithMultipleUsagesSupport
    {
        public ClassWithAttributeWithMultipleUsagesSupport() { }
    }
}");
        }

        [Fact]
        public void Should_handle_attribute_with_object_initialiser()
        {
            AssertPublicApi<ClassWithAttributeWithObjectInitialiser>(
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithObjectInitialiser(""Hello world"")]
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithObjectArrayInitialiser(new object[] {
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
@"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.AttributeWithStringArrayInitialiser(new string[] {
            ""Hello"",
            ""world""})]
    public class ClassWithAttributeWithStringArrayInitialiser
    {
        public ClassWithAttributeWithStringArrayInitialiser() { }
    }
}");
        }

        [Fact]
        public void Should_not_output_internal_attributes()
        {
            AssertPublicApi<ClassWithInternalAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithInternalAttribute
    {
        public ClassWithInternalAttribute() { }
    }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            AssertPublicApi<ClassWithMultipleAttributes>(
                @"namespace PublicApiGeneratorTests.Examples
{
    [PublicApiGeneratorTests.Examples.Attribute_AA]
    [PublicApiGeneratorTests.Examples.Attribute_ZZ]
    public class ClassWithMultipleAttributes
    {
        public ClassWithMultipleAttributes() { }
    }
}", opt => opt.ExcludeAttributes = new[] { "PublicApiGeneratorTests.Examples.Attribute_MM" });
        }

        [Fact]
        public void Should_include_Serializable_attribute()
        {
            AssertPublicApi<ClassWithSerializableAttribute>(
                @"namespace PublicApiGeneratorTests.Examples
{
    [System.Serializable]
    public class ClassWithSerializableAttribute
    {
        public ClassWithSerializableAttribute() { }
    }
}");
        }

        [Fact]
        public void Should_reproduce_AttributeTargets_value_correctly_on_AttributeUsage_attribute()
        {
            AssertPublicApi<ClassWithAttributeUsageAttribute>(
                @"namespace PublicApiGeneratorTests.Examples
{
    [System.AttributeUsage(System.AttributeTargets.Struct | System.AttributeTargets.Field)]
    public class ClassWithAttributeUsageAttribute : System.Attribute
    {
        public ClassWithAttributeUsageAttribute() { }
    }
}");
        }
    }

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

        [AttributeWithEnumWithSomeSpecialFlags(EnumWithSomeSpecialFlags.PublicConstructors)]
        public class ClassWithAttributeWithEnumWithSomeSpecialFlags
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

        [AttributeWhichIsInternal]
        public class ClassWithInternalAttribute
        {
        }

        [Serializable]
        public class ClassWithSerializableAttribute
        {
        }

        [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Field)]
        public class ClassWithAttributeUsageAttribute : Attribute
        {
        }
    }
}
