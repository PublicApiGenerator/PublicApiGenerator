using PublicApiGeneratorTests.Examples;
using System;
using System.Collections.Generic;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Dynamics : ApiGeneratorTestsBase
    {
        [Fact(Skip = "Needs investigation")]
        public void Should_output_dynamic()
        {
            AssertPublicApi<ClassWithDynamic>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithDynamic : System.Collections.Generic.List<dynamic>
    {
        public class ClassWithDynamic2 : System.Collections.Generic.List<dynamic?> { }
        public ClassWithDynamic() { }
        public dynamic DoIt1(dynamic p) { }
        public dynamic? DoIt2(dynamic? p) { }
    }
}");
        }

        // TODO: Enum with flags + undefined value
        // Not supported by Cecil?
    }

    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public class ClassWithDynamic : List<dynamic>
        {
            public class ClassWithDynamic2 : List<dynamic?> { }

            public dynamic DoIt1(dynamic p) { throw null; }

            public dynamic? DoIt2(dynamic? p) { throw null; }
        }
    }
    // ReSharper restore UnusedMember.Global
}
