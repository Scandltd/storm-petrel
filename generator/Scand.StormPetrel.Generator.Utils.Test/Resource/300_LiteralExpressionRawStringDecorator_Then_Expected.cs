new FooClass()
{
    NullProperty = null,
    EmptyStringProperty = """""",
    SingleLineProperty = """single line""",
    MultiLineProperty = """
    line1 ending with CRLF
    line2 ending with LF
    line3 ending with CR    END
    """,
    MultiLinePropertyWithVeryFirstLFAsEndOfLine = """
    line1 ending with LF
    line2 ending with CRLF
    line3 ending with CR    END
    """,
        MultiLinePropertyWithExtraTrivia = """
        line1
        line2
        """,
    MultiLinePropertyWithExtraDoubleQuotes = """""
    l"""ine"""1
    """"line2""""
    """"",
    MultiLinePropertyWithWhitespacesValueTrivia =      """
    line1
    line2
    """,
    MultiLinePropertyWithNewLinesValueTrivia =
    """
    line1
    line2
    """,
    PropertyWithEmptyStringAsRawString = """""",
    PropertyWithNonEmptyStringAsRawString = """
        line1
        line2
        """,
    VerbatimStringPropertyWithCoupleDoubleQuotes = """""""",
    RegularStringPropertyWithCoupleDoubleQuotes = """""""""""""",
}
