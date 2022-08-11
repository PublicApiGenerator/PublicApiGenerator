using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Record : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_simple_record()
        {
            AssertPublicApi<User>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class User : System.IEquatable<PublicApiGeneratorTests.Examples.User>
    {
        public User(string login, string password) { }
        public string login { get; set; }
        public string password { get; set; }
    }
}");
        }
    }

    namespace Examples
    {
        public record User(string login, string password);
    }
}
