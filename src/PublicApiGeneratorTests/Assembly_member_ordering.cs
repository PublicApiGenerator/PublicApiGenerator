using ApiApproverTests.Examples;
using ApiApproverTests.Examples_AA;
using ApiApproverTests.Examples_ZZ;
using Xunit;

namespace ApiApproverTests
{
    public class Assembly_member_ordering : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_in_known_order_and_alphabetically()
        {
            AssertPublicApi(new[]
            {
                typeof(AssemblyMember_Delegate2),
                typeof(IAssemblyMember_Interface1),
                typeof(AssemblyMember_Class2),
                typeof(IAssemblyMember_Interface2),
                typeof(AssemblyMember_Class1),
                typeof(AssemblyMember_Delegate1)
            },
@"namespace ApiApproverTests.Examples
{
    public class AssemblyMember_Class1
    {
        public AssemblyMember_Class1() { }
    }
    public class AssemblyMember_Class2
    {
        public AssemblyMember_Class2() { }
    }
    public delegate void AssemblyMember_Delegate1();
    public delegate void AssemblyMember_Delegate2();
    public interface IAssemblyMember_Interface1 { }
    public interface IAssemblyMember_Interface2 { }
}");
        }

        [Fact]
        public void Should_order_namespaces_alphabetically()
        {
            AssertPublicApi(new[]
            {
                typeof(AssemblyOrdering_1),
                typeof(AssemblyOrdering_2)
            },
@"namespace ApiApproverTests.Examples_AA
{
    public class AssemblyOrdering_2
    {
        public AssemblyOrdering_2() { }
    }
}
namespace ApiApproverTests.Examples_ZZ
{
    public class AssemblyOrdering_1
    {
        public AssemblyOrdering_1() { }
    }
}");
        }
    }

    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Global
    namespace Examples
    {
        public class AssemblyMember_Class2
        {
        }

        public class AssemblyMember_Class1
        {
        }

        public interface IAssemblyMember_Interface2
        {
        }

        public interface IAssemblyMember_Interface1
        {
        }

        public delegate void AssemblyMember_Delegate2();
        public delegate void AssemblyMember_Delegate1();
    }

    namespace Examples_AA
    {
        public class AssemblyOrdering_2
        {
        }
    }

    namespace Examples_ZZ
    {
        public class AssemblyOrdering_1
        {
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore InconsistentNaming
}