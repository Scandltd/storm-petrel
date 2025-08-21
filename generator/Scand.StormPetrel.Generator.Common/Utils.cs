using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Scand.StormPetrel.Generator.Common
{
    internal static class Utils
    {
        /// <summary>
        /// Case-insensitive check
        /// </summary>
        /// <param name="additionalText"></param>
        /// <returns></returns>
        public static bool IsAppSettings(AdditionalText additionalText)
            => IsAppSettings(Path.GetFileName(additionalText.Path));
        /// <summary>
        /// Case-insensitive check
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsAppSettings(string fileName)
            => Regex.IsMatch(fileName, "StormPetrel", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static T GetAppSetting<T>(IEnumerable<T> appSettingCandidates, Func<T, string> pathSelector)
            => appSettingCandidates
                .OrderBy(x => pathSelector(x), StringComparer.Ordinal)
                .FirstOrDefault();
    }
}
