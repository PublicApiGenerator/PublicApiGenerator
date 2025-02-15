using System.CodeDom;

namespace PublicApiGenerator;

internal class CodeMemberFieldEx : CodeMemberField
{
    public CodeMemberFieldEx(CodeTypeReference type, string name)
        : base(type, name)
    {
    }

    public bool IsReadonly { get; set; }
}
