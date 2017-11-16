using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Class_modifiers : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_abstract_modifier()
        {
            AssertPublicApi<AbstractClass>(
@"namespace ApiApproverTests.Examples
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
@"namespace ApiApproverTests.Examples
{
    public class static StaticClass { }
}");
        }

        [Fact]
        public void Should_output_sealed_modifier()
        {
            AssertPublicApi<SealedClass>(
@"namespace ApiApproverTests.Examples
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