using ApiApproverTests.Examples;
using Xunit;

namespace ApiApproverTests
{
    public class Field_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Only_public_and_protected_fields_visible()
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

#pragma warning disable 169, 649

    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedField.Compiler
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
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
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore InconsistentNaming
    // ReSharper restore UnusedField.Compiler

#pragma warning restore 169, 649
}