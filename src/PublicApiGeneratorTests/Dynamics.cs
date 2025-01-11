using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Dynamics : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_dynamic()
        {
            AssertPublicApi<ClassWithDynamic>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithDynamic : System.Collections.Generic.List<dynamic>
    {
        public ClassWithDynamic() { }
        public dynamic DoIt1(dynamic p) { }
        public dynamic? DoIt2(dynamic? p) { }
        public System.Collections.Generic.Dictionary<dynamic, object> DoIt3(System.Collections.Generic.Dictionary<dynamic, object>? p) { }
        public class ClassWithDynamic2 : System.Collections.Generic.List<dynamic?>
        {
            public ClassWithDynamic2() { }
        }
        public class ClassWithDynamic3 : System.Collections.Generic.Dictionary<dynamic, object>
        {
            public ClassWithDynamic3() { }
        }
        public class ClassWithDynamic4 : System.Collections.Generic.Dictionary<object, dynamic>
        {
            public ClassWithDynamic4() { }
        }
    }
}
""");
        }
    }

    namespace Examples
    {
        public class ClassWithDynamic : List<dynamic>
        {
            public class ClassWithDynamic2 : List<dynamic?> { }

            public class ClassWithDynamic3 : Dictionary<dynamic, object> { }

            public class ClassWithDynamic4 : Dictionary<object, dynamic> { }

            public dynamic DoIt1(dynamic p) { throw null; }

            public dynamic? DoIt2(dynamic? p) { throw null; }

            public Dictionary<dynamic, object> DoIt3(Dictionary<dynamic, object>? p) { throw null; }
        }
    }
}
