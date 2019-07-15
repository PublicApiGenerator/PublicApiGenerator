using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Field_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Only_public_and_protected_fields_visible()
        {
            AssertPublicApi<ClassWithFields>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithFields
    {
        protected int protectedFieldIsVisible;
        protected int protectedInternalFieldIsVisible;
        public int publicFieldIsVisible;
        public ClassWithFields() { }
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
            internal int internalFieldNotVisible;

            protected int protectedFieldIsVisible;
            protected internal int protectedInternalFieldIsVisible;
            public int publicFieldIsVisible;
        }
    }
    // ReSharper restore UnusedMember.Global
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore InconsistentNaming
    // ReSharper restore UnusedField.Compiler

#pragma warning restore 169, 649
}