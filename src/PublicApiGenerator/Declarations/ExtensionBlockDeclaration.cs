using System.CodeDom;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace PublicApiGenerator;

internal class ExtensionBlockDeclaration : CodeTypeDeclaration
{
    public ExtensionBlockDeclaration(TypeDefinition definition)
    {
        TypeDefinition = definition;

        var m = definition.GetMethods().First(m => m.IsSpecialName && m.IsCompilerGenerated() && m.IsStatic && m.ReturnType.FullName == "System.Void" && m.Name == "<Extension>$" && m.Parameters.Count == 1);
        if (m.Parameters[0].Name != string.Empty)
            Name = m.Parameters[0].Name;
        AnchorType = m.Parameters[0].ParameterType;
    }

    public TypeReference AnchorType { get; set; }

    public TypeDefinition TypeDefinition { get; set; }

    public List<MethodDefinition> Methods { get; set; } = [];
}
