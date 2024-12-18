﻿using FluentAssertions;
using Test.Integration.Shared;

namespace Test.Integration.XUnit
{
    public class LogsTest
    {
        [Fact]
        public void LogTest()
        {
            Shared.LogsTestHelper.IsStormPetrelLogFileExists().Should().BeTrue();
            BackupHelper.DeleteBackupWithResultAssertion(GetType());
        }
    }
}
