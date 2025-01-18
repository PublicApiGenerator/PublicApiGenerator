using System.CodeDom;

namespace PublicApiGenerator;

internal class CodeTypeDeclarationEx : CodeTypeDeclaration
{
    public CodeTypeDeclarationEx(string name) : base(name)
    {
    }

    public bool IsStatic { get; set; }

    public bool IsReadonly { get; set; }

    public bool IsByRefLike { get; set; }

    public bool IsRecord { get; set; }
}
