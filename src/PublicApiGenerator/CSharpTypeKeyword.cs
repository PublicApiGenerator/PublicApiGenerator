namespace PublicApiGenerator
{
    internal static class CSharpTypeKeyword
    {
        public static string Get(string typeName)
        {
            switch (typeName)
            {
                case "System.Byte":
                    return "byte";
                case "System.SByte":
                    return "sbyte";
                case "System.Int16":
                    return "short";
                case "System.UInt16":
                    return "ushort";
                case "System.Int32":
                    return "int";
                case "System.UInt32":
                    return "uint";
                case "System.Int64":
                    return "long";
                case "System.UInt64":
                    return "ulong";
                case "System.Single":
                    return "float";
                case "System.Double":
                    return "double";
                case "System.Decimal":
                    return "decimal";
                case "System.Object":
                    return "object";
                case "System.String":
                    return "string";
                case "System.Boolean":
                    return "bool";
                case "System.Void":
                    return "void";
                default:
                    return typeName;
            }
        }
    }
}
