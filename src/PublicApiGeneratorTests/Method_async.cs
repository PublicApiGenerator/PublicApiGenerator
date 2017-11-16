using System.Threading.Tasks;
using PublicApiGeneratorTests.Examples;
using Xunit;

namespace PublicApiGeneratorTests
{
    public class Method_async : ApiGeneratorTestsBase
    {
        [Fact]
        public void Should_output_async_void_method()
        {
            AssertPublicApi<MethodAsyncVoid>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodAsyncVoid
    {
        public MethodAsyncVoid() { }
        public void AsyncMethod() { }
    }
}");
        }

        [Fact]
        public void Should_output_async_method()
        {
            AssertPublicApi<MethodAsync>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodAsync
    {
        public MethodAsync() { }
        public System.Threading.Tasks.Task AsyncMethod() { }
    }
}");
        }

        [Fact]
        public void Should_output_async_method_without_keyword()
        {
            AssertPublicApi<MethodAsyncWithoutAsyncKeyword>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodAsyncWithoutAsyncKeyword
    {
        public MethodAsyncWithoutAsyncKeyword() { }
        public System.Threading.Tasks.Task AsyncMethod() { }
    }
}");
        }

        [Fact]
        public void Should_output_async_method_with_return_value()
        {
            AssertPublicApi<MethodAsyncReturnValue>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodAsyncReturnValue
    {
        public MethodAsyncReturnValue() { }
        public System.Threading.Tasks.Task<string> AsyncMethod() { }
    }
}");
        }

        [Fact]
        public void Should_output_async_method_without_keyword_with_return_value()
        {
            AssertPublicApi<MethodAsyncReturnValueWithoutAsyncKeyword>(
@"namespace PublicApiGeneratorTests.Examples
{
    public class MethodAsyncReturnValueWithoutAsyncKeyword
    {
        public MethodAsyncReturnValueWithoutAsyncKeyword() { }
        public System.Threading.Tasks.Task<string> AsyncMethod() { }
    }
}");
        }
    }

    // ReSharper disable UnusedMember.Global
    // ReSharper disable CSharpWarnings::CS1998
    // ReSharper disable ClassNeverInstantiated.Global
#pragma warning disable 1998
    namespace Examples
    {
        public class MethodAsyncVoid
        {
            public async void AsyncMethod()
            {
            }
        }

        public class MethodAsync
        {
            public async Task AsyncMethod()
            {
            }
        }

        public class MethodAsyncWithoutAsyncKeyword
        {
            public Task AsyncMethod()
            {
                return Task.FromResult(0);
            }
        }

        public class MethodAsyncReturnValue
        {
            public async Task<string> AsyncMethod()
            {
                return await Task.FromResult("Hello world");
            }
        }

        public class MethodAsyncReturnValueWithoutAsyncKeyword
        {
            public Task<string> AsyncMethod()
            {
                return Task.FromResult("Hello world");
            }
        }
    }
#pragma warning restore 1998
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore CSharpWarnings::CS1998
    // ReSharper restore UnusedMember.Global
}