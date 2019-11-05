using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Method_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_show_public_methods()
        {
            AssertPublicApi<ClassWithPublicMethod>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPublicMethod
    {
        public ClassWithPublicMethod() { }
        public void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_show_protected_methods()
        {
            AssertPublicApi<ClassWithProtectedMethod>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedMethod
    {
        public ClassWithProtectedMethod() { }
        protected void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_not_show_internal_methods()
        {
            AssertPublicApi<ClassWithInternalMethod>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithInternalMethod
    {
        public ClassWithInternalMethod() { }
    }
}");
        }

        [Fact]
        public void Should_show_protected_internal_methods()
        {
            AssertPublicApi<ClassWithProtectedInternalMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithProtectedInternalMethod
    {
        public ClassWithProtectedInternalMethod() { }
        protected void DoSomething() { }
    }
}");
        }

        [Fact]
        public void Should_not_show_private_protected_methods()
        {
            AssertPublicApi<ClassWithPrivateProtectedMethod>(
                @"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPrivateProtectedMethod
    {
        public ClassWithPrivateProtectedMethod() { }
    }
}");
        }

        [Fact]
        public void Should_not_show_private_methods()
        {
            AssertPublicApi<ClassWithPrivateMethod>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithPrivateMethod
    {
        public ClassWithPrivateMethod() { }
    }
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

        public class ClassWithProtectedInternalMethod
        {
            protected internal void DoSomething()
            {
            }
        }

        public class ClassWithPrivateProtectedMethod
        {
            private protected void DoSomething()
            {
            }
        }

        public class ClassWithPrivateMethod
        {
            private void DoSomething()
            {
            }
        }
    }
    // ReSharper restore UnusedMember.Local
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}