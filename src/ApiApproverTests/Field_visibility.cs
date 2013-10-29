using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Field_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Only_public_fields_visible()
        {
            AssertPublicApi<ClassWithFields>(
@"namespace ApiApproverTests.Examples
{
    public class ClassWithFields
    {
        protected int protectedFieldIsVisible;
        public int publicFieldIsVisible;
    }
}");
        }
    }

    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedField.Compiler
    namespace Examples
    {
        public class ClassWithFields
        {
            private int privateFieldNotVisible;
            internal int internalFieldNotVisile;

            protected int protectedFieldIsVisible;
            public int publicFieldIsVisible;
        }
    }
    // ReSharper restore InconsistentNaming
    // ReSharper restore UnusedField.Compiler
}