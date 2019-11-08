using System;

namespace PublicApiGenerator
{
    /// <summary>
    /// Options to influence the ApiGenerator
    /// </summary>
    public class ApiGeneratorOptions
    {
        /// <summary>
        /// Allows to control which types of the generated assembly should be included. If this option is specified all other types found in the assembly that are not present here will be exclude.
        /// </summary>
        public Type[]? IncludeTypes { get; set; }

        /// <summary>
        /// Instructs the generator to include assembly level attributes.
        /// </summary>
        /// <remarks>Defaults to true</remarks>
        public bool IncludeAssemblyAttributes { get; set; } = true;

        /// <summary>
        /// Allows to whitelist certain namespace prefixes. For example by default types found in Microsoft or System namespaces are not treated as part of the public API.
        /// </summary>
        /// <example>
        /// <code>
        /// var options = new DefaultApiGeneratorOptions
        /// {
        ///    WhitelistedNamespacePrefixes = new[] { "Microsoft.Whitelisted" }
        /// };
        /// </code>
        /// </example>
        public string[] WhitelistedNamespacePrefixes { get; set; } = DefaultWhitelistedNamespacePrefixes;

        /// <summary>
        /// Allows to exclude attributes by specifying the fullname of the attribute to exclude.
        /// </summary>
        /// <example>
        ///<code>
        /// var options = new DefaultApiGeneratorOptions
        /// {
        ///    ExcludeAttributes = new[] { "PublicApiGeneratorTests.Examples.SimpleAttribute" }
        /// };
        /// </code>
        /// </example>
        public string[]? ExcludeAttributes { get; set; }

        static readonly string[] DefaultWhitelistedNamespacePrefixes = Array.Empty<string>();
    }
}
