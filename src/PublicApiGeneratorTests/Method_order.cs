using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Method_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_be_alphabetical_order()
        {
            AssertPublicApi<MethodOrdering>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodOrdering
    {
        public MethodOrdering() { }
        public void Method_AA() { }
        public void Method_AA<T>() { }
        public void Method_AA<T, U>() { }
        public void Method_BB(int foo) { }
        public void Method_BB(string bar) { }
        public void Method_BB(int foo, string bar) { }
        public void Method_I() { }
        public void Method_I(string arg) { }
        public void Method_i() { }
        public static void Method_CC() { }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class MethodOrdering
        {
            public static void Method_CC()
            {
            }

            public void Method_BB(int foo, string bar)
            {
            }

            public void Method_BB(string bar)
            {
            }

            public void Method_BB(int foo)
            {
            }

            public void Method_AA<T, U>()
            {
            }

            public void Method_AA<T>()
            {
            }

            public void Method_AA()
            {
            }

            public void Method_I(string arg)
            {
            }

            public void Method_I()
            {
            }

            public void Method_i()
            {
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}
