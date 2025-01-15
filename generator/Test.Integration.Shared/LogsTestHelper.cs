using System;
using System.Globalization;
using System.IO;

namespace Test.Integration.Shared
{
    internal static class LogsTestHelper
    {
        public static bool IsStormPetrelLogFileExists()
        {
            var today = DateTime.Today;
            var possibleLogCreationDates = new[]
            {
                today,
                today.AddDays(-1), //if this method is executed after midnight while log file was created before the midnight
            };
            foreach (var date in possibleLogCreationDates)
            {
                var dateSuffix = date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"../../../Logs/StormPetrel-{dateSuffix}.log");
                bool logExists = File.Exists(filePath);
                if (logExists)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
