using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Scand.StormPetrel.FileSnapshotInfrastructure
{
    /// <summary>
    /// Provides file snapshot information based on instance parameters.
    /// </summary>
    public sealed class SnapshotInfoProvider : ISnapshotInfoProvider
    {
        public const string TokenForCallerFileNameWithoutExtension = "<CallerFileNameWithoutExtension>";
        public const string TokenForMemberName = "<CallerMemberName>";
        public const string TokenForUseCaseId = "<UseCaseId>";
        private readonly string _extraPathPattern;
        private readonly string _fileNameWithoutExtensionPattern;
        private readonly string _fileExtension;
        public SnapshotInfoProvider(
            string extraPathPattern = TokenForCallerFileNameWithoutExtension + ".Expected",
            string fileNameWithoutExtensionPattern = TokenForMemberName + "." + TokenForUseCaseId,
            string fileExtension = null)
        {
            _extraPathPattern = extraPathPattern;
            _fileNameWithoutExtensionPattern = fileNameWithoutExtensionPattern;
            _fileExtension = fileExtension;
        }

        public string GetFilePathWithoutExtension(string useCaseId, string callerFilePath, string callerMemberName)
        {
            if (useCaseId == null)
            {
                useCaseId = "";
            }
            if (callerFilePath == null)
            {
                callerFilePath = "";
            }
            if (callerMemberName == null)
            {
                callerMemberName = "";
            }
            var (filePath, memberName) = ConvertStormPetrelFilePathOrSelf(callerFilePath, callerMemberName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            var fileName = Path.GetFileName(filePath);
            var dirPath = filePath.Substring(0, filePath.Length - fileName.Length);
            foreach (var p in _extraPathPattern.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar))
            {
                if (string.IsNullOrEmpty(p))
                {
                    continue;
                }
                var segment = ReplaceTokens(p, fileNameWithoutExtension, memberName, useCaseId);
                if (string.IsNullOrEmpty(p))
                {
                    continue;
                }
                dirPath = Path.Combine(dirPath, segment);
            }
            fileNameWithoutExtension = ReplaceTokens(_fileNameWithoutExtensionPattern, fileNameWithoutExtension, memberName, useCaseId);
            //Do not add extra "." if useCaseId is empty
            fileNameWithoutExtension = fileNameWithoutExtension.TrimEnd('.');
            return Path.Combine(dirPath, fileNameWithoutExtension);
        }

        public string GetFileExtension(string useCaseId, string callerFilePath, string callerMemberName) => _fileExtension;

        private static (string FilePathConverted, string MemberNameConverted) ConvertStormPetrelFilePathOrSelf(string callerFilePath, string callerMemberName)
        {
            var incrementalGeneratorMethodSuffix = "StormPetrel";
            if (!callerMemberName.EndsWith(incrementalGeneratorMethodSuffix, StringComparison.OrdinalIgnoreCase))
            {
                return (callerFilePath, callerMemberName);
            }

            callerMemberName = callerMemberName.Substring(0, callerMemberName.Length - incrementalGeneratorMethodSuffix.Length);
            //Truncate file extension added by Incremental Generator.
            var truncatedFilePath = callerFilePath.Substring(0, callerFilePath.Length - ".g.cs".Length);
            var segments = truncatedFilePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            //Truncate path extra segments like `/obj/Debug/...` or `/obj/Release/...` which exists in the case of StormPetrel method call.
            //TODO: This implementation does not cover some use cases (e.g. test project is configured to not compile to "obj" folder).
            segments = segments
                        .TakeWhile(x => x != "obj")
                        .Select(x => x.IndexOf(':') > -1
                                        ? x + Path.DirectorySeparatorChar //To properly "Path.Combine(segments)" .NET Framework 4.7.2
                                        : x)
                        .ToArray();
            var originalCallerDir = Path.Combine(segments);
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !Path.IsPathRooted(originalCallerDir))
            {
                originalCallerDir = Path.Combine("/", originalCallerDir);
            }
            var originalCallerFileName = Path.GetFileName(truncatedFilePath);
            callerFilePath = Path.Combine(originalCallerDir, originalCallerFileName);
            return (callerFilePath, callerMemberName);
        }

        private static string ReplaceTokens(string stringWithTokens, string fileNameWithoutExtension, string memberName, string useCaseId) =>
            stringWithTokens
                .Replace(TokenForCallerFileNameWithoutExtension, fileNameWithoutExtension)
                .Replace(TokenForMemberName, memberName)
                .Replace(TokenForUseCaseId, useCaseId);
    }
}
