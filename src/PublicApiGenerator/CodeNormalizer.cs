using System.Text;
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
    internal const string STATIC_MARKER = "static_C91E2709_C00B-4CAB_8BBC_B2B11DC75E50 ";
    internal const string READONLY_MARKER = "readonly_79D3ED2A_0B60_4C3B_8432_941FE471A38B ";
    internal const string ATTRIBUTE_MARKER = "_attribute_292C96C3_C42E_4C07_BEED_73F5DAA0A6DF_";
    internal const string EVENT_MODIFIER_MARKER_TEMPLATE = "_{0}_292C96C3C42E4C07BEED73F5DAA0A6DF_";
    internal const string EVENT_REMOVE_PUBLIC_MARKER = "removepublic";
    internal const string METHOD_MODIFIER_MARKER_TEMPLATE = "_{0}_3C0D97CD952D40AA8B6E1ECB98FFC79F_";
    internal const string PROPERTY_MODIFIER_MARKER_TEMPLATE = "_{0}_5DB9F56043FF464997155541DA321AD4_";
    internal const string PROPERTY_INIT_ONLY_SETTER_TEMPLATE = "_{0}_156783F107B3427090B5486DC33EE6A9_";

    public static string NormalizeMethodName(string methodName)
    {
        return Regex.Replace(methodName, @"(_(.*)_3C0D97CD952D40AA8B6E1ECB98FFC79F_)?", string.Empty);
    }

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
        gennedClass = Regex.Replace(gennedClass, @"\r\n\s+;\r\n", ";" + Environment.NewLine); // bug-fix for https://github.com/PublicApiGenerator/PublicApiGenerator/issues/301
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

        gennedClass = Regex.Replace(gennedClass, @"(public|protected) (.*) _(.*)_292C96C3C42E4C07BEED73F5DAA0A6DF_(.*)", EventModifierMatcher);
        gennedClass = Regex.Replace(gennedClass, @"(public|protected)( abstract | static | new static | virtual | override | new | unsafe | )(.*) _(.*)_5DB9F56043FF464997155541DA321AD4_(.*)", PropertyModifierMatcher);
        gennedClass = Regex.Replace(gennedClass, @"_(.*)_156783F107B3427090B5486DC33EE6A9_(.*)", PropertyInitOnlySetterMatcher);
        gennedClass = Regex.Replace(gennedClass, @"(public|protected)( abstract | static | new static | virtual | override | new | unsafe | )(.*) _(.*)_3C0D97CD952D40AA8B6E1ECB98FFC79F_(.*)", MethodModifierMatcher);
        gennedClass = gennedClass.Replace("class " + STATIC_MARKER, "static class ");
        gennedClass = gennedClass.Replace("struct " + READONLY_MARKER, "readonly struct ");
        gennedClass = gennedClass.Replace(READONLY_MARKER, string.Empty); // remove magic marker from readonly struct ctor
        gennedClass = Regex.Replace(gennedClass, @"\r\n|\n\r|\r|\n", Environment.NewLine);

        gennedClass = RemoveUnnecessaryWhiteSpace(gennedClass);
        return gennedClass;
    }

    private static string EventModifierMatcher(Match match)
    {
        var visibility = match.Groups[1].Value;
        var modifier = match.Groups[3].Value;

        var replacementBuilder = new StringBuilder();
        if (modifier.EndsWith(EVENT_REMOVE_PUBLIC_MARKER))
        {
            replacementBuilder.Append(modifier == EVENT_REMOVE_PUBLIC_MARKER
                ? "event "
                : $"{modifier.Replace(EVENT_REMOVE_PUBLIC_MARKER, string.Empty)} event ");
        }
        else
        {
            replacementBuilder.Append($"{visibility} {modifier} event ");
        }

        return match.ToString().Replace(string.Format(EVENT_MODIFIER_MARKER_TEMPLATE, modifier), string.Empty)
            .Replace($"{visibility} event ", replacementBuilder.ToString());
    }

    private static string PropertyModifierMatcher(Match match)
    {
        var oldModifier = match.Groups[2].Value;
        var modifier = match.Groups[4].Value;

        var s = match.ToString().Replace(string.Format(PROPERTY_MODIFIER_MARKER_TEMPLATE, modifier), string.Empty);
        return string.IsNullOrWhiteSpace(oldModifier) ? s.Insert(s.IndexOf(oldModifier, StringComparison.Ordinal), modifier.Substring(0, modifier.Length - 1)) : s.Replace(oldModifier, modifier);
    }

    private static string PropertyInitOnlySetterMatcher(Match match)
    {
        var name = match.Groups[1].Value;
        var tail = match.Groups[2].Value;
        return name + tail.Replace("set;", "init;");
    }

    private static string MethodModifierMatcher(Match match)
    {
        var oldModifier = match.Groups[2].Value;
        var modifier = match.Groups[4].Value;

        var s = match.ToString().Replace(string.Format(METHOD_MODIFIER_MARKER_TEMPLATE, modifier), string.Empty);
        return string.IsNullOrWhiteSpace(oldModifier) ? s.Insert(s.IndexOf(oldModifier, StringComparison.Ordinal), modifier.Substring(0, modifier.Length - 1)) : s.Replace(oldModifier, modifier);
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
