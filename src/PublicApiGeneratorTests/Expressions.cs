using PublicApiGeneratorTests.Examples;
using System.Linq.Expressions;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Expressions : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_write_expression()
        {
            AssertPublicApi(typeof(ClassWithExpressions<>),
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithExpressions<TSourceType>
    {
        public ClassWithExpressions(params System.Linq.Expressions.Expression<System.Func<int, object>>[] expr1) { }
        public ClassWithExpressions(params System.Linq.Expressions.Expression<System.Func<TSourceType, object?>>[] expr2) { }
        public ClassWithExpressions(System.Linq.Expressions.Expression<System.Func<TSourceType, object>?> expr3) { }
        public ClassWithExpressions(System.Linq.Expressions.Expression<System.Func<int, object>>? expr4) { }
    }
}");
        }
    }

    namespace Examples
    {
        public class ClassWithExpressions<TSourceType>
        {
            public ClassWithExpressions(params Expression<Func<int, object>>[] expr1) { }
            public ClassWithExpressions(params Expression<Func<TSourceType, object?>>[] expr2) { }
            public ClassWithExpressions(Expression<Func<TSourceType, object>?> expr3) { }
            public ClassWithExpressions(Expression<Func<int, object>>? expr4) { }
        }
    }
}
