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
                    Docs = new List<MessageTreeDoc>()
                    {
                        new MessageTreeDoc()
                        {
                            AncestorPartyId = 1000,
                            Id = "A1000-S1000-M1",
                            LastReceived = new DateTimeOffset(2021, 7, 5, 14, 8, 44, TimeSpan.Zero),
                            MessageDetails = new List<MessageDetailsDoc>()
                            {
                                new MessageDetailsDoc()
                                {
                                    Header = new MessageHeader()
                                    {
                                        HeaderId = 1,
                                        ClassId = 2
                                    },
                                    Message = new Message()
                                    {
                                        HeaderId = 1,
                                        MessageId = 1,
                                        MessageStatusId = 45,
                                        Received = new DateTimeOffset(2021, 7, 5, 14, 8, 44, TimeSpan.Zero),
                                    },
                                    MessageId = 1,
                                    Metadata = new List<KeyValuePair<string,string>>()
                                    {
                                        new KeyValuePair<string,string>("BuyerGln", "6661230000006"),
                                        new KeyValuePair<string,string>("OrderType", "ORDER_TYPE_NORMAL"),
                                    },
                                    NodeId = 1,
                                    Received = new DateTimeOffset(2021, 11, 11, 9, 8, 46, TimeSpan.Zero),
                                    SystemPartyId = 1000,
                                    Transitions = new List<MessageTransitionLog>(),
                                },
                            },
                        },
                    },
                },
            };
        }
    }
}