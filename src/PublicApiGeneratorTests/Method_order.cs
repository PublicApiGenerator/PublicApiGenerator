using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Method_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_be_alphabetical_order()
        {
            AssertPublicApi<MethodOrdering>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodOrdering
    {
        public MethodOrdering() { }
        public void Method_AA() { }
        public void Method_BB() { }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class MethodOrdering
        {
            public void Method_BB()
            {
            }

            public void Method_AA()
            {
            }
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
}