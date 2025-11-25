using System.CodeDom;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace PublicApiGenerator;

internal class ExtensionBlockDeclaration : CodeTypeDeclaration
{
    public ExtensionBlockDeclaration(TypeDefinition definition)
    {
        AnchorMethod = definition.NestedTypes[0].GetMethods().Single();
        if (AnchorMethod.Parameters[0].Name != string.Empty)
            Name = AnchorMethod.Parameters[0].Name;
        AnchorType = AnchorMethod.Parameters[0].ParameterType.CreateCodeTypeReference();
        // rename $T0 $T1 in extension blocks to TKey TValue taking generic parameter names from this anchor
        Rewrite = x => x.StartsWith("$T")
            ? AnchorMethod.Parameters[0].ParameterType switch
            {
                GenericInstanceType genericInstanceType => genericInstanceType
                    .GenericArguments[int.Parse(x.Substring(2))].Name,
                GenericParameter genericParameter => genericParameter.Name,
                object unknown => throw new NotSupportedException($"Unknown parameter type {unknown}.")
            }
            : x;
    }

    public MethodDefinition AnchorMethod { get; }

    public CodeTypeReference AnchorType { get; }

    public Func<string, string> Rewrite { get; }
}
