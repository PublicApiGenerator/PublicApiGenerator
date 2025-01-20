using PublicApiGeneratorTests.Examples;

namespace PublicApiGeneratorTests
{
    public class Constants : ApiGeneratorTestsBase
    {
        // https://github.com/PublicApiGenerator/PublicApiGenerator/issues/82
        [Fact]
        public void Should_escape_literals_in_public_constant_string()
        {
            AssertPublicApi<MyConstants>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MyConstants
    {
        public const string MULTILINE1 = "This\r\nIs\r\nMultiline\r\n";
        public const string MULTILINE2 = "This\nIs\nMultiline\n";
        public const string SINGLELINE = "ABC";
        public MyConstants() { }
    }
}
""");
        }

        // https://github.com/PublicApiGenerator/PublicApiGenerator/issues/83
        [Fact]
        public void Should_escape_special_literals_in_public_constant_string()
        {
            AssertPublicApi<MyConstants2>("""
namespace PublicApiGeneratorTests.Examples
{
    public class MyConstants2
    {
        public const string ALERT = "This is \a alert";
        public const string BACKSLASH = "This is \\ backslash";
        public const string BACKSPACE = "This is \b backspace";
        public const string CARRET = "This is \r carriage return";
        public const string CHAR0 = "This is \0 character 0";
        public const string DOUBLEQUOTE = "This is \" double quote";
        public const string FORMFEED = "This is \f form feed";
        public const string NEWLINE = "This is \n new line";
        public const string QUOTE = "This is \' quote";
        public const string TAB = "This is \t horizontal tab";
        public const string UNICODE = "This is ꧰ Ʀ unicode escape";
        public const string VERTICALQUOTE = "This is \v vertical quote";
        public MyConstants2() { }
    }
}
""");
        }
    }

    namespace Examples
    {
        public class MyConstants
        {
            public const string SINGLELINE = "ABC";
            public const string MULTILINE1 = "This\r\nIs\r\nMultiline\r\n";
            public const string MULTILINE2 = "This\nIs\nMultiline\n";
        }

        // https://learn.microsoft.com/en-us/archive/blogs/csharpfaq/what-character-escape-sequences-are-available
        public class MyConstants2
        {
            public const string ALERT = "This is \a alert";
            public const string BACKSLASH = "This is \\ backslash";
            public const string BACKSPACE = "This is \b backspace";
            public const string CARRET = "This is \r carriage return";
            public const string CHAR0 = "This is \0 character 0";
            public const string DOUBLEQUOTE = "This is \" double quote";
            public const string FORMFEED = "This is \f form feed";
            public const string NEWLINE = "This is \n new line";
            public const string QUOTE = "This is ' quote";
            public const string TAB = "This is \t horizontal tab";
            public const string UNICODE = "This is \uA9F0 \x1A6 unicode escape";
            public const string VERTICALQUOTE = "This is \v vertical quote";
        }
    }
}
