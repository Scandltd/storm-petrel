﻿namespace Test.Integration.XUnit
{
    internal static class MultithreadTestHelper
    {
        public static int GetExpected(int arg)
            =>
        arg switch
        {
            1 => 1,
            2 => 1,
            3 => 1,
            4 => 1,
            5 => 1,
            6 => 1,
            7 => 1,
            8 => 1,
            9 => 1,
            10 => 1,
            _ => throw new InvalidOperationException(),
        };
    }
}
