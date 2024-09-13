using PublicApiGeneratorTests.Examples;
using PublicApiGeneratorTests.Examples_AA;
using PublicApiGeneratorTests.Examples_ZZ;

namespace PublicApiGeneratorTests
{
    public class Assembly_member_ordering : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_in_known_order_and_alphabetically()
        {
            AssertPublicApi(
            [
                typeof(AssemblyMember_Delegate2),
                typeof(AssemblyMember_IDelegate2),
                typeof(AssemblyMember_ClassI),
                typeof(AssemblyMember_Classi),
                typeof(IAssemblyMember_Interface1),
                typeof(IAssemblyMember_interface1),
                typeof(AssemblyMember_Class2),
                typeof(IAssemblyMember_Interface2),
                typeof(IAssemblyMember_interface2),
                typeof(AssemblyMember_Class1),
                typeof(AssemblyMember_Delegate1),
                typeof(AssemblyMember_iDelegate1)
            ],
@"namespace PublicApiGeneratorTests.Examples
{
    public class AssemblyMember_Class1
    {
        public AssemblyMember_Class1() { }
    }
    public class AssemblyMember_Class2
    {
        public AssemblyMember_Class2() { }
    }
    public class AssemblyMember_ClassI
    {
        public AssemblyMember_ClassI() { }
    }
    public class AssemblyMember_Classi
    {
        public AssemblyMember_Classi() { }
    }
    public delegate void AssemblyMember_Delegate1();
    public delegate void AssemblyMember_Delegate2();
    public delegate void AssemblyMember_IDelegate2();
    public delegate void AssemblyMember_iDelegate1();
    public interface IAssemblyMember_Interface1 { }
    public interface IAssemblyMember_Interface2 { }
    public interface IAssemblyMember_interface1 { }
    public interface IAssemblyMember_interface2 { }
}");
        }

        [Fact]
        public void Should_order_namespaces_alphabetically()
        {
            AssertPublicApi(
            [
                typeof(AssemblyOrdering_1),
                typeof(AssemblyOrdering_2),
                typeof(Examples_I.AssemblyOrdering_2),
                typeof(Examples_i.AssemblyOrdering_1),
            ],
@"namespace PublicApiGeneratorTests.Examples_AA
{
    public class AssemblyOrdering_2
    {
        public AssemblyOrdering_2() { }
    }
}
namespace PublicApiGeneratorTests.Examples_I
{
    public class AssemblyOrdering_2
    {
        public AssemblyOrdering_2() { }
    }
}
namespace PublicApiGeneratorTests.Examples_ZZ
{
    public class AssemblyOrdering_1
    {
        public AssemblyOrdering_1() { }
    }
}
namespace PublicApiGeneratorTests.Examples_i
{
    public class AssemblyOrdering_1
    {
        public AssemblyOrdering_1() { }
    }
}");
        }

        [Fact]
        public void Should_order_namespaces_alphabetically2()
        {
            AssertPublicApi(
            [
                typeof(A.D),
                typeof(A.C.B),
            ],
@"namespace PublicApiGeneratorTests.A
{
    public class D
    {
        public D() { }
    }
}
namespace PublicApiGeneratorTests.A.C
{
    public class B
    {
        public B() { }
    }
}");
        }
    }

    namespace Examples
    {
        public class AssemblyMember_Class2
        {
        }

        public class AssemblyMember_Class1
        {
        }

        public class AssemblyMember_ClassI
        {
        }

        public class AssemblyMember_Classi
        {
        }

        public interface IAssemblyMember_Interface2
        {
        }

        public interface IAssemblyMember_interface2
        {
        }

        public interface IAssemblyMember_Interface1
        {
        }

        public interface IAssemblyMember_interface1
        {
        }

        public delegate void AssemblyMember_Delegate2();
        public delegate void AssemblyMember_Delegate1();
        public delegate void AssemblyMember_IDelegate2();
        public delegate void AssemblyMember_iDelegate1();
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

    namespace Examples_I
    {
        public class AssemblyOrdering_2
        {
        }
    }

    namespace Examples_i
    {
        public class AssemblyOrdering_1
        {
        }
    }

    namespace A
    {
        public class D;
    }

    namespace A.C
    {
        public class B;
    }
}
