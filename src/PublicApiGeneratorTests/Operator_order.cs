using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Operator_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_sort_unary_operators()
        {
            AssertPublicApi<ClassWithUnaryOperators>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithUnaryOperators
    {
        public ClassWithUnaryOperators() { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators operator !(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators operator +(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators operator ++(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators operator -(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators operator --(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static bool operator false(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static bool operator true(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
        public static PublicApiGeneratorTests.Examples.ClassWithUnaryOperators operator ~(PublicApiGeneratorTests.Examples.ClassWithUnaryOperators first) { }
    }
}
""");
        }

        [Fact]
        public void Should_sort_binary_operators()
        {
            AssertPublicApi<ClassWithBinaryOperators>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithBinaryOperators
    {
        public ClassWithBinaryOperators() { }
        public static PublicApiGeneratorTests.Examples.ClassWithBinaryOperators operator %(PublicApiGeneratorTests.Examples.ClassWithBinaryOperators first, PublicApiGeneratorTests.Examples.ClassWithBinaryOperators second) { }
        public static PublicApiGeneratorTests.Examples.ClassWithBinaryOperators operator &(PublicApiGeneratorTests.Examples.ClassWithBinaryOperators first, PublicApiGeneratorTests.Examples.ClassWithBinaryOperators second) { }
        public static PublicApiGeneratorTests.Examples.ClassWithBinaryOperators operator *(PublicApiGeneratorTests.Examples.ClassWithBinaryOperators first, PublicApiGeneratorTests.Examples.ClassWithBinaryOperators second) { }
        public static PublicApiGeneratorTests.Examples.ClassWithBinaryOperators operator +(PublicApiGeneratorTests.Examples.ClassWithBinaryOperators first, PublicApiGeneratorTests.Examples.ClassWithBinaryOperators second) { }
        public static PublicApiGeneratorTests.Examples.ClassWithBinaryOperators operator -(PublicApiGeneratorTests.Examples.ClassWithBinaryOperators first, PublicApiGeneratorTests.Examples.ClassWithBinaryOperators second) { }
        public static PublicApiGeneratorTests.Examples.ClassWithBinaryOperators operator /(PublicApiGeneratorTests.Examples.ClassWithBinaryOperators first, PublicApiGeneratorTests.Examples.ClassWithBinaryOperators second) { }
        public static PublicApiGeneratorTests.Examples.ClassWithBinaryOperators operator <<(PublicApiGeneratorTests.Examples.ClassWithBinaryOperators first, int second) { }
        public static PublicApiGeneratorTests.Examples.ClassWithBinaryOperators operator >>(PublicApiGeneratorTests.Examples.ClassWithBinaryOperators first, int second) { }
        public static PublicApiGeneratorTests.Examples.ClassWithBinaryOperators operator ^(PublicApiGeneratorTests.Examples.ClassWithBinaryOperators first, PublicApiGeneratorTests.Examples.ClassWithBinaryOperators second) { }
        public static PublicApiGeneratorTests.Examples.ClassWithBinaryOperators operator |(PublicApiGeneratorTests.Examples.ClassWithBinaryOperators first, PublicApiGeneratorTests.Examples.ClassWithBinaryOperators second) { }
    }
}
""");
        }

        [Fact]
        public void Should_sort_comparison_operators()
        {
            AssertPublicApi<ClassWithComparisonOperators>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithComparisonOperators
    {
        public ClassWithComparisonOperators() { }
        public static bool operator !=(PublicApiGeneratorTests.Examples.ClassWithComparisonOperators first, PublicApiGeneratorTests.Examples.ClassWithComparisonOperators second) { }
        public static bool operator <(PublicApiGeneratorTests.Examples.ClassWithComparisonOperators first, PublicApiGeneratorTests.Examples.ClassWithComparisonOperators second) { }
        public static bool operator <=(PublicApiGeneratorTests.Examples.ClassWithComparisonOperators first, PublicApiGeneratorTests.Examples.ClassWithComparisonOperators second) { }
        public static bool operator ==(PublicApiGeneratorTests.Examples.ClassWithComparisonOperators first, PublicApiGeneratorTests.Examples.ClassWithComparisonOperators second) { }
        public static bool operator >(PublicApiGeneratorTests.Examples.ClassWithComparisonOperators first, PublicApiGeneratorTests.Examples.ClassWithComparisonOperators second) { }
        public static bool operator >=(PublicApiGeneratorTests.Examples.ClassWithComparisonOperators first, PublicApiGeneratorTests.Examples.ClassWithComparisonOperators second) { }
    }
}
""");
        }
    }

    namespace Examples
    {
        public class ClassWithUnaryOperators
        {
            public static ClassWithUnaryOperators operator +(ClassWithUnaryOperators first) => first;
            public static ClassWithUnaryOperators operator -(ClassWithUnaryOperators first) => first;
            public static ClassWithUnaryOperators operator !(ClassWithUnaryOperators first) => first;
            public static ClassWithUnaryOperators operator ~(ClassWithUnaryOperators first) => first;
            public static ClassWithUnaryOperators operator ++(ClassWithUnaryOperators first) => first;
            public static ClassWithUnaryOperators operator --(ClassWithUnaryOperators first) => first;
            public static bool operator true(ClassWithUnaryOperators first) => true;
            public static bool operator false(ClassWithUnaryOperators first) => false;
        }

        public class ClassWithBinaryOperators
        {
            public static ClassWithBinaryOperators operator +(ClassWithBinaryOperators first, ClassWithBinaryOperators second) => first;
            public static ClassWithBinaryOperators operator -(ClassWithBinaryOperators first, ClassWithBinaryOperators second) => first;
            public static ClassWithBinaryOperators operator /(ClassWithBinaryOperators first, ClassWithBinaryOperators second) => first;
            public static ClassWithBinaryOperators operator *(ClassWithBinaryOperators first, ClassWithBinaryOperators second) => first;
            public static ClassWithBinaryOperators operator %(ClassWithBinaryOperators first, ClassWithBinaryOperators second) => first;
            public static ClassWithBinaryOperators operator &(ClassWithBinaryOperators first, ClassWithBinaryOperators second) => first;
            public static ClassWithBinaryOperators operator |(ClassWithBinaryOperators first, ClassWithBinaryOperators second) => first;
            public static ClassWithBinaryOperators operator ^(ClassWithBinaryOperators first, ClassWithBinaryOperators second) => first;
            public static ClassWithBinaryOperators operator <<(ClassWithBinaryOperators first, int second) => first;
            public static ClassWithBinaryOperators operator >>(ClassWithBinaryOperators first, int second) => first;
        }

#pragma warning disable 660,661
        public class ClassWithComparisonOperators
#pragma warning restore 660,661
        {
            public static bool operator ==(ClassWithComparisonOperators first, ClassWithComparisonOperators second) => true;
            public static bool operator !=(ClassWithComparisonOperators first, ClassWithComparisonOperators second) => true;
            public static bool operator <(ClassWithComparisonOperators first, ClassWithComparisonOperators second) => true;
            public static bool operator >(ClassWithComparisonOperators first, ClassWithComparisonOperators second) => true;
            public static bool operator <=(ClassWithComparisonOperators first, ClassWithComparisonOperators second) => true;
            public static bool operator >=(ClassWithComparisonOperators first, ClassWithComparisonOperators second) => true;
        }
    }
}
