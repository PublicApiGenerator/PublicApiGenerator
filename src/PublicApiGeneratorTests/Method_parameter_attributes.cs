using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Method_parameter_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameter()
        {
            AssertPublicApi<MethodParameterWithSimpleAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodParameterWithSimpleAttribute
    {
        public MethodParameterWithSimpleAttribute() { }
        public void Method([PublicApiGeneratorTests.Examples.SimpleAttribute()] int value) { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<MethodParameterWithAttributeWithPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodParameterWithAttributeWithPositionalParameters
    {
        public MethodParameterWithAttributeWithPositionalParameters() { }
        public void Method1([PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")] int value) { }
        public void Method2([PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2Attribute(42)] int value) { }
        public void Method3([PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello"")] int value) { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<MethodParameterWithAttributeWithNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodParameterWithAttributeWithNamedParameters
    {
        public MethodParameterWithAttributeWithNamedParameters() { }
        public void Method1([PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")] int value) { }
        public void Method2([PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)] int value) { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<MethodParameterWithAttributeWithMultipleNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodParameterWithAttributeWithMultipleNamedParameters
    {
        public MethodParameterWithAttributeWithMultipleNamedParameters() { }
        public void Method([PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello"")] int value) { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<MethodParameterWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodParameterWithAttributeWithBothNamedAndPositionalParameters
    {
        public MethodParameterWithAttributeWithBothNamedAndPositionalParameters() { }
        public void Method([PublicApiGeneratorTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")] int value) { }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<MethodParameterWithAttributeWithEnumFlags>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodParameterWithAttributeWithEnumFlags
    {
        public MethodParameterWithAttributeWithEnumFlags() { }
        public void Method([PublicApiGeneratorTests.Examples.AttributeWithEnumFlagsAttribute(PublicApiGeneratorTests.Examples.EnumWithFlags.One | PublicApiGeneratorTests.Examples.EnumWithFlags.Two | PublicApiGeneratorTests.Examples.EnumWithFlags.Three)] int value) { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<MethodParameterWithMultipleAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodParameterWithMultipleAttributes
    {
        public MethodParameterWithMultipleAttributes() { }
        public void Method([PublicApiGeneratorTests.Examples.Attribute_AA()] [PublicApiGeneratorTests.Examples.Attribute_MM()] [PublicApiGeneratorTests.Examples.Attribute_ZZ()] int value) { }
    }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            AssertPublicApi<MethodParameterWithAttributeWithPositionalParameters>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class MethodParameterWithAttributeWithPositionalParameters
    {
        public MethodParameterWithAttributeWithPositionalParameters() { }
        public void Method1([PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")] int value) { }
        public void Method2(int value) { }
        public void Method3([PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello"")] int value) { }
    }
}", excludeAttributes: new[] { "PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2Attribute" });
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedParameter.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class MethodParameterWithSimpleAttribute
        {
            public void Method([SimpleAttribute] int value)
            {
            }
        }

        public class MethodParameterWithAttributeWithPositionalParameters
        {
            public void Method1([AttributeWithPositionalParameters1("Hello")] int value)
            {
            }

            public void Method2([AttributeWithPositionalParameters2(42)] int value)
            {
            }

            public void Method3([AttributeWithMultiplePositionalParameters(42, "Hello")] int value)
            {
            }
        }

        public class MethodParameterWithAttributeWithNamedParameters
        {
            public void Method1([AttributeWithNamedParameter(StringValue = "Hello")] int value)
            {
            }

            public void Method2([AttributeWithNamedParameter(IntValue = 42)] int value)
            {
            }
        }

        public class MethodParameterWithAttributeWithMultipleNamedParameters
        {
            public void Method([AttributeWithNamedParameter(StringValue = "Hello", IntValue = 42)] int value)
            {
            }
        }

        public class MethodParameterWithAttributeWithBothNamedAndPositionalParameters
        {
            public void Method([AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)] int value)
            {
            }
        }

        public class MethodParameterWithAttributeWithEnumFlags
        {
            public void Method([AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)] int value)
            {
            }
        }

        public class MethodParameterWithMultipleAttributes
        {
            public void Method([Attribute_ZZ] [Attribute_MM] [Attribute_AA] int value)
            {
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedParameter.Global
    // ReSharper restore UnusedMember.Global
}