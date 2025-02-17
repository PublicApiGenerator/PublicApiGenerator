using Microsoft.Whitelisted;
using System.Whitelisted;

namespace PublicApiGeneratorTests
{
    public class Namespaces_whitelisting : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_allow_microsoft_namespace_whitelisting()
        {
            AssertPublicApi([typeof(Simple1), typeof(Simple2)], """
namespace Microsoft.Whitelisted
{
    public class Simple1
    {
        public Simple1() { }
        public void Simple() { }
    }
    public class Simple2
    {
        public Simple2() { }
        public void Simple() { }
    }
}
""", opt => opt.AllowNamespacePrefixes = ["Microsoft.Whitelisted"]);
        }

        [Fact]
        public void Should_filter_microsoft_namespace()
        {
            AssertPublicApi([typeof(Simple1), typeof(Simple2)], "");
        }

        [Fact]
        public void Should_allow_system_namespace_whitelisting()
        {

            AssertPublicApi([typeof(System1), typeof(System2)], """
namespace System.Whitelisted
{
    public class System1
    {
        public System1() { }
        public void System() { }
    }
    public class System2
    {
        public System2() { }
        public void System() { }
    }
}
""", opt => opt.AllowNamespacePrefixes = ["System.Whitelisted"]);
        }

        [Fact]
        public void Should_filter_system_namespace()
        {
            AssertPublicApi([typeof(System1), typeof(System2)], "");
        }
    }
}

namespace Microsoft.Whitelisted
{
    public class Simple1
    {
        public void Simple() { }
    }

    public class Simple2
    {
        public void Simple() { }
    }
}

namespace System.Whitelisted
{
    public class System1
    {
        public void System() { }
    }

    public class System2
    {
        public void System() { }
    }
}
