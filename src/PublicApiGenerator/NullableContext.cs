using Mono.Cecil;

namespace PublicApiGenerator;

// https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-metadata.md#nullablecontextattribute
internal class NullableContext
{
    [field: ThreadStatic]
    private static Stack<ICustomAttributeProvider> NullableContextProviders
    {
        get
        {
            field ??= new Stack<ICustomAttributeProvider>();
            return field;
        }
    }

    internal static IDisposable Push(ICustomAttributeProvider provider) => new PopDisposable(provider);

    internal static IEnumerable<ICustomAttributeProvider> Providers => NullableContextProviders;

    private sealed class PopDisposable : IDisposable
    {
        public PopDisposable(ICustomAttributeProvider provider)
        {
            NullableContextProviders.Push(provider);
        }

        public void Dispose()
        {
            NullableContextProviders.Pop();
        }
    }
}
