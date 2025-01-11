using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Field_visibility : ApiGeneratorTestsBase
    {
        [Fact]
        public void Only_public_and_protected_fields_visible()
        {
            AssertPublicApi<ClassWithFields>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithFields
    {
        protected int protectedFieldIsVisible;
        protected int protectedInternalFieldIsVisible;
        public int publicFieldIsVisible;
        public ClassWithFields() { }
    }
}
""");
        }
    }

#pragma warning disable 169, 649

    namespace Examples
    {
        public class ClassWithFields
        {
            private readonly int privateFieldNotVisible;
            internal int internalFieldNotVisible;
            private protected int privateProtectedFieldNotVisible;

            protected int protectedFieldIsVisible;
            protected internal int protectedInternalFieldIsVisible;
            public int publicFieldIsVisible;
        }
    }

#pragma warning restore 169, 649
}
