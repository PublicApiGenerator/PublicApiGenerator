using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Method_modifiers : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_abstract_modifier()
        {
            AssertPublicApi<ClassWithAbstractMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractMethod
    {
        protected ClassWithAbstractMethod() { }
        public abstract void DoSomething();
    }
}");
        }

        [Fact]
        public void Should_output_abstract_new_modifier()
        {
            AssertPublicApi<ClassWithAbstractNewMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractNewMethod : PublicApiGeneratorTests.Examples.ClassWithVirtualMethod
    {
        protected ClassWithAbstractNewMethod() { }
        public new abstract void DoSomething();
    }
}");
        }

        [Fact]
        public void Should_output_static_modifier()
        {
            AssertPublicApi<ClassWithStaticMethod>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithStaticMethod
    {
        public ClassWithStaticMethod() { }
        public static void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_static_modifier()
        {
            AssertPublicApi<ClassWithProtectedStaticMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedStaticMethod
    {
        public ClassWithProtectedStaticMethod() { }
        protected static void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_static_new_modifier()
        {
            AssertPublicApi<ClassWithStaticNewMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithStaticNewMethod : PublicApiGeneratorTests.Examples.ClassWithStaticMethod
    {
        public ClassWithStaticNewMethod() { }
        public new static void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_static_new_modifier()
        {
            AssertPublicApi<ClassWithProtectedStaticNewMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedStaticNewMethod : PublicApiGeneratorTests.Examples.ClassWithProtectedStaticMethod
    {
        public ClassWithProtectedStaticNewMethod() { }
        protected new static void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_public_static_new_modifier()
        {
            AssertPublicApi<ClassWithPublicStaticNewMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicStaticNewMethod : PublicApiGeneratorTests.Examples.ClassWithProtectedStaticMethod
    {
        public ClassWithPublicStaticNewMethod() { }
        public new static void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_virtual_modifier()
        {
            AssertPublicApi<ClassWithVirtualMethod>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithVirtualMethod
    {
        public ClassWithVirtualMethod() { }
        public virtual void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_virtual_modifier()
        {
            AssertPublicApi<ClassWithProtectedVirtualMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedVirtualMethod
    {
        public ClassWithProtectedVirtualMethod() { }
        protected virtual void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_new_virtual_modifier()
        {
            AssertPublicApi<ClassWithNewVirtualMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithNewVirtualMethod : PublicApiGeneratorTests.Examples.ClassWithVirtualMethod
    {
        public ClassWithNewVirtualMethod() { }
        public new virtual void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_new_virtual_modifier()
        {
            AssertPublicApi<ClassWithProtectedNewVirtualMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedNewVirtualMethod : PublicApiGeneratorTests.Examples.ClassWithProtectedVirtualMethod
    {
        public ClassWithProtectedNewVirtualMethod() { }
        protected new virtual void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_override_modifier()
        {
            AssertPublicApi<ClassWithOverridingMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithOverridingMethod : PublicApiGeneratorTests.Examples.ClassWithVirtualMethod
    {
        public ClassWithOverridingMethod() { }
        public override void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_override_modifier()
        {
            AssertPublicApi<ClassWithProtectedOverridingMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedOverridingMethod : PublicApiGeneratorTests.Examples.ClassWithProtectedVirtualMethod
    {
        public ClassWithProtectedOverridingMethod() { }
        protected override void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_abstract_override_modifier()
        {
            AssertPublicApi<ClassWithAbstractOverridingMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public abstract class ClassWithAbstractOverridingMethod : PublicApiGeneratorTests.Examples.ClassWithVirtualMethod
    {
        protected ClassWithAbstractOverridingMethod() { }
        public abstract override void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_sealed_modifier()
        {
            AssertPublicApi<ClassWithSealedOverridingMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithSealedOverridingMethod : PublicApiGeneratorTests.Examples.ClassWithVirtualMethod
    {
        public ClassWithSealedOverridingMethod() { }
        public override sealed void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_sealed_modifier()
        {
            AssertPublicApi<ClassWithProtectedSealedOverridingMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedSealedOverridingMethod : PublicApiGeneratorTests.Examples.ClassWithProtectedVirtualMethod
    {
        public ClassWithProtectedSealedOverridingMethod() { }
        protected override sealed void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_allow_overriding_object_methods()
        {
            AssertPublicApi<ClassWithMethodOverridingObjectMethod>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithMethodOverridingObjectMethod
    {
        public ClassWithMethodOverridingObjectMethod() { }
        public override string ToString() { }
    }
}");
        }

        [Fact]
        public void Should_output_new_modifier()
        {
            AssertPublicApi<ClassWithMethodHiding>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithMethodHiding : PublicApiGeneratorTests.Examples.ClassWithSimpleMethod
    {
        public ClassWithMethodHiding() { }
        public new void Method() { }
    }
}");
        }

        [Fact]
        public void Should_output_new_modifier_for_generics()
        {
            AssertPublicApi(typeof(ClassWithMethodExtensions<>),
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithMethodExtensions<T> : PublicApiGeneratorTests.Examples.ClassWithMethodExtensions
    {
        public ClassWithMethodExtensions() { }
        public new PublicApiGeneratorTests.Examples.ClassWithMethodExtensions<T> Extend(string parameter) { }
    }
}");
        }

        [Fact]
        public void Should_output_protected_new_modifier()
        {
            AssertPublicApi<ClassWithProtectedMethodHiding>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedMethodHiding : PublicApiGeneratorTests.Examples.ClassWithSimpleProtectedMethod
    {
        public ClassWithProtectedMethodHiding() { }
        protected new void Method() { }
    }
}");
        }

        [Fact]
        public void Should_output_public_new_modifier()
        {
            AssertPublicApi<ClassWithPublicMethodHiding>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicMethodHiding : PublicApiGeneratorTests.Examples.ClassWithSimpleProtectedMethod
    {
        public ClassWithPublicMethodHiding() { }
        public new void Method() { }
    }
}");
        }

        [Fact]
        public void Should_not_output_new_when_base_differs_in_parameters()
        {
            AssertPublicApi<ClassWithBaseMethodConstraint>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithBaseMethodConstraint : PublicApiGeneratorTests.Examples.ClassWithBaseMethod
    {
        public ClassWithBaseMethodConstraint() { }
        public void SomeMethod(PublicApiGeneratorTests.Examples.ClassWithBaseMethodConstraint input1, PublicApiGeneratorTests.Examples.ClassWithBaseMethodConstraint input2) { }
    }
}");
        }

        [Fact]
        public void Should_output_unsafe_modifier()
        {
            AssertPublicApi<ClassWithUnsafeMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithUnsafeMethod
    {
        public ClassWithUnsafeMethod() { }
        public unsafe void* DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_unsafe_virtual_modifier()
        {
            AssertPublicApi<ClassWithUnsafeVirtualMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithUnsafeVirtualMethod
    {
        public ClassWithUnsafeVirtualMethod() { }
        public virtual unsafe void* DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_new_modifier_even_under_evil_circumstances()
        {
            AssertPublicApi<AbstractClassRedeclaringAbstractMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public abstract class AbstractClassRedeclaringAbstractMethod : PublicApiGeneratorTests.Examples.ClassInheritingVirtualMethod
    {
        protected AbstractClassRedeclaringAbstractMethod() { }
        public new abstract void DoSomething();
    }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedMemberHierarchy.Global
    // ReSharper disable MemberCanBeProtected.Global
    // ReSharper disable RedundantOverridenMember
    // ReSharper disable ClassWithVirtualMembersNeverInherited.Global
    namespace Examples
    {
        public class ClassWithStaticMethod
        {
            public static void DoSomething()
            {
            }
        }

        public class ClassWithProtectedStaticMethod
        {
            protected static void DoSomething()
            {
            }
        }

        public class ClassWithStaticNewMethod : ClassWithStaticMethod
        {
            public new static void DoSomething()
            {
            }
        }

        public class ClassWithProtectedStaticNewMethod : ClassWithProtectedStaticMethod
        {
            protected new static void DoSomething()
            {
            }
        }

        public class ClassWithPublicStaticNewMethod : ClassWithProtectedStaticMethod
        {
            public new static void DoSomething()
            {
            }
        }

        public class ClassWithSimpleMethod
        {
            public void Method()
            {
            }
        }

        public class ClassWithMethodHiding : ClassWithSimpleMethod
        {
            public new void Method()
            {
            }
        }

        public class ClassWithSimpleProtectedMethod
        {
            protected void Method()
            {
            }
        }

        public class ClassWithProtectedMethodHiding : ClassWithSimpleProtectedMethod
        {
            protected new void Method()
            {
            }
        }

        public class ClassWithPublicMethodHiding : ClassWithSimpleProtectedMethod
        {
            public new void Method()
            {
            }
        }

        public abstract class ClassWithAbstractMethod
        {
            public abstract void DoSomething();
        }

        public abstract class ClassWithAbstractNewMethod : ClassWithVirtualMethod
        {
            public new abstract void DoSomething();
        }

        public class ClassWithVirtualMethod
        {
            public virtual void DoSomething()
            {
            }
        }

        public class ClassWithProtectedVirtualMethod
        {
            protected virtual void DoSomething()
            {
            }
        }

        public class ClassWithNewVirtualMethod : ClassWithVirtualMethod
        {
            public new virtual void DoSomething()
            {
            }
        }

        public class ClassWithProtectedNewVirtualMethod : ClassWithProtectedVirtualMethod
        {
            protected new virtual void DoSomething()
            {
            }
        }


        public class ClassWithOverridingMethod : ClassWithVirtualMethod
        {
            public override void DoSomething()
            {
                base.DoSomething();
            }
        }

        public class ClassWithProtectedOverridingMethod : ClassWithProtectedVirtualMethod
        {
            protected override void DoSomething()
            {
                base.DoSomething();
            }
        }

        public abstract class ClassWithAbstractOverridingMethod : ClassWithVirtualMethod
        {
            public abstract override void DoSomething();
        }

        public class ClassWithSealedOverridingMethod : ClassWithVirtualMethod
        {
            public sealed override void DoSomething()
            {
                base.DoSomething();
            }
        }

        public class ClassWithProtectedSealedOverridingMethod : ClassWithProtectedVirtualMethod
        {
            protected sealed override void DoSomething()
            {
                base.DoSomething();
            }
        }

        public class ClassWithMethodOverridingObjectMethod
        {
            public override string ToString()
            {
                return base.ToString();
            }
        }

        public class ClassWithUnsafeMethod
        {
            public unsafe void* DoSomething()
            {
                return null;
            }
        }

        public class ClassWithUnsafeVirtualMethod
        {
            public virtual unsafe void* DoSomething()
            {
                return null;
            }
        }

        public class ClassInheritingVirtualMethod : ClassWithVirtualMethod
        {
        }

        public abstract class AbstractClassRedeclaringAbstractMethod : ClassInheritingVirtualMethod
        {
            protected AbstractClassRedeclaringAbstractMethod()
            {
            }
            public new abstract void DoSomething();
        }

        public class ClassWithMethodExtensions
        {
            public ClassWithMethodExtensions Extend(string parameter)
            {
                return this;
            }
        }

        public class ClassWithMethodExtensions<T> : ClassWithMethodExtensions
        {
            public new ClassWithMethodExtensions<T> Extend(string parameter)
            {
                return this;
            }
        }

        public class ClassWithBaseMethod
        {
            public void SomeMethod(ClassWithBaseMethod input1, ClassWithBaseMethod input2)
            {
            }
        }

        public class ClassWithBaseMethodConstraint : ClassWithBaseMethod
        {
            public void SomeMethod(ClassWithBaseMethodConstraint input1, ClassWithBaseMethodConstraint input2)
            {
            }
        }
    }
    // ReSharper restore ClassWithVirtualMembersNeverInherited.Global
    // ReSharper restore RedundantOverridenMember
    // ReSharper restore MemberCanBeProtected.Global
    // ReSharper restore UnusedMemberHierarchy.Global
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}
