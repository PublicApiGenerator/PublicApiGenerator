using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Method_flags : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_static_modifier()
        {
            AssertPublicApi<ClassWithStaticMethod>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithStaticMethod
    {
        public static void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_abstract_modifier()
        {
            AssertPublicApi<ClassWithAbstractMethod>(
@"namespace ApiApproverTests.Examples
{
    public abstract class ClassWithAbstractMethod
    {
        public abstract void DoSomething();
    }
}");
        }

        [Fact]
        public void Should_output_virtual_modifier()
        {
            AssertPublicApi<ClassWithVirtualMethod>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithVirtualMethod
    {
        public virtual void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_output_override_modifier()
        {
         AssertPublicApi<ClassWithOverridingMethod>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithOverridingMethod : ApiApproverTests.Examples.ClassWithVirtualMethod
    {
        public override void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_allow_overriding_object_methods()
        {
            AssertPublicApi<ClassWithMethodOverridingObjectMethod>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithMethodOverridingObjectMethod
    {
        public override string ToString() { }
    }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedMemberHiearchy.Global
    // ReSharper disable MemberCanBeProtected.Global
    // ReSharper disable RedundantOverridenMember
    namespace Examples
    {
        public class ClassWithStaticMethod
        {
            public static void DoSomething()
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
    }
    // ReSharper restore RedundantOverridenMember
    // ReSharper restore MemberCanBeProtected.Global
    // ReSharper restore UnusedMemberHiearchy.Global
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
}