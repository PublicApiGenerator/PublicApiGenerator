using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class NativeIntegers : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_native_integers()
        {
#if NET7_0_OR_GREATER
            AssertPublicApi<ClassWithNativeIntegers>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithNativeIntegers
    {
        public nint Add1(nint a, nint b) { }
        public nuint Add2(nuint a, nuint b) { }
        public nint Add3(nint a, nint b) { }
        public nuint Add4(nuint a, nuint b) { }
    }
}
""");
#else
            AssertPublicApi<ClassWithNativeIntegers>("""
namespace PublicApiGeneratorTests.Examples
{
    public class ClassWithNativeIntegers
    {
        public nint Add1(nint a, nint b) { }
        public nuint Add2(nuint a, nuint b) { }
        public System.IntPtr Add3(System.IntPtr a, System.IntPtr b) { }
        public System.UIntPtr Add4(System.UIntPtr a, System.UIntPtr b) { }
    }
}
""");
#endif
        }
    }

    namespace Examples
    {
        public class ClassWithNativeIntegers
        {
            private ClassWithNativeIntegers() { }

            public nint Add1(nint a, nint b) => a + b;

            public nuint Add2(nuint a, nuint b) => a + b;

            public IntPtr Add3(IntPtr a, IntPtr b) => throw null;

            public UIntPtr Add4(UIntPtr a, UIntPtr b) => throw null;
        }
    }
}
