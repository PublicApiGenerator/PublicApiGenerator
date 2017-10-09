using Microsoft.Whitelisted;
using Xunit;

namespace ApiApproverTests
{
    public class Namespaces_whitelisting : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_allow_namespace_whitelisting()
        {
            AssertPublicApi(new[] {typeof(Simple1), typeof(Simple2)},
                @"namespace Microsoft.Whitelisted
{
    public class Simple1
    {
        public Simple1() { }
    }
    public class Simple2
    {
        public Simple2() { }
    }
}");
        }
    }
}

namespace Microsoft.Whitelisted
{
    public class Simple1
    {
    }

    public class Simple2
    {
    }
}