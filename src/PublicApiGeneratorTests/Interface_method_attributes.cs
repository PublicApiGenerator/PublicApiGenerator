using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Interface_method_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<IMethodWithSimpleAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithSimpleAttribute
    {
        [PublicApiGeneratorTests.Examples.Simple]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<IMethodsWithAttributeWithPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodsWithAttributeWithPositionalParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1(""Hello"")]
        void Method1();
        [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2(42)]
        void Method2();
        [PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParameters(42, ""Hello world"")]
        void Method3();
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<IMethodsWithAttributeWithNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodsWithAttributeWithNamedParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(StringValue=""Hello"")]
        void Method1();
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42)]
        void Method2();
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<IMethodWithAttributeWithMultipleNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithAttributeWithMultipleNamedParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42, StringValue=""Hello world"")]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<IMethodWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithAttributeWithBothNamedAndPositionalParameters
    {
        [PublicApiGeneratorTests.Examples.AttributeWithNamedAndPositionalParameter(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<IMethodWithAttributeWithEnumFlags>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithAttributeWithEnumFlags
    {
        [PublicApiGeneratorTests.Examples.AttributeWithEnumFlags(PublicApiGeneratorTests.Examples.EnumWithFlags.One | PublicApiGeneratorTests.Examples.EnumWithFlags.Two | PublicApiGeneratorTests.Examples.EnumWithFlags.Three)]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_handle_typeof_argument()
        {
            AssertPublicApi<IMethodWithAttributeWithType>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithAttributeWithType
    {
        [PublicApiGeneratorTests.Examples.AttributeWithTypeParameter(typeof(string))]
        void Method1();
        [PublicApiGeneratorTests.Examples.AttributeWithTypeParameter(typeof(PublicApiGeneratorTests.Examples.ComplexType))]
        void Method2();
        [PublicApiGeneratorTests.Examples.AttributeWithTypeParameter(typeof(PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType>))]
        void Method3();
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<IMethodWithMultipleAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodWithMultipleAttributes
    {
        [PublicApiGeneratorTests.Examples.Attribute_AA]
        [PublicApiGeneratorTests.Examples.Attribute_MM]
        [PublicApiGeneratorTests.Examples.Attribute_ZZ]
        void Method();
    }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            AssertPublicApi<IMethodsWithAttributeWithNamedParameters>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public interface IMethodsWithAttributeWithNamedParameters
    {
        void Method1();
        void Method2();
    }
}", opt => opt.ExcludeAttributes = ["PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute"]);
        }
    }

    namespace Examples
    {
        public interface IMethodWithSimpleAttribute
        {
            [SimpleAttribute]
            void Method();
        }

        public interface IMethodsWithAttributeWithPositionalParameters
        {
            [AttributeWithPositionalParameters1("Hello")]
            void Method1();

            [AttributeWithPositionalParameters2(42)]
            void Method2();

            [AttributeWithMultiplePositionalParameters(42, "Hello world")]
            void Method3();
        }

        public interface IMethodsWithAttributeWithNamedParameters
        {
            [AttributeWithNamedParameter(StringValue = "Hello")]
            void Method1();

            [AttributeWithNamedParameter(IntValue = 42)]
            void Method2();
        }

        public interface IMethodWithAttributeWithMultipleNamedParameters
        {
            [AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
            void Method();
        }

        public interface IMethodWithAttributeWithBothNamedAndPositionalParameters
        {
            [AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)]
            void Method();
        }

        public interface IMethodWithAttributeWithEnumFlags
        {
            [AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
            void Method();
        }

        public interface IMethodWithAttributeWithType
        {
            [AttributeWithTypeParameterAttribute(typeof(string))]
            void Method1();
            [AttributeWithTypeParameterAttribute(typeof(ComplexType))]
            void Method2();
            [AttributeWithTypeParameterAttribute(typeof(GenericType<ComplexType>))]
            void Method3();
        }

        public interface IMethodWithMultipleAttributes
        {
            [Attribute_ZZ]
            [Attribute_MM]
            [Attribute_AA]
            void Method();
        }
    }
}
