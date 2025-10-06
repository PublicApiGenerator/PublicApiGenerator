using System.Reflection;
using Mono.Cecil;

namespace PublicApiGenerator;

internal sealed class ForwardedTypesContext : IDisposable
{
    private readonly Dictionary<Assembly, AssemblyDefinition> _assemblyDefinitions = [];

    public ForwardedTypesContext(Assembly initialAssembly, DefaultAssemblyResolver assemblyResolver, ApiGeneratorOptions options)
    {
        if (options.IncludeForwardedTypes)
        {
            // GetForwardedTypes method is available since netstandard2.1
            var method = initialAssembly.GetType().GetMethod("GetForwardedTypes") ?? throw new PlatformNotSupportedException("IncludeForwardedTypes option works only in .NET Core apps. Please either migrate your app or disable this option.");
            foreach (var type in (Type[])method.Invoke(initialAssembly, []))
            {
                if (!_assemblyDefinitions.TryGetValue(type.Assembly, out var assemblyDef))
                {
                    assemblyDef = AssemblyDefinition.ReadAssembly(type.Assembly.Location, new ReaderParameters(ReadingMode.Deferred)
                    {
                        ReadSymbols = File.Exists(Path.ChangeExtension(type.Assembly.Location, ".pdb")),
                        AssemblyResolver = assemblyResolver
                    });
                    assemblyResolver.AddSearchDirectory(Path.GetDirectoryName(type.Assembly.Location));
                    _assemblyDefinitions[type.Assembly] = assemblyDef;
                }

                ForwardedTypes.Add(assemblyDef.Modules.Select(m => m.GetType(type.FullName)).Single());
            }
        }
    }

    public List<TypeDefinition> ForwardedTypes { get; set; } = [];

    public void Dispose()
    {
        foreach (var pair in _assemblyDefinitions)
            pair.Value.Dispose();

        _assemblyDefinitions.Clear();
        ForwardedTypes.Clear();
    }
}
