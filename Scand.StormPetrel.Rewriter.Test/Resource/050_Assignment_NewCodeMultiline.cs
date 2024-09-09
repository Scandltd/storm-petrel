new List<MessageTreeDoc>()
{
    new MessageTreeDoc()
    {
        AncestorPartyId = 4007,
        Directions = 1,
        Id = "A4007-S2007-M2",
        LastReceived = new DateTimeOffset(2021, 7, 5, 14, 8, 44, TimeSpan.Zero),
        MessageDetails = new List<MessageDetailsDoc>()
        {
            new MessageDetailsDoc()
            {
                Header = new MessageHeader()
                {
                    HeaderId = 1,
                    ClassId = 2,
                },
                Message = new Message()
                {
                    HeaderId = 1,
                    MessageId = 1,
                    MessageStatusId = 45,
                    Received = new DateTimeOffset(2021, 7, 5, 14, 8, 44, TimeSpan.Zero),
                },
            }
        }
    }
}