new FooClass()
{
    NullProperty = null,
    EmptyStringProperty = // lang=json
    """""",
    SingleLineProperty = // lang=json
    """single line""",
    MultiLineProperty = // lang=json
    """
    line1 ending with CRLF
    line2 ending with LF
    line3 ending with CR    END
    """,
    MultiLinePropertyWithVeryFirstLFAsEndOfLine = // lang=json
    """
    line1 ending with LF
    line2 ending with CRLF
    line3 ending with CR    END
    """,
        MultiLinePropertyWithExtraTrivia = // lang=json
        """
        line1
        line2
        """,
    MultiLinePropertyWithExtraDoubleQuotes = // lang=json
    """""
    l"""ine"""1
    """"line2""""
    """"",
    MultiLinePropertyWithWhitespacesValueTrivia =      // lang=json
    """
    line1
    line2
    """,
    MultiLinePropertyWithNewLinesValueTrivia =
    // lang=json
    """
    line1
    line2
    """,
    PropertyWithEmptyStringAsRawString = """""",
    PropertyWithNonEmptyStringAsRawString = """
        line1
        line2
        """,
    VerbatimStringPropertyWithCoupleDoubleQuotes = // lang=json
    """""""",
    RegularStringPropertyWithCoupleDoubleQuotes = // lang=json
    """""""""""""",
}
