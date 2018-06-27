using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Operator_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_show_custom_operators()
        {
            AssertPublicApi<ClassWithUnaryOperators>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithUnaryOperators
    {
        public ClassWithUnaryOperators() { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators op_Addition(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first, PublicApiGeneratorTests.Examples.ClassWithUnaryOperators second) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators op_Decrement(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators op_Division(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first, PublicApiGeneratorTests.Examples.ClassWithUnaryOperators second) { }
        public static bool op_False(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators op_Increment(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators op_LogicalNot(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators op_Multiply(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first, PublicApiGeneratorTests.Examples.ClassWithUnaryOperators second) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators op_OnesComplement(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators op_Subtraction(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first, PublicApiGeneratorTests.Examples.ClassWithUnaryOperators second) { }
        public static bool op_True(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
    }
}");
        }
    }

    namespace Examples
    {
        public class ClassWithUnaryOperators
        {
            public static ClassWithUnaryOperators operator +(ClassWithUnaryOperators first, ClassWithUnaryOperators second) => second;
            public static ClassWithUnaryOperators operator -(ClassWithUnaryOperators first, ClassWithUnaryOperators second) => second;
            public static ClassWithUnaryOperators operator /(ClassWithUnaryOperators first, ClassWithUnaryOperators second) => second;
            public static ClassWithUnaryOperators operator *(ClassWithUnaryOperators first, ClassWithUnaryOperators second) => second;
            public static ClassWithUnaryOperators operator !(ClassWithUnaryOperators first) => first;
            public static ClassWithUnaryOperators operator ~(ClassWithUnaryOperators first) => first;
            public static ClassWithUnaryOperators operator ++(ClassWithUnaryOperators first) => first;
            public static ClassWithUnaryOperators operator --(ClassWithUnaryOperators first) => first;
            public static bool operator true(ClassWithUnaryOperators first) => true;
            public static bool operator false(ClassWithUnaryOperators first) => false;
        }
    }
}
