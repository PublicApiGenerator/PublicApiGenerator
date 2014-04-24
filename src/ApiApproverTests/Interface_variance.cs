using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Interface_variance : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_contravariance()
        {
            AssertPublicApi(typeof(IInterfaceWithContravariance<>),
@"namespace ApiApproverTests.Examples
{
    public interface IInterfaceWithContravariance<in T>
    {
        void Method(T item);
    }
}");
        }

        [Fact]
        public void Should_output_covariance()
        {
            AssertPublicApi(typeof(IInterfaceWithCovariance<>),
@"namespace ApiApproverTests.Examples
{
    public interface IInterfaceWithCovariance<out T>
    {
        T Method();
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public interface IInterfaceWithContravariance<in T>
        {
            void Method(T item);
        }

        public interface IInterfaceWithCovariance<out T>
        {
            T Method();
        }
    }
    // ReSharper restore UnusedMember.Global
}