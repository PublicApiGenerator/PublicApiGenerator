using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Interface_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_interfaces_in_alphabetical_order()
        {
            AssertPublicApi(new[] { typeof(MM_Interface), typeof(ZZ_Interface), typeof(AA_Interface) },
@"namespace ApiApproverTests.Examples
{
    public interface AA_Interface { }
}
namespace ApiApproverTests.Examples
{
    public interface MM_Interface { }
}
namespace ApiApproverTests.Examples
{
    public interface ZZ_Interface { }
}");
        }
    }

    // ReSharper disable InconsistentNaming
    namespace Examples
    {
        public interface MM_Interface
        {
        }

        public interface ZZ_Interface
        {
        }

        public interface AA_Interface
        {
        }
    }
    // ReSharper restore InconsistentNaming
}