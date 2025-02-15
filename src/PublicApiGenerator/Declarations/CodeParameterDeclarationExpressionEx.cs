using System.CodeDom;

namespace PublicApiGenerator;

internal class CodeParameterDeclarationExpressionEx : CodeParameterDeclarationExpression
{
    public CodeParameterDeclarationExpressionEx(CodeTypeReference type, string name) : base(type, name)
    {
    }

    public bool This { get; set; }
}
