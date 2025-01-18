using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Record : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_simple_record_as_class()
        {
            AssertPublicApi<User>("""
namespace PublicApiGeneratorTests.Examples
{
    public class User : System.IEquatable<PublicApiGeneratorTests.Examples.User>
    {
        public User(string login, string password) { }
        public string login { get; init; }
        public string password { get; init; }
    }
}
""");
        }

        [Fact]
        public void Should_output_simple_record_as_record()
        {
            AssertPublicApi<User>("""
namespace PublicApiGeneratorTests.Examples
{
    public record User : System.IEquatable<PublicApiGeneratorTests.Examples.User>
    {
        public User(string login, string password) { }
        public string login { get; init; }
        public string password { get; init; }
    }
}
""", opt => opt.TreatRecordsAsClasses = false);
        }
    }

    namespace Examples
    {
        public record User(string login, string password);
    }
}
