using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Property_modifiers : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_abstract_modifier()
        {
            AssertPublicApi<ClassWithAbstractProperty>(
@"namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractProperty
    {
        protected ClassWithAbstractProperty() { }
        public abstract string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_static_modifier()
        {
            AssertPublicApi<ClassWithStaticProperty>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithStaticProperty
    {
        public ClassWithStaticProperty() { }
        public static string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_virtual_modifier()
        {
            AssertPublicApi<ClassWithVirtualProperty>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithVirtualProperty
    {
        public ClassWithVirtualProperty() { }
        public virtual string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_override_modifier()
        {
            AssertPublicApi<ClassWithOverriddenProperty>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithOverriddenProperty : PublicApiGeneratorTests.Examples.ClassWithVirtualProperty
    {
        public ClassWithOverriddenProperty() { }
        public override string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_new_modifier()
        {
            AssertPublicApi<ClassWithPropertyHiding>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPropertyHiding : PublicApiGeneratorTests.Examples.ClassWithProperty
    {
        public ClassWithPropertyHiding() { }
        public new string Value { get; set; }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable ValueParameterNotUsed
    namespace Examples
    {
        public class ClassWithProperty
        {
            public string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public abstract class ClassWithAbstractProperty
        {
            public abstract string Value { get; set; }
        }

        public class ClassWithStaticProperty
        {
            public static string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithVirtualProperty
        {
            public virtual string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithOverriddenProperty : ClassWithVirtualProperty
        {
            public override string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithPropertyHiding : ClassWithProperty
        {
            public new string Value
            {
                get { return string.Empty; }
                set { }
            }
        }
    }
    // ReSharper restore ValueParameterNotUsed
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}