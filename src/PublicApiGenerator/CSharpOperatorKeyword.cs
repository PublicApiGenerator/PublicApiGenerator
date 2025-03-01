namespace PublicApiGenerator;

internal static class CSharpOperatorKeyword
{
    private static readonly Dictionary<string, string> _operatorNameMap = new()
    {
        { "op_False", "false" },
        { "op_True", "true" },
        { "op_Addition", "+" },
        { "op_UnaryPlus", "+" },
        { "op_Subtraction", "-" },
        { "op_UnaryNegation", "-" },
        { "op_Multiply", "*" },
        { "op_Division", "/" },
        { "op_Modulus", "%" },
        { "op_Increment", "++" },
        { "op_Decrement", "--" },
        { "op_OnesComplement", "~" },
        { "op_LogicalNot", "!" },
        { "op_BitwiseAnd", "&" },
        { "op_BitwiseOr", "|" },
        { "op_ExclusiveOr", "^" },
        { "op_LeftShift", "<<" },
        { "op_RightShift", ">>" },
        { "op_Equality", "==" },
        { "op_Inequality", "!=" },
        { "op_GreaterThan", ">" },
        { "op_GreaterThanOrEqual", ">=" },
        { "op_LessThan", "<" },
        { "op_LessThanOrEqual", "<=" }
    };

    public static string Get(string memberName)
    {
        return _operatorNameMap.TryGetValue(memberName, out string mappedMemberName) ? "operator " + mappedMemberName : memberName;
    }
}
