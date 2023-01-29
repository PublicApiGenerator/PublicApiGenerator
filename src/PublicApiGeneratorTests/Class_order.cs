using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Class_order : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_classes_in_alphabetical_order()
        {
            AssertPublicApi(new[] { typeof(MM_Class), typeof(ZZ_Class), typeof(AA_Class), typeof(I_Class), typeof(i_Class) },
@"namespace PublicApiGeneratorTests.Examples
{
   public class AA_Class
    {
        public AA_Class() { }
    }
    public class I_Class
    {
        public I_Class() { }
    }
    public class MM_Class
    {
        public MM_Class() { }
    }
    public class ZZ_Class
    {
        public ZZ_Class() { }
    }
    public class i_Class
    {
        public i_Class() { }
    }
}");
        }
    }

    // ReSharper disable InconsistentNaming
    namespace Examples
    {
        public class MM_Class
        {
        }

        public class ZZ_Class
        {
        }

        public class AA_Class
        {
        }

        public class I_Class
        {
        }

        public class i_Class
        {
        }
    }
    // ReSharper restore InconsistentNaming
}