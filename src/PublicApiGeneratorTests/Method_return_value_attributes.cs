using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Method_return_value_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<MethodReturnValueWithSimpleAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodReturnValueWithSimpleAttribute
    {
        public MethodReturnValueWithSimpleAttribute() { }
        [return: PublicApiGeneratorTests.Examples.SimpleAttribute()]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<MethodReturnValuesWithAttributeWithPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodReturnValuesWithAttributeWithPositionalParameters
    {
        public MethodReturnValuesWithAttributeWithPositionalParameters() { }
        [return: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
        public void Method1() { }
        [return: PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
        public void Method2() { }
        [return: PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
        public void Method3() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<MethodReturnValuesWithAttributeWithNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodReturnValuesWithAttributeWithNamedParameters
    {
        public MethodReturnValuesWithAttributeWithNamedParameters() { }
        [return: PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
        public void Method1() { }
        [return: PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
        public void Method2() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<MethodReturnValueWithAttributeWithMultipleNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodReturnValueWithAttributeWithMultipleNamedParameters
    {
        public MethodReturnValueWithAttributeWithMultipleNamedParameters() { }
        [return: PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<MethodReturnValueWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodReturnValueWithAttributeWithBothNamedAndPositionalParameters
    {
        public MethodReturnValueWithAttributeWithBothNamedAndPositionalParameters() { }
        [return: PublicApiGeneratorTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<MethodReturnValueWithAttributeWithEnumFlags>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodReturnValueWithAttributeWithEnumFlags
    {
        public MethodReturnValueWithAttributeWithEnumFlags() { }
        [return: PublicApiGeneratorTests.Examples.AttributeWithEnumFlagsAttribute(PublicApiGeneratorTests.Examples.EnumWithFlags.One | PublicApiGeneratorTests.Examples.EnumWithFlags.Two | PublicApiGeneratorTests.Examples.EnumWithFlags.Three)]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<MethodReturnValueWithMultipleAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodReturnValueWithMultipleAttributes
    {
        public MethodReturnValueWithMultipleAttributes() { }
        [return: PublicApiGeneratorTests.Examples.Attribute_AA()]
        [return: PublicApiGeneratorTests.Examples.Attribute_MM()]
        [return: PublicApiGeneratorTests.Examples.Attribute_ZZ()]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_order_return_and_method_attributes_correctly()
        {
            AssertPublicApi<MethodWithAttributesOnMethodAndReturnValue>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithAttributesOnMethodAndReturnValue
    {
        public MethodWithAttributesOnMethodAndReturnValue() { }
        [PublicApiGeneratorTests.Examples.SimpleAttribute()]
        [return: PublicApiGeneratorTests.Examples.SimpleAttribute()]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            AssertPublicApi<MethodReturnValueWithAttributeWithMultipleNamedParameters>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class MethodReturnValueWithAttributeWithMultipleNamedParameters
    {
        public MethodReturnValueWithAttributeWithMultipleNamedParameters() { }
        public void Method() { }
    }
}", excludeAttributes: new[] { "PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute" });
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class MethodReturnValueWithSimpleAttribute
        {
            [return: SimpleAttribute]
            public void Method()
            {
            }
        }

        public class MethodReturnValuesWithAttributeWithPositionalParameters
        {
            [return: AttributeWithPositionalParameters1("Hello")]
            public void Method1()
            {
            }

            [return: AttributeWithPositionalParameters2(42)]
            public void Method2()
            {
            }

            [return: AttributeWithMultiplePositionalParameters(42, "Hello world")]
            public void Method3()
            {
            }
        }

        public class MethodReturnValuesWithAttributeWithNamedParameters
        {
            [return: AttributeWithNamedParameter(StringValue = "Hello")]
            public void Method1()
            {
            }

            [return: AttributeWithNamedParameter(IntValue = 42)]
            public void Method2()
            {
            }
        }

        public class MethodReturnValueWithAttributeWithMultipleNamedParameters
        {
            [return: AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
            public void Method()
            {
            }
        }

        public class MethodReturnValueWithAttributeWithBothNamedAndPositionalParameters
        {
            [return: AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)]
            public void Method()
            {
            }
        }

        public class MethodReturnValueWithAttributeWithEnumFlags
        {
            [return: AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
            public void Method()
            {
            }
        }

        public class MethodReturnValueWithMultipleAttributes
        {
            [return: Attribute_ZZ]
            [return: Attribute_MM]
            [return: Attribute_AA]
            public void Method()
            {
            }
        }

        public class MethodWithAttributesOnMethodAndReturnValue
        {
            [SimpleAttribute]
            [return: SimpleAttribute]
            public void Method()
            {
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}