namespace PublicApiGenerator;

internal static class CSharpTypeKeyword
{
    public static string Get(string typeName) => typeName switch
    {
        "System.Byte" => "byte",
        "System.SByte" => "sbyte",
        "System.Int16" => "short",
        "System.UInt16" => "ushort",
        "System.Int32" => "int",
        "System.UInt32" => "uint",
        "System.Int64" => "long",
        "System.UInt64" => "ulong",
        "System.Single" => "float",
        "System.Double" => "double",
        "System.Decimal" => "decimal",
        "System.Object" => "object",
        "System.String" => "string",
        "System.Boolean" => "bool",
        "System.Void" => "void",
        _ => typeName,
    };
}
