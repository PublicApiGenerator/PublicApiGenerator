using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Interface_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_interfaces_in_alphabetical_order()
        {
            AssertPublicApi(new[] { typeof(MM_Interface), typeof(ZZ_Interface), typeof(AA_Interface), typeof(I_Interface), typeof(i_Interface) },
@"namespace PublicApiGeneratorTests.Examples
{
    public interface AA_Interface { }
    public interface I_Interface { }
    public interface MM_Interface { }
    public interface ZZ_Interface { }
    public interface i_Interface { }
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
        public interface I_Interface
        {
        }
        public interface i_Interface
        {
        }
    }
    // ReSharper restore InconsistentNaming
}