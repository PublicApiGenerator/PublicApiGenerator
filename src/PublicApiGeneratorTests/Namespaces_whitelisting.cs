using Microsoft.Whitelisted;
using System.Whitelisted;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Namespaces_whitelisting : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_allow_microsoft_namespace_whitelisting()
        {
            var options = new DefaultApiGeneratorOptions
            {
                WhitelistedNamespacePrefixes = new[] {"Microsoft.Whitelisted"}
            };

            AssertPublicApi(new[] { typeof(Simple1), typeof(Simple2) },
                @"namespace Microsoft.Whitelisted
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
}", options);
        }

        [Fact]
        public void Should_filter_microsoft_namespace()
        {
            AssertPublicApi(new[] { typeof(Simple1), typeof(Simple2) },
                @"namespace Microsoft.Whitelisted
{
    public class Simple1 { }
    public class Simple2 { }
}");
        }

        [Fact]
        public void Should_allow_system_namespace_whitelisting()
        {
            var options = new DefaultApiGeneratorOptions
            {
                WhitelistedNamespacePrefixes = new[] { "System.Whitelisted" }
            };

            AssertPublicApi(new[] { typeof(System1), typeof(System2)},
                @"namespace System.Whitelisted
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
}", options);
        }

        [Fact]
        public void Should_filter_system_namespace()
        {
            AssertPublicApi(new[] { typeof(System1), typeof(System2) },
                @"namespace System.Whitelisted
{
    public class System1 { }
    public class System2 { }
}");
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
