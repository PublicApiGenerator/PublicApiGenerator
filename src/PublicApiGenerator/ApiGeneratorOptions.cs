using System;

namespace PublicApiGenerator
{
    public class ApiGeneratorOptions
    {
        public Type[]? IncludeTypes { get; set; }

        public bool IncludeAssemblyAttributes { get; set; } = true;

        public string[] WhitelistedNamespacePrefixes { get; set; } = DefaultWhitelistedNamespacePrefixes;

        public string[]? ExcludeAttributes { get; set; }

        static readonly string[] DefaultWhitelistedNamespacePrefixes = Array.Empty<string>();
    }
}
