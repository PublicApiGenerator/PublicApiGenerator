using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Method_parameter_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameter()
        {
            AssertPublicApi<MethodParameterWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    public class MethodParameterWithSimpleAttribute
    {
        public void Method([ApiApproverTests.Examples.SimpleAttribute()] int value) { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<MethodParameterWithAttributeWithPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodParameterWithAttributeWithPositionalParameters
    {
        public void Method1([ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")] int value) { }
        public void Method2([ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)] int value) { }
        public void Method3([ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello"")] int value) { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<MethodParameterWithAttributeWithNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodParameterWithAttributeWithNamedParameters
    {
        public void Method1([ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")] int value) { }
        public void Method2([ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)] int value) { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<MethodParameterWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodParameterWithAttributeWithMultipleNamedParameters
    {
        public void Method([ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello"")] int value) { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<MethodParameterWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodParameterWithAttributeWithBothNamedAndPositionalParameters
    {
        public void Method([ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")] int value) { }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<MethodParameterWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    public class MethodParameterWithAttributeWithEnumFlags
    {
        public void Method([ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)] int value) { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<MethodParameterWithMultipleAttributes>(
@"namespace ApiApproverTests.Examples
{
    public class MethodParameterWithMultipleAttributes
    {
        public void Method([ApiApproverTests.Examples.Attribute_AA()] [ApiApproverTests.Examples.Attribute_MM()] [ApiApproverTests.Examples.Attribute_ZZ()] int value) { }
    }
}");
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