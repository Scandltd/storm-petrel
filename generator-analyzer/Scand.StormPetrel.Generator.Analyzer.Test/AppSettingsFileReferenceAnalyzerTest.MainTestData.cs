using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using System.Globalization;

namespace Scand.StormPetrel.Generator.Analyzer.Test;

partial class AppSettingsFileReferenceAnalyzerTest
{
    public static TheoryData<string,
                            (string filename, SourceText content)[],
                            (Dictionary<string, string>? Cache, Func<string, IEnumerable<string>>? EnumerateFilesFunc, Func<long>? TicksProvider),
                            DiagnosticResult?,
                            Dictionary<string, string>?> MainTestData =>
    new()
    {
        {
            "When no AdditionalFiles and no files in file system Then no diagnostics",
            [],
            default,
            null,
            []
        },
        {
            "When no AdditionalFiles and appsettings.StormPetrel.json file is in file system Then the diagnostic",
            [],
            (null, GetFilesFunc(["appsettings.StormPetrel.json"]), null),
            GetExpectedDiagnosticResult(),
            new()
            {
                { Path.Combine("c", "my", "project"), "appsettings.StormPetrel.json" },
            }
        },
        {
            "When no AdditionalFiles and appseTTings.StormPetrel.json file is in file system Then the diagnostic",
            [],
            (null, GetFilesFunc(["appseTTings.StormPetrel.json"]), null),
            GetExpectedDiagnosticResult("appseTTings.StormPetrel.json"),
            new()
            {
                { Path.Combine("c", "my", "project"), "appseTTings.StormPetrel.json" },
            }
        },
        {
            "When no AdditionalFiles, and .csproj and appsettings.StormPetrel.json files are in file system Then the diagnostic",
            [],
            (null, GetFilesFunc(["My.csproj", "appsettings.StormPetrel.json"]), null),
            GetExpectedDiagnosticResult(),
            new()
            {
                { Path.Combine("c", "my", "project"), "appsettings.StormPetrel.json" },
            }
        },
        {
            "When no AdditionalFiles and appsettings.StormPetrel.json file is above csproj Then no diagnostics",
            [],
            (null, GetFilesFunc(["My.csproj"], ["appsettings.StormPetrel.json"]), null),
            null,
            new()
            {
                { Path.Combine("c", "my", "project"), null! },
            }
        },
        {
            "When no AdditionalFiles and appsettings.StormPetrel.json file is above cSProj Then no diagnostics",
            [],
            (null, GetFilesFunc(["My.cSProj"],["appsettings.StormPetrel.json"]), null),
            null,
            new()
            {
                { Path.Combine("c", "my", "project"), null! },
            }
        },
        {
            "When no AdditionalFiles and appsettings.StormPetrel.json file is above csproj in the root Then no diagnostics",
            [],
            (null, GetFilesFunc([],["My.csproj"],["appsettings.StormPetrel.json"]), null),
            null,
            new()
            {
                { Path.Combine("c", "my", "project"), null! },
            }
        },
        {
            "When no AdditionalFiles, and .csproj and appsettings.StormPetrel.json files are in level up of file system Then the diagnostic",
            [],
            (null, GetFilesFunc([],["My.csproj", "appsettings.StormPetrel.json"]), null),
            GetExpectedDiagnosticResult(),
            new()
            {
                { Path.Combine("c", "my", "project"), "appsettings.StormPetrel.json" },
            }
        },
        {
            "When no AdditionalFiles, and .csproj and appsettings.StormPetrel.json files are in root of file system Then the diagnostic",
            [],
            (null, GetFilesFunc([],[],["My.csproj", "appsettings.StormPetrel.json"]), null),
            GetExpectedDiagnosticResult(),
            new()
            {
                { Path.Combine("c", "my", "project"), "appsettings.StormPetrel.json" },
            }
        },
        {
            "When appsettings.StormPetrel.json is in AdditionalFiles, and no files in file system Then no diagnostics",
            [("appsettings.StormPetrel.json", SourceText.From(""))],
            default,
            null,
            []
        },
        {
            "When cached Then the diagnostic comes from the cache",
            [],
            (new()
            {
                { Path.Combine("c", "my", "project"), "appsettings.StormPetrel.json" },
            }, null, null),
            GetExpectedDiagnosticResult(),
            new()
            {
                { Path.Combine("c", "my", "project"), "appsettings.StormPetrel.json" },
            }
        },
        {
            "When cached null Then the diagnostic comes from the cache",
            [],
            (new()
            {
                { Path.Combine("c", "my", "project"), null! },
            }, null, null),
            null,
            new()
            {
                { Path.Combine("c", "my", "project"), null! },
            }
        },
        {
            "When cached null but not expired Then the diagnostic comes from the cache",
            [],
            (new()
            {
                { Path.Combine("c", "my", "project"), null! },
            }, GetFilesFunc(["appsettings.StormPetrel.json"]), () => long.MinValue + TimeSpan.FromSeconds(3).Ticks),
            null,
            new()
            {
                { Path.Combine("c", "my", "project"), null! },
            }
        },
        {
            "When cached null but expired Then the diagnostic comes from the cache",
            [],
            (new()
            {
                { Path.Combine("c", "my", "project"), null! },
            }, GetFilesFunc(["appsettings.StormPetrel.json"]), () => long.MinValue + TimeSpan.FromSeconds(3).Ticks + 1),
            GetExpectedDiagnosticResult(),
            new()
            {
                { Path.Combine("c", "my", "project"), "appsettings.StormPetrel.json" },
            }
        },
        {
            "When appseTTings.StormPetrel.json is in AdditionalFiles, and no files in file system Then no diagnostics",
            [("appseTTings.StormPetrel.json", SourceText.From(""))],
            (null, null, null),
            null,
            []
        },
    };

    private static DiagnosticResult GetExpectedDiagnosticResult(string appsettingsFileName = "appsettings.StormPetrel.json")
        => new DiagnosticResult(AppSettingsFileReferenceAnalyzerHelpers.Rule)
                    .WithMessage(string.Format(CultureInfo.InvariantCulture, AppSettingsFileReferenceAnalyzerHelpers.Rule.MessageFormat.ToString(), appsettingsFileName))
                    .WithLocation(appsettingsFileName, 1, 1);

    private static Func<string, IEnumerable<string>> GetFilesFunc(IEnumerable<string>? projectFiles = null, IEnumerable<string>? myFiles = null, IEnumerable<string>? rootFiles = null)
        => x =>
    {
        if (x == Path.Combine("c", "my", "project"))
        {
            return projectFiles ?? [];
        }
        else if (x == Path.Combine("c", "my"))
        {
            return myFiles ?? [];
        }
        else if (x == Path.Combine("c"))
        {
            return rootFiles ?? [];
        }
        throw new InvalidOperationException(x);
    };
}
