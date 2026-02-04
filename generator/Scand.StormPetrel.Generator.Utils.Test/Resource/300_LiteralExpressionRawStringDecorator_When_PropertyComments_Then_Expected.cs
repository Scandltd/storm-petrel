new FooClass()
{
    NullProperty = null,
    EmptyStringProperty = "",
    SingleLineProperty = // lang=regex
    """single line""",
    MultiLineProperty = // lang=json
    """
    line1 ending with CRLF
    line2 ending with LF
    line3 ending with CR    END
    """,
    MultiLinePropertyWithVeryFirstLFAsEndOfLine = """
    line1 ending with LF
    line2 ending with CRLF
    line3 ending with CR    END
    """,
        MultiLinePropertyWithExtraTrivia = // lang=json
        """
        line1
        line2
        """,
    MultiLinePropertyWithExtraDoubleQuotes = "l\"\"\"ine\"\"\"1\r\n\"\"\"\"line2\"\"\"\"",
    MultiLinePropertyWithWhitespacesValueTrivia =      "line1\r\nline2",
    MultiLinePropertyWithNewLinesValueTrivia =
    "line1\r\nline2",
    PropertyWithEmptyStringAsRawString = """""",
    PropertyWithNonEmptyStringAsRawString = """
        line1
        line2
        """,
    VerbatimStringPropertyWithCoupleDoubleQuotes = @"""""",
    RegularStringPropertyWithCoupleDoubleQuotes = "\"\"\"\"",
}
