new FooClass()
{
    NullProperty = null,
    EmptyStringProperty = "",
    SingleLineProperty = "single line",
    MultiLineProperty = "line1 ending with CRLF\r\nline2 ending with LF\nline3 ending with CR\rEND",
    MultiLinePropertyWithVeryFirstLFAsEndOfLine = "line1 ending with LF\nline2 ending with CRLF\r\nline3 ending with CR\rEND",
        MultiLinePropertyWithExtraTrivia = "line1\r\nline2",
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
