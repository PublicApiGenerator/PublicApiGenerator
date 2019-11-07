using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace PublicApiGenerator
{
    // https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-metadata.md#nullablecontextattribute
    internal class NullableContext
    {
        [ThreadStatic]
        private static Stack<ICustomAttributeProvider>? _nullableContextProviders;

        private static Stack<ICustomAttributeProvider> NullableContextProviders
        {
            get
            {
                if (_nullableContextProviders == null)
                    _nullableContextProviders = new Stack<ICustomAttributeProvider>();
                return _nullableContextProviders;
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
}
