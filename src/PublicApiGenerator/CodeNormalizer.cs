using System.Text.RegularExpressions;

namespace PublicApiGenerator;

internal static class CodeNormalizer
{
    private const string AUTO_GENERATED_HEADER = @"^//-+\s*$.*^//-+\s*$";
    private const string EMPTY_GET_SET = @"\s+{\s+get\s+{\s+}\s+set\s+{\s+}\s+}";
    private const string EMPTY_GET = @"\s+{\s+get\s+{\s+}\s+}";
    private const string EMPTY_SET = @"\s+{\s+set\s+{\s+}\s+}";
    private const string GET_SET = @"\s+{\s+get;\s+set;\s+}";
    private const string GET = @"\s+{\s+get;\s+}";
    private const string SET = @"\s+{\s+set;\s+}";

    // https://github.com/PublicApiGenerator/PublicApiGenerator/issues/80
    internal const string ATTRIBUTE_MARKER = "_attribute_292C96C3_C42E_4C07_BEED_73F5DAA0A6DF_";

    public static string NormalizeGeneratedCode(StringWriter writer)
    {
        var gennedClass = writer.ToString();

        gennedClass = Regex.Replace(gennedClass, AUTO_GENERATED_HEADER, string.Empty,
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.Singleline);
        gennedClass = Regex.Replace(gennedClass, EMPTY_GET_SET, " { get; set; }",
            RegexOptions.IgnorePatternWhitespace);
        gennedClass = Regex.Replace(gennedClass, GET_SET, " { get; set; }", RegexOptions.IgnorePatternWhitespace);
        gennedClass = Regex.Replace(gennedClass, EMPTY_GET, " { get; }", RegexOptions.IgnorePatternWhitespace);
        gennedClass = Regex.Replace(gennedClass, EMPTY_SET, " { set; }", RegexOptions.IgnorePatternWhitespace);
        gennedClass = Regex.Replace(gennedClass, GET, " { get; }", RegexOptions.IgnorePatternWhitespace);
        gennedClass = Regex.Replace(gennedClass, SET, " { set; }", RegexOptions.IgnorePatternWhitespace);
        gennedClass = Regex.Replace(gennedClass, @"\s+{\s+}", " { }", RegexOptions.IgnorePatternWhitespace);
        gennedClass = Regex.Replace(gennedClass, @"\)\s+;", ");", RegexOptions.IgnorePatternWhitespace);
        var attributeMarkerEscaped = Regex.Escape(ATTRIBUTE_MARKER);
        gennedClass = Regex.Replace(gennedClass, $@"
                (Attribute)?                               # Delete this if present. Would create a clash for Attribute1, Attribute1Attribute but that is a very rare edge case
                (                                          # Then require:
                {attributeMarkerEscaped}(\(\))?(?=\])      # - Empty parens (to delete) if present, immediately followed by closing square brace (not deleted),
                |
                {attributeMarkerEscaped}(?=\(.+\)\])       # - or non-empty parens immediately followed by closing square brace (not deleted).
                |
                {attributeMarkerEscaped}(?=new.+\}}\)\]))  # - or non-empty parens immediately followed by new, closing curlies, closing brace and square brace to cover new object [] {{ }} cases (not deleted).
                ",
            string.Empty,
            RegexOptions.Singleline |
            RegexOptions.IgnorePatternWhitespace); // SingleLine is required for multi line params arrays

        gennedClass = Regex.Replace(gennedClass, @"\r\n|\n\r|\r|\n", Environment.NewLine);
        gennedClass = Regex.Replace(gennedClass, @$"{Environment.NewLine}\s+;{Environment.NewLine}", ";" + Environment.NewLine); // bug-fix for https://github.com/PublicApiGenerator/PublicApiGenerator/issues/301

        gennedClass = RemoveUnnecessaryWhiteSpace(gennedClass);
        return gennedClass;
    }

    private static string RemoveUnnecessaryWhiteSpace(string publicApi)
    {
        return string.Join(Environment.NewLine, publicApi.Split(new[]
            {
                Environment.NewLine
            }, StringSplitOptions.RemoveEmptyEntries)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.TrimEnd())
        );
    }
}
