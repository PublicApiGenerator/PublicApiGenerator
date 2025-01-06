using System.CodeDom;

namespace PublicApiGenerator;

internal class CodeTypeDeclarationEx : CodeTypeDeclaration
{
    public CodeTypeDeclarationEx(string name) : base(name)
    {
    }

    public bool IsStatic { get; set; }
}
