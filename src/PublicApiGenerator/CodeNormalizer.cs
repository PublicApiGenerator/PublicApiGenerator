namespace PublicApiGenerator;

internal static class CodeNormalizer
{
    public static string NormalizeGeneratedCode(StringWriter writer)
    {
        var gennedClass = writer.ToString();

        if (gennedClass.EndsWith(Environment.NewLine))
            gennedClass = gennedClass.Substring(0, gennedClass.Length - Environment.NewLine.Length);

        return gennedClass;
    }
}
