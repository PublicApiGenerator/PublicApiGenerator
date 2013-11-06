using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Method_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<MethodWithSimpleAttribute>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithSimpleAttribute
    {
        [ApiApproverTests.Examples.SimpleAttribute()]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<MethodsWithAttributeWithPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodsWithAttributeWithPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithPositionalParameters1Attribute(""Hello"")]
        public void Method1() { }
        [ApiApproverTests.Examples.AttributeWithPositionalParameters2Attribute(42)]
        public void Method2() { }
        [ApiApproverTests.Examples.AttributeWithMultiplePositionalParametersAttribute(42, ""Hello world"")]
        public void Method3() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<MethodsWithAttributeWithNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodsWithAttributeWithNamedParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(StringValue=""Hello"")]
        public void Method1() { }
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42)]
        public void Method2() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            // TODO: Is order actually important? These are properties, after all
            AssertPublicApi<MethodWithAttributeWithMultipleNamedParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithAttributeWithMultipleNamedParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedParameterAttribute(IntValue=42, StringValue=""Hello world"")]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<MethodWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithAttributeWithBothNamedAndPositionalParameters
    {
        [ApiApproverTests.Examples.AttributeWithNamedAndPositionalParameterAttribute(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<MethodWithAttributeWithEnumFlags>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithAttributeWithEnumFlags
    {
        [ApiApproverTests.Examples.AttributeWithEnumFlagsAttribute(ApiApproverTests.Examples.EnumWithFlags.One | ApiApproverTests.Examples.EnumWithFlags.Two | ApiApproverTests.Examples.EnumWithFlags.Three)]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_handle_typeof_argument()
        {
            AssertPublicApi<MethodWithAttributeWithType>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithAttributeWithType
    {
        [ApiApproverTests.Examples.AttributeWithTypeParameterAttribute(typeof(string))]
        public void Method1() { }
        [ApiApproverTests.Examples.AttributeWithTypeParameterAttribute(typeof(ApiApproverTests.Examples.ComplexType))]
        public void Method2() { }
        [ApiApproverTests.Examples.AttributeWithTypeParameterAttribute(typeof(ApiApproverTests.Examples.GenericType<ApiApproverTests.Examples.ComplexType>))]
        public void Method3() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<MethodWithMultipleAttributes>(
@"namespace ApiApproverTests.Examples
{
    public class MethodWithMultipleAttributes
    {
        [ApiApproverTests.Examples.Attribute_AA()]
        [ApiApproverTests.Examples.Attribute_MM()]
        [ApiApproverTests.Examples.Attribute_ZZ()]
        public void Method() { }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class MethodWithSimpleAttribute
        {
            [SimpleAttribute]
            public void Method()
            {
            }
        }

        public class MethodsWithAttributeWithPositionalParameters
        {
            [AttributeWithPositionalParameters1("Hello")]
            public void Method1()
            {
            }

            [AttributeWithPositionalParameters2(42)]
            public void Method2()
            {
            }

            [AttributeWithMultiplePositionalParameters(42, "Hello world")]
            public void Method3()
            {
            }
        }

        public class MethodsWithAttributeWithNamedParameters
        {
            [AttributeWithNamedParameter(StringValue = "Hello")]
            public void Method1()
            {
            }

            [AttributeWithNamedParameter(IntValue = 42)]
            public void Method2()
            {
            }
        }

        public class MethodWithAttributeWithMultipleNamedParameters
        {
            [AttributeWithNamedParameter(StringValue = "Hello world", IntValue = 42)]
            public void Method()
            {
            }
        }

        public class MethodWithAttributeWithBothNamedAndPositionalParameters
        {
            [AttributeWithNamedAndPositionalParameter(42, "Hello world", StringValue = "Thingy", IntValue = 13)]
            public void Method()
            {
            }
        }

        public class MethodWithAttributeWithEnumFlags
        {
            [AttributeWithEnumFlags(EnumWithFlags.One | EnumWithFlags.Two | EnumWithFlags.Three)]
            public void Method()
            {
            }
        }

        public class MethodWithAttributeWithType
        {
            [AttributeWithTypeParameterAttribute(typeof(string))]
            public void Method1() { }
            [AttributeWithTypeParameterAttribute(typeof(ComplexType))]
            public void Method2() { }
            [AttributeWithTypeParameterAttribute(typeof(GenericType<ComplexType>))]
            public void Method3() { }
        }

        public class MethodWithMultipleAttributes
        {
            [Attribute_ZZ]
            [Attribute_MM]
            [Attribute_AA]
            public void Method()
            {
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}