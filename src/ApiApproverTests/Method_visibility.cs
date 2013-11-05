using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Method_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_show_public_methods()
        {
            AssertPublicApi<ClassWithPublicMethod>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithPublicMethod
    {
        public void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_show_protected_methods()
        {
            AssertPublicApi<ClassWithProtectedMethod>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithProtectedMethod
    {
        protected void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_not_show_internal_methods()
        {
            AssertPublicApi<ClassWithInternalMethod>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithInternalMethod { }
}");
        }

        [Fact]
        public void Should_not_show_private_methods()
        {
            AssertPublicApi<ClassWithPrivateMethod>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithPrivateMethod { }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Local
    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public class ClassWithPublicMethod
        {
            public void DoSomething()
            {
            }
        }

        public class ClassWithProtectedMethod
        {
            protected void DoSomething()
            {
            }
        }

        public class ClassWithInternalMethod
        {
            internal void DoSomething()
            {
            }
        }

        public class ClassWithPrivateMethod
        {
            private void Method()
            {
            }
        }
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}