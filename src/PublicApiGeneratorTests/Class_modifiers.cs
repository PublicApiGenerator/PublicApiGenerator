using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Class_modifiers : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_abstract_modifier()
        {
            AssertPublicApi<AbstractClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public abstract class AbstractClass
    {
        protected AbstractClass() { }
    }
}");
        }

        [Fact]
        public void Should_output_static_modifier()
        {
            AssertPublicApi(typeof(StaticClass),
@"namespace PublicApiGeneratorTests.Examples
{
    public static class StaticClass { }
}");
        }

        [Fact]
        public void Should_output_sealed_modifier()
        {
            AssertPublicApi<SealedClass>(
@"namespace PublicApiGeneratorTests.Examples
{
    public sealed class SealedClass
    {
        public SealedClass() { }
    }
}");
        }
    }

    // ReSharper disable ClassNeverInstantiated.Global
    namespace Examples
    {
        public abstract class AbstractClass
        {
        }

        public static class StaticClass
        {
        }

        public sealed class SealedClass
        {
        }
    }
    // ReSharper restore ClassNeverInstantiated.Global
}
