using System.CodeDom;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace PublicApiGenerator;

internal class ExtensionBlockDeclaration : CodeTypeDeclaration
{
    public ExtensionBlockDeclaration(TypeDefinition definition)
    {
        TypeDefinition = definition;

        var m = definition.NestedTypes[0].GetMethods().Single();
        if (m.Parameters[0].Name != string.Empty)
            Name = m.Parameters[0].Name;
        AnchorType = m.Parameters[0].ParameterType;
    }

    public TypeReference AnchorType { get; set; }

    public TypeDefinition TypeDefinition { get; set; }

    public List<MethodDefinition> Methods { get; set; } = [];
}
