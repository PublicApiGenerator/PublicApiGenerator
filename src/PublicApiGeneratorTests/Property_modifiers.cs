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
        public void Should_output_abstract_new_modifier()
        {
            AssertPublicApi<ClassWithAbstractNewProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractNewProperty : PublicApiGeneratorTests.Examples.ClassWithProperty
    {
        protected ClassWithAbstractNewProperty() { }
        public new abstract string Value { get; set; }
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
        public void Should_output_protected_static_modifier()
        {
            AssertPublicApi<ClassWithProtectedStaticProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedStaticProperty
    {
        public ClassWithProtectedStaticProperty() { }
        protected static string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_static_new_modifier()
        {
            AssertPublicApi<ClassWithStaticNewProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithStaticNewProperty : PublicApiGeneratorTests.Examples.ClassWithStaticProperty
    {
        public ClassWithStaticNewProperty() { }
        public new static string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_protected_static_new_modifier()
        {
            AssertPublicApi<ClassWithProtectedStaticNewProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedStaticNewProperty : PublicApiGeneratorTests.Examples.ClassWithProtectedStaticProperty
    {
        public ClassWithProtectedStaticNewProperty() { }
        protected new static string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_public_static_new_modifier()
        {
            AssertPublicApi<ClassWithPublicStaticNewProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicStaticNewProperty : PublicApiGeneratorTests.Examples.ClassWithProtectedStaticProperty
    {
        public ClassWithPublicStaticNewProperty() { }
        public new static string Value { get; set; }
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
        public void Should_output_protected_virtual_modifier()
        {
            AssertPublicApi<ClassWithVirtualProtectedProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithVirtualProtectedProperty
    {
        public ClassWithVirtualProtectedProperty() { }
        protected virtual string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_override_modifier()
        {
            AssertPublicApi<ClassWithOverridingProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithOverridingProperty : PublicApiGeneratorTests.Examples.ClassWithVirtualProperty
    {
        public ClassWithOverridingProperty() { }
        public override string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_protected_override_modifier()
        {
            AssertPublicApi<ClassWithOverridingProtectedProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithOverridingProtectedProperty : PublicApiGeneratorTests.Examples.ClassWithVirtualProtectedProperty
    {
        public ClassWithOverridingProtectedProperty() { }
        protected override string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_abstract_override_modifier()
        {
            AssertPublicApi<ClassWithAbstractOverridingProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractOverridingProperty : PublicApiGeneratorTests.Examples.ClassWithVirtualProperty
    {
        protected ClassWithAbstractOverridingProperty() { }
        public abstract override string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_sealed_modifier()
        {
            AssertPublicApi<ClassWithSealedOverridingProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithSealedOverridingProperty : PublicApiGeneratorTests.Examples.ClassWithVirtualProperty
    {
        public ClassWithSealedOverridingProperty() { }
        public override sealed string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_protected_sealed_modifier()
        {
            AssertPublicApi<ClassWithProtectedSealedOverridingProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedSealedOverridingProperty : PublicApiGeneratorTests.Examples.ClassWithVirtualProtectedProperty
    {
        public ClassWithProtectedSealedOverridingProperty() { }
        protected override sealed string Value { get; set; }
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

        [Fact]
        public void Should_output_protected_new_modifier()
        {
            AssertPublicApi<ClassWithProtectedPropertyHiding>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedPropertyHiding : PublicApiGeneratorTests.Examples.ClassWithProtectedProperty
    {
        public ClassWithProtectedPropertyHiding() { }
        protected new string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_public_new_modifier()
        {
            AssertPublicApi<ClassWithPublicPropertyHiding>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicPropertyHiding : PublicApiGeneratorTests.Examples.ClassWithProtectedProperty
    {
        public ClassWithPublicPropertyHiding() { }
        public new string Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_unsafe_modifier()
        {
            AssertPublicApi<ClassWithUnsafeProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithUnsafeProperty
    {
        public ClassWithUnsafeProperty() { }
        public unsafe int* Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_unsafe_virtual_modifier()
        {
            AssertPublicApi<ClassWithUnsafeVirtualProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithUnsafeVirtualProperty
    {
        public ClassWithUnsafeVirtualProperty() { }
        public virtual unsafe int* Value { get; set; }
    }
}");
        }

        [Fact]
        public void Should_output_new_modifier_even_under_evil_circumstances()
        {
            AssertPublicApi<AbstractClassRedeclaringAbstractProperty>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public abstract class AbstractClassRedeclaringAbstractProperty : PublicApiGeneratorTests.Examples.ClassInheritingVirtualProperty
    {
        protected AbstractClassRedeclaringAbstractProperty() { }
        public new abstract string Value { get; set; }
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

        public abstract class ClassWithAbstractNewProperty : ClassWithProperty
        {
            public new abstract string Value { get; set; }
        }

        public class ClassWithStaticProperty
        {
            public static string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithProtectedStaticProperty
        {
            protected static string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithStaticNewProperty : ClassWithStaticProperty
        {
            public new static string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithProtectedStaticNewProperty : ClassWithProtectedStaticProperty
        {
            protected new static string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithPublicStaticNewProperty : ClassWithProtectedStaticProperty
        {
            public new static string Value
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

        public class ClassWithVirtualProtectedProperty
        {
            protected virtual string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithOverridingProperty : ClassWithVirtualProperty
        {
            public override string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithOverridingProtectedProperty : ClassWithVirtualProtectedProperty
        {
            protected override string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public abstract class ClassWithAbstractOverridingProperty : ClassWithVirtualProperty
        {
            public abstract override string Value { get; set; }
        }

        public class ClassWithSealedOverridingProperty : ClassWithVirtualProperty
        {
            public sealed override string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithProtectedSealedOverridingProperty : ClassWithVirtualProtectedProperty
        {
            protected sealed override string Value
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

        public class ClassWithProtectedPropertyHiding : ClassWithProtectedProperty
        {
            protected new string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithPublicPropertyHiding : ClassWithProtectedProperty
        {
            public new string Value
            {
                get { return string.Empty; }
                set { }
            }
        }

        public class ClassWithUnsafeProperty
        {
            public unsafe int* Value { get; set; }
        }

        public class ClassWithUnsafeVirtualProperty
        {
            public virtual unsafe int* Value { get; set; }
        }

        public class ClassInheritingVirtualProperty : ClassWithVirtualProperty
        {
        }

        public abstract class AbstractClassRedeclaringAbstractProperty : ClassInheritingVirtualProperty
        {
            protected AbstractClassRedeclaringAbstractProperty()
            {
            }
            public new abstract string Value { get; set; }
        }
    }
    // ReSharper restore ValueParameterNotUsed
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}
