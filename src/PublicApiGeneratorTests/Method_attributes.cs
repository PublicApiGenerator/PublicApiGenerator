using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Method_attributes : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_add_attribute_with_no_parameters()
        {
            AssertPublicApi<MethodWithSimpleAttribute>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithSimpleAttribute
    {
        public MethodWithSimpleAttribute() { }
        [PublicApiGeneratorTests.Examples.Simple]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_positional_parameters()
        {
            AssertPublicApi<MethodsWithAttributeWithPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodsWithAttributeWithPositionalParameters
    {
        public MethodsWithAttributeWithPositionalParameters() { }
        [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters1(""Hello"")]
        public void Method1() { }
        [PublicApiGeneratorTests.Examples.AttributeWithPositionalParameters2(42)]
        public void Method2() { }
        [PublicApiGeneratorTests.Examples.AttributeWithMultiplePositionalParameters(42, ""Hello world"")]
        public void Method3() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_named_parameters()
        {
            AssertPublicApi<MethodsWithAttributeWithNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodsWithAttributeWithNamedParameters
    {
        public MethodsWithAttributeWithNamedParameters() { }
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(StringValue=""Hello"")]
        public void Method1() { }
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42)]
        public void Method2() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_named_parameters_in_alphabetical_order()
        {
            AssertPublicApi<MethodWithAttributeWithMultipleNamedParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithAttributeWithMultipleNamedParameters
    {
        public MethodWithAttributeWithMultipleNamedParameters() { }
        [PublicApiGeneratorTests.Examples.AttributeWithNamedParameter(IntValue=42, StringValue=""Hello world"")]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_add_attribute_with_both_named_and_positional_parameters()
        {
            AssertPublicApi<MethodWithAttributeWithBothNamedAndPositionalParameters>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithAttributeWithBothNamedAndPositionalParameters
    {
        public MethodWithAttributeWithBothNamedAndPositionalParameters() { }
        [PublicApiGeneratorTests.Examples.AttributeWithNamedAndPositionalParameter(42, ""Hello world"", IntValue=13, StringValue=""Thingy"")]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_expand_enum_flags()
        {
            AssertPublicApi<MethodWithAttributeWithEnumFlags>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithAttributeWithEnumFlags
    {
        public MethodWithAttributeWithEnumFlags() { }
        [PublicApiGeneratorTests.Examples.AttributeWithEnumFlags(PublicApiGeneratorTests.Examples.EnumWithFlags.One | PublicApiGeneratorTests.Examples.EnumWithFlags.Two | PublicApiGeneratorTests.Examples.EnumWithFlags.Three)]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_handle_typeof_argument()
        {
            AssertPublicApi<MethodWithAttributeWithType>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithAttributeWithType
    {
        public MethodWithAttributeWithType() { }
        [PublicApiGeneratorTests.Examples.AttributeWithTypeParameter(typeof(string))]
        public void Method1() { }
        [PublicApiGeneratorTests.Examples.AttributeWithTypeParameter(typeof(PublicApiGeneratorTests.Examples.ComplexType))]
        public void Method2() { }
        [PublicApiGeneratorTests.Examples.AttributeWithTypeParameter(typeof(PublicApiGeneratorTests.Examples.GenericType<PublicApiGeneratorTests.Examples.ComplexType>))]
        public void Method3() { }
    }
}");
        }

        [Fact]
        public void Should_add_multiple_attributes_in_alphabetical_order()
        {
            AssertPublicApi<MethodWithMultipleAttributes>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithMultipleAttributes
    {
        public MethodWithMultipleAttributes() { }
        [PublicApiGeneratorTests.Examples.Attribute_AA]
        [PublicApiGeneratorTests.Examples.Attribute_MM]
        [PublicApiGeneratorTests.Examples.Attribute_ZZ]
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_ignore_compiler_attributes()
        {
            AssertPublicApi<MethodWithCompilerAttributes>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithCompilerAttributes
    {
        public MethodWithCompilerAttributes() { }
        public void Method() { }
    }
}");
        }

        [Fact]
        public void Should_skip_excluded_attribute()
        {
            AssertPublicApi<MethodWithAttributeWithMultipleNamedParameters>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class MethodWithAttributeWithMultipleNamedParameters
    {
        public MethodWithAttributeWithMultipleNamedParameters() { }
        public void Method() { }
    }
}", opt => opt.ExcludeAttributes = new[] { "PublicApiGeneratorTests.Examples.AttributeWithNamedParameterAttribute" });
        }
    }

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

        [DefaultMember("Method")]
        public class MethodWithCompilerAttributes
        {
            [IteratorStateMachine(typeof(MethodWithCompilerAttributes))]
            [AsyncStateMachine(typeof(MethodWithCompilerAttributes))]
            [GeneratedCode("foo", "4.0")]
            [EditorBrowsable(EditorBrowsableState.Never)]
            [DebuggerNonUserCodeAttribute]
            [DebuggerStepThroughAttribute]
            public void Method()
            {
            }
        }

    }
}
