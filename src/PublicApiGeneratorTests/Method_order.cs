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
}", opt => opt.BracingStyle = "C");
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
____public MethodOrdering() { }
____public void Method_AA() { }
____public void Method_AA<T>() { }
____public void Method_AA<T, U>() { }
____public void Method_BB(int foo) { }
____public void Method_BB(string bar) { }
____public void Method_BB(int foo, string bar) { }
____public void Method_I() { }
____public void Method_I(string arg) { }
____public void Method_i() { }
____public static void Method_CC() { }
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
