using System;
using System.Collections.Generic;

namespace Scand.StormPetrel.Rewriter.Resource.Test
{
    public class GivenExample01FromSpec_ThenOutputDoc
    {
        public static TestCaseData GetData()
        {
            return new TestCaseData()
            {
                InputMessages = new[]
                {
                },
                Expected = new TestCaseDataExpected()
                {
                    Docs = new List<MessageTreeDoc>(){
                        """Single line""",
                        """
                        Line1
                        Line2
                        """,
                        // lang=json
                        """
                        Line1
                        Line2
                        """,
                        "A non-raw string", """
                        Line1
                        Line2
                        """,
                    },
                },
            };
        }
    }
}