namespace PublicApiGeneratorTests;

public partial class CecilEx
{
    public class The_AreSame_Method : ApiGeneratorTestsBase
    {
        [Fact]
        public void Handles_TypeReferences_Correctly()
        {
            AssertPublicApi(typeof(GenericClass<>), """
namespace PublicApiGeneratorTests
{
    public class GenericClass<TA> : PublicApiGeneratorTests.GenericClass<TA, TA>
        where TA : System.IComparable<TA>
    {
        public GenericClass(TA a, double data = 0) { }
        public GenericClass(TA a, TA b, double data = 0) { }
    }
}
""");
        }
    }
}

public class GenericClass<TA> : GenericClass<TA, TA>
    where TA : IComparable<TA>
{
    public GenericClass(TA a, double data = 0)
        : base(a, data)
    {
    }

    public GenericClass(TA a, TA b, double data = 0)
        : base(a, b, data)
    {
    }
}

public class GenericClass<TA, TB>
    where TA : IComparable<TA>
    where TB : IComparable<TB>
{
    public GenericClass(TA a, double data = 0)
    {
    }

    public GenericClass(TA a, TA b, double data = 0)
    {
    }
}
