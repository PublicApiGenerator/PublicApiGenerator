using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Method_return_value_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<MethodReturnValueWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    public class MethodReturnValueWithSimpleAttribute
    {
        [return: ApiApproverTests.Examples.SimpleAttribute()]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<MethodReturnValuesWithAttributeWithPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodReturnValuesWithAttributeWithPositionalParameters
    {
        [return: ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
        public void Method1() { }
        [return: ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
        public void Method2() { }
        [return: ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
        public void Method3() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<MethodReturnValuesWithAttributeWithNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodReturnValuesWithAttributeWithNamedParameters
    {
        [return: ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
        public void Method1() { }
        [return: ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
        public void Method2() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<MethodReturnValueWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodReturnValueWithAttributeWithMultipleNamedParameters
    {
        [return: ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<MethodReturnValueWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodReturnValueWithAttributeWithBothNamedAndPositionalParameters
    {
        [return: ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<MethodReturnValueWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    public class MethodReturnValueWithAttributeWithEnumFlags
    {
        [return: ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<MethodReturnValueWithMultipleAttributes>(
@"namespace ApiApproverTests.Examples
{
    public class MethodReturnValueWithMultipleAttributes
    {
        [return: ApiApproverTests.Examples.Attribute_AA()]
        [return: ApiApproverTests.Examples.Attribute_MM()]
        [return: ApiApproverTests.Examples.Attribute_ZZ()]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_order_return_and_method_attributes_correctly()
        {
            AssertPublicApi<MethodWithAttributesOnMethodAndReturnValue>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithAttributesOnMethodAndReturnValue
    {
        [ApiApproverTests.Examples.SimpleAttribute()]
        [return: ApiApproverTests.Examples.SimpleAttribute()]
        public void Method() { }
    }
}");
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