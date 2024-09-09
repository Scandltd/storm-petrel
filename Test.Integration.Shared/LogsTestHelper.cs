using System;
using System.Globalization;
using System.IO;

namespace Test.Integration.Shared
{
    internal static class LogsTestHelper
    {
        public static bool IsStormPetrelLogFileExists()
        {
            var todaySuffix = DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            bool logExists = File.Exists($"../../../Logs/StormPetrel-{todaySuffix}.log");
            var todaySuffix2 = DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            var isAfterMidnight = todaySuffix != todaySuffix2;
            if (isAfterMidnight)
            {
                logExists = File.Exists($"../../../Logs/StormPetrel-{todaySuffix2}.log");
            }
            return logExists;
        }
    }
}
