using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Class_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Public_class_is_visible()
        {
            AssertPublicApi<PublicClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class PublicClass
    {
        public PublicClass() { }
    }
}");
        }

        [Fact]
        public void Internal_class_is_not_visible()
        {
            AssertPublicApi<InternalClass>(string.Empty);
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public class PublicClass
        {
        }

        internal class InternalClass
        {
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
}