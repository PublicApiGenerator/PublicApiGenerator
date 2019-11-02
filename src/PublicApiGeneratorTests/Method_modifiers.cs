using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Method_modifiers : ApiGeneratorTestsBase
    {
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
        public void Should_output_unsafe_modifier()
        {
            AssertPublicApi<ClassWithUnsafeMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithUnsafeMethod
    {
        public ClassWithUnsafeMethod() { }
        public unsafe System.Void* DoSomething() { }
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

        public abstract class ClassWithAbstractMethod
        {
            public abstract void DoSomething();
        }

        public class ClassWithVirtualMethod
        {
            public virtual void DoSomething()
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
    }
    // ReSharper restore ClassWithVirtualMembersNeverInherited.Global
    // ReSharper restore RedundantOverridenMember
    // ReSharper restore MemberCanBeProtected.Global
    // ReSharper restore UnusedMemberHierarchy.Global
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}