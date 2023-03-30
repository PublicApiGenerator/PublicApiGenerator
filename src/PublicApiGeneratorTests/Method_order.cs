using PublicApiGeneratorTests.Examples;

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

        [Fact]
        public void Should_be_alphabetical_order_with_block_bracing_style()
        {
            AssertPublicApi<MethodOrdering>(
@"namespace PublicApiGeneratorTests.Examples {
    public class MethodOrdering {
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
}", opt => opt.BracingStyle = "Block");
        }

        [Fact]
        public void Should_be_alphabetical_order_with_custom_indent_string1()
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
}", opt => opt.IndentString = " ");
        }

        [Fact]
        public void Should_be_alphabetical_order_with_custom_indent_string2()
        {
            AssertPublicApi<MethodOrdering>(
@"namespace PublicApiGeneratorTests.Examples
{
__
__public class MethodOrdering
__{
____public MethodOrdering()
____{
____}
____public void Method_AA()
____{
____}
____public void Method_AA<T>()
____{
____}
____public void Method_AA<T, U>()
____{
____}
____public void Method_BB(int foo)
____{
____}
____public void Method_BB(string bar)
____{
____}
____public void Method_BB(int foo, string bar)
____{
____}
____public void Method_I()
____{
____}
____public void Method_I(string arg)
____{
____}
____public void Method_i()
____{
____}
____public static void Method_CC()
____{
____}
__}
}", opt => opt.IndentString = "__");
        }
    }

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
}
