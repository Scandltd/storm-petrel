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
                    Docs = @"""""""",
                },
            };
        }
    }
}