using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Issue301 : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_Render_Semicolon_Properly_1()
        {
            AssertPublicApi(typeof(Interface1),
"""
namespace PublicApiGeneratorTests.Examples
{
    public interface Interface1
    {
        void ConfigureMapping<T>()
            where T :  class;
    }
}
""");
        }

        [Fact]
        public void Should_Render_Semicolon_Properly_2()
        {
            AssertPublicApi(typeof(Interface2),
"""
namespace PublicApiGeneratorTests.Examples
{
    public interface Interface2
    {
        void ConfigureMapping<T, U>()
            where T :  class;
    }
}
""");
        }

        [Fact]
        public void Should_Render_Semicolon_Properly_3()
        {
            AssertPublicApi(typeof(Interface3),
"""
namespace PublicApiGeneratorTests.Examples
{
    public interface Interface3
    {
        void ConfigureMapping<T, U, V>()
            where T :  class;
    }
}
""");
        }

        [Fact]
        public void Should_Render_Semicolon_Properly_4()
        {
            AssertPublicApi(typeof(Interface4),
"""
namespace PublicApiGeneratorTests.Examples
{
    public interface Interface4
    {
        void ConfigureMapping<T, U, V, K>()
            where T :  class
            where K :  struct;
    }
}
""");
        }
    }


    namespace Examples
    {
        public interface Interface1
        {
            void ConfigureMapping<T>()
                where T : class;
        }

        public interface Interface2
        {
            void ConfigureMapping<T, U>()
                where T : class;
        }

        public interface Interface3
        {
            void ConfigureMapping<T, U, V>()
                where T : class;
        }

        public interface Interface4
        {
            void ConfigureMapping<T, U, V, K>()
                where T : class
                where K : struct;
        }
    }
}
