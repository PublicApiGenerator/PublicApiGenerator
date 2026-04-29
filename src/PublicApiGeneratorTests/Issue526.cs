using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    // https://github.com/PublicApiGenerator/PublicApiGenerator/issues/526
    public class Issue526 : ApiGeneratorTestsBase
    {
        [Fact]
        public void Issue526_Should_Work()
        {
            AssertPublicApi(typeof(X),
"""
namespace PublicApiGeneratorTests.Examples
{
    public static class X
    {
        extension<T, UNKNOWN>(System.Collections.Generic.IList<T> some)
            where T : UNKNOWN
        {
            public System.Collections.Generic.IList<UNKNOWN> ThisDoesNotWork() { }
        }
    }
}
""");
        }

    }


    namespace Examples
    {
        public static class X
        {
            extension<T, OtherT>(IList<T> some) where T : OtherT
            {
                public IList<OtherT> ThisDoesNotWork()
                {
                    // Some not relevant implementation
                    return new List<OtherT>();
                }
            }
        }
    }
}
