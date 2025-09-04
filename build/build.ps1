param(
    [bool]$SkipAbstraction = $false,                                #To build the package, copy it to generator and file-snapshot-infrastructure
    [bool]$SkipGeneratorBuild = $false,                             #To build the package, execute its unit tests, and copy the package to file-snapshot-infrastructure
    [bool]$SkipGeneratorTest = $false,                              #To build and execute the package integration tests
    [bool]$SkipGeneratorTestPerformance = $false,                   #To build and execute the package performance tests in context of the integration tests. Can be utilized for development purposes to speed up the build
    [bool]$SkipFileSnapshotInfrastructureBuild = $false,            #To build the package and execute its unit tests
    [bool]$SkipFileSnapshotInfrastructureTest = $false,             #To build and execute the package integration tests
    [string]$FSIIntegrationTestGeneratorVersion = "2.6.0",          #To execute File Snapshot Infrastructure integration tests with specific version of the generator
    [bool]$SkipAnalyzerBuild = $false,                              #To build the package and execute its unit tests
    [bool]$SkipAnalyzerTest = $false                                #To build and execute the package integration tests
)

$isWin = [System.Environment]::OSVersion.Platform.ToString().StartsWith("Win")

#Use XUNIT, TEST, MEMBERDATA upper-case values to ensure case-insensitivity of TestFrameworkKindName, KindName, XUnitTestCaseSourceKindName properties
$env:SCAND_STORM_PETREL_GENERATOR_CONFIG = '{"CustomTestAttributes":[{"TestFrameworkKindName":"XUnit","FullName":"Xunit.UIFactAttribute","KindName":"Test"},{"TestFrameworkKindName":"XUnit","FullName":"Xunit.UITheoryAttribute","KindName":"Test"},{"TestFrameworkKindName":"XUnit","FullName":"Xunit.CustomFactAttribute","KindName":"Test"},{"TestFrameworkKindName":"XUnit","FullName":"Xunit.CustomTheoryAttribute","KindName":"Test"},{"TestFrameworkKindName":"XUnit","FullName":"Xunit.CustomInlineDataAttribute","KindName":"TestCase"},{"TestFrameworkKindName":"XUnit","FullName":"AutoFixture.Xunit2.InlineAutoDataAttribute","KindName":"TestCase"},{"TestFrameworkKindName":"XUnit","FullName":"Xunit.CustomMemberDataAttribute","KindName":"TestCaseSource","XUnitTestCaseSourceKindName":"MEMBERDATA"},{"TestFrameworkKindName":"XUNIT","FullName":"Xunit.CustomClassDataAttribute","KindName":"TestCaseSource","XUnitTestCaseSourceKindName":"ClassData"},{"TestFrameworkKindName":"Xunit","FullName":"xRetry.RetryFactAttribute","KindName":"Test"},{"TestFrameworkKindName":"Xunit","FullName":"xRetry.RetryTheoryAttribute","KindName":"Test"},{"TestFrameworkKindName":"NUnit","FullName":"NUnit.CustomTestAttribute","KindName":"Test"},{"TestFrameworkKindName":"NUnit","FullName":"NUnit.CustomTestCaseAttribute","KindName":"TestCase"},{"TestFrameworkKindName":"NUnit","FullName":"NUnit.CustomTestCaseSourceAttribute","KindName":"TestCaseSource"},{"TestFrameworkKindName":"MSTest","FullName":"MSTest.CustomTestMethodAttribute","KindName":"TEST"},{"TestFrameworkKindName":"MSTest","FullName":"MSTest.CustomDataRowAttribute","KindName":"TestCase"}]}'

function ClearPackageCache {
    param (
        $PackageCacheDirName
    )

    Write-Output "Clear package cache: $PackageCacheDirName"
    #Select a path according to globalPackagesFolder description in https://learn.microsoft.com/en-us/nuget/reference/nuget-config-file
    if ($isWin) {
        $cachedPackagePath = "$env:USERPROFILE/.nuget/packages/$PackageCacheDirName"
    } else {
        $cachedPackagePath = "~/.nuget/packages/$PackageCacheDirName"
    }
    if (Test-Path -Path $cachedPackagePath -PathType Container) {
        Write-Output "Removing $cachedPackagePath"
        Remove-Item -Path $cachedPackagePath -Recurse -Force
    }
}

function BuildPackage {
    param (
        $PackageDirName,
        $PackageCsprojPath
    )

    Write-Output "Build package: $PackageDirName/$PackageCsprojPath"
    dotnet build "$PackageDirName/$PackageCsprojPath" --configuration Release
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed building $PackageDirName/$PackageCsprojPath"
        exit $LASTEXITCODE
    }
}

function CopyTo {
    param (
        $FilePathTemplate,
        $DestinationDirPath
    )

    if (-not (Test-Path -Path $DestinationDirPath -PathType Container)) {
        New-Item -Path $DestinationDirPath -ItemType Directory
    }
    Copy-Item -Path $FilePathTemplate -Destination $DestinationDirPath
}

function RunUnitTest {
    param (
        $PackageDirName
    )
    
    $unitTestDirectories = Get-ChildItem -Path $PackageDirName -Filter "Scand.StormPetrel.*.Test" -Directory
    foreach ($directory in $unitTestDirectories) {
        Write-Output "Executing unit tests in directory: $($directory.Name)"
        dotnet test $directory.FullName
        if ($LASTEXITCODE -ne 0) {
            Write-Error "$PackageDirName unit tests failed. Exiting with error."
            exit $LASTEXITCODE
        }
    }
}

function RunIntegrationTests {
    param (
        $PackageDirName,
        $SolutionFileName = ""
    )
    Write-Output "Running StormPetrel tests in integration tests"
    Write-Output "We use special individual $SolutionFileName with the only integration test projects to"
    Write-Output "- allow testing of NuGet packages downloaded directly from nuget.org;"
    Write-Output "- building/running the test projects in parallel by single `donet test` command."
    if (-not (Test-Path -Path "$PackageDirName/bin" -PathType Container)) {
        #Create 'bin' directory if the package was not built and thus the directory was not created
        New-Item -Path "$PackageDirName/bin" -ItemType Directory
    }
    Write-Output "Executing StormPetrel integration tests"
    dotnet test "$PackageDirName/$SolutionFileName" --logger:junit --filter "FullyQualifiedName~StormPetrel"

    $failedItems = [System.Collections.ArrayList]::new()
    foreach ($i in (get-childitem $PackageDirName -recurse | where {$_.name -eq "TestResults.xml"} | Select-Object -Property FullName).FullName) {
        $allFailures = Select-Xml -Path "$i" -XPath '//failure' | Select-Object -ExpandProperty Node | Select-Object -ExpandProperty message
        $stormPetrelFailures = $allFailures | Select-string -Pattern 'Scand.StormPetrel.Generator.Abstraction.Exceptions.BaselineUpdatedException'
        if ($allFailures.Count -ne $stormPetrelFailures.Count) {
            $failedItems.Add($i) | Out-Null
        }
    }
    if ($failedItems.Count -ne 0) {
        Write-Warning "Some StormPetrel tests are failed with exception(s) other than BaselineUpdatedException."
        Write-Warning "See more details in the failed items below:"
        $failedItems | ConvertTo-Json | Write-Warning
        Write-Error "Aborted due to the exception(s) above"
        exit 1
    }

    Write-Output "Executing all integration tests now, including StormPetrel tests which are executed second time"
    dotnet test "$PackageDirName/$SolutionFileName"
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Aborted due to failed integration tests in $PackageDirName/$SolutionFileName"
        exit 1
    }
}

function ChangeTargetFramework {
    param (
        $FilePath,
        $NewTargetFramework = "net8.0"
    )
    $content = Get-Content -Path $FilePath -Raw
    $content = $content -replace "(<TargetFramework>).*?(</TargetFramework>)", "`$1$NewTargetFramework`$2"
    Set-Content -Path $FilePath -Value $content
}

#Stops dotnet process which locks StormPetrel log files created (and locked) by Serilog while integration tests execution.
function StopDotnetProcess {
    if (-not (Get-Process -Name "dotnet" -ErrorAction SilentlyContinue)) {
        Write-Host "Process 'dotnet' does not exist"
        return
    }
    $processIds = @()
    Get-Process -Name "dotnet" | foreach {
        $processVar = $_;$_.Modules | foreach {
            if($_.FileName -like "*StormPetrel*" -and $processIds -notcontains $processVar.id) {
                $processIds += $processVar.id
            }
        }
    }
    foreach ($processId in $processIds) {
        Stop-Process -Id $processId -Force
    }
    $counter = 10
    foreach ($processId in $processIds) {
        $process = Get-Process -Id $processId -ErrorAction SilentlyContinue
        if ($process) {
            while ((-not $process.HasExited) -and $counter -gt 0) {
                Write-Output "'dotnet' process is still running."
                Start-Sleep -Seconds 1
                $process.Refresh()
                $counter--
            }
            if ($counter -eq 0) {
                Throw "Cannot stop 'dotnet' process"
            }
        }
    }
}

function Recursively-Copy-Cs-Files {
    param (
        $SourceDirPath,
        $DestinationDirPath,
        [array]$IgnoreFileNames
    )

    Get-ChildItem -Path $SourceDirPath -Recurse -Filter *.cs |
        Where-Object { $IgnoreFileNames -notcontains $_.Name } |
        ForEach-Object {
            $targetPath = $_.FullName.Replace($SourceDirPath, $DestinationDirPath)
            if ($isWin) {
                $targetPath = $targetPath.Replace($SourceDirPath.Replace("/", "\"), $DestinationDirPath)
            }
            $targetDir = Split-Path -Path $targetPath -Parent
            if (-not (Test-Path -Path $targetDir)) {
                New-Item -Path $targetDir -ItemType Directory -ErrorAction Stop | Out-Null
            }
            Copy-Item -Path $_.FullName -Destination $targetPath -ErrorAction Stop
        }
}

ClearPackageCache "scand.stormpetrel.generator.abstraction"
if (-not $SkipAbstraction) {
    BuildPackage "abstraction" "Scand.StormPetrel.Generator.Abstraction/Scand.StormPetrel.Generator.Abstraction.csproj"
    CopyTo "abstraction/bin/Scand.StormPetrel.Generator.Abstraction.*" "generator/bin"
    CopyTo "abstraction/bin/Scand.StormPetrel.Generator.Abstraction.*" "file-snapshot-infrastructure/bin"
    CopyTo "abstraction/bin/Scand.StormPetrel.Generator.Abstraction.*" "generator-analyzer/bin"
}

ClearPackageCache "scand.stormpetrel.generator"
if (-not $SkipGeneratorBuild) {
    RunUnitTest "generator"
    #Build package in Release mode after unit tests
    BuildPackage "generator" "Scand.StormPetrel.Generator/Scand.StormPetrel.Generator.csproj"
    CopyTo "generator/bin/Scand.StormPetrel.Generator.2.*" "file-snapshot-infrastructure/bin"
    CopyTo "generator/bin/Scand.StormPetrel.Generator.2.*" "generator-analyzer/bin"
}

if (-not $SkipGeneratorTest) {
    $solutionFileName = "Scand.StormPetrel.Test.Integration.slnx"
    if ($SkipGeneratorTestPerformance) {
        #Create temporary slnx file where no performance integration tests project
        $solutionFileName = "Scand.StormPetrel.Test.Integration.NoPerformance.Build.Temp.slnx"
        $content = Get-Content -Path "generator/Scand.StormPetrel.Test.Integration.slnx" -Raw
        $performanceProjectPattern = "<Project Path=`"Test.Integration.Performance.XUnit/Test.Integration.Performance.XUnit.csproj`" />"
        $content = $content -replace $performanceProjectPattern, ""
        Set-Content -Path "generator/$solutionFileName" -Value $content
    } else {
        #Enable StormPetrel tests in performance integration tests
        $performanceTestSettingsFilePath = "generator/Test.Integration.Performance.XUnit/appsettings.StormPetrel.json"
        $performanceTestSettings = Get-Content -Path $performanceTestSettingsFilePath
        $updatedPerformanceTestSettings = $performanceTestSettings -replace '"IsDisabled": true,', '"IsDisabled": false,'
        Set-Content -Path $performanceTestSettingsFilePath -Value $updatedPerformanceTestSettings
    }
    #Disable .NETFramework tests on OS other than Windows
    if (!$isWin) {
        $content = Get-Content -Path "generator/$solutionFileName" -Raw
        $netFrameworkProjectPattern = "<Project Path=`"Test.Integration.NETFramework.NUnit/Test.Integration.NETFramework.NUnit.csproj`" />"
        $content = $content -replace $netFrameworkProjectPattern, ""
        Set-Content -Path "generator/$solutionFileName" -Value $content
    }

    #Copy *.cs files to execute integration tests under next version of .NET
    Recursively-Copy-Cs-Files "generator/Test.Integration.XUnit" "generator/Test.Integration.XUnit.NetNextVersion" -IgnoreFileNames "Utils.cs", "Utils.IgnoredMembersMiddleware.cs"
    #Copy *.cs files to execute integration tests under XUnit v3
    Recursively-Copy-Cs-Files "generator/Test.Integration.XUnit" "generator/Test.Integration.XUnitV3" -IgnoreFileNames "Utils.cs", "Utils.IgnoredMembersMiddleware.cs", "CustomInlineDataAttribute.cs", "CustomMemberDataAttribute.cs"
    #Update the signature of MemberData attribute for XUnit v3
    $testFileName = "generator/Test.Integration.XUnitV3/TestCaseSourceMemberDataTest.cs"
    $testsContent = Get-Content -Path $testFileName -Raw
    $testsContent = $testsContent -replace "parameters:", "arguments:"
    Set-Content -Path $testFileName -Value $testsContent

    RunIntegrationTests "generator" $solutionFileName
}

ClearPackageCache "scand.stormpetrel.filesnapshotinfrastructure"
if (-not $SkipFileSnapshotInfrastructureBuild) {
    RunUnitTest "file-snapshot-infrastructure"
    #Build package in Release mode after unit tests
    BuildPackage "file-snapshot-infrastructure" "Scand.StormPetrel.FileSnapshotInfrastructure/Scand.StormPetrel.FileSnapshotInfrastructure.csproj"
}

if (-not $SkipFileSnapshotInfrastructureTest) {
    #Change NETFramework TargetFramework for Custom Configuration on OS other than Windows
    if (!$isWin) {
         ChangeTargetFramework "file-snapshot-infrastructure/Test.Integration.CustomConfiguration/Test.Integration.CustomConfiguration.csproj"
         $solutionFileName = "Scand.StormPetrel.FileSnapshotInfrastructure.Test.Integration.slnx"
         $content = Get-Content -Path "file-snapshot-infrastructure/$solutionFileName" -Raw
         $winformsProjectPattern = "<Project Path=`"Test.Integration.WinFormsApp/Test.Integration.WinFormsApp.csproj`" />"
         $testProjectPattern = "<Project Path=`"Test.Integration.WinFormsAppTest/Test.Integration.WinFormsAppTest.csproj`" />"
         $wpfProjectPattern = "<Project Path=`"Test.Integration.WpfApp/Test.Integration.WpfApp.csproj`" />"
         $testWpfProjectPattern = "<Project Path=`"Test.Integration.WpfAppTest/Test.Integration.WpfAppTest.csproj`" />"
         $content = $content -replace $testProjectPattern, "" -replace $winformsProjectPattern, "" -replace $wpfProjectPattern, "" -replace $testWpfProjectPattern, ""
         Set-Content -Path "file-snapshot-infrastructure/$solutionFileName" -Value $content
    }
    #Update Scand.StormPetrel.Generator package references in integration test projects
    $testProjectFiles = Get-ChildItem -Path "file-snapshot-infrastructure" -Recurse -File | Where-Object { $_.Name -match "^Test\.Integration\..*\.csproj`$" }
    foreach ($projectFile in $testProjectFiles) {
        $content = Get-Content -Path $projectFile.FullName -Raw
        $replacement = "`${1}$FSIIntegrationTestGeneratorVersion`$3"
        $content = $content -replace "(<PackageReference Include=\""Scand.StormPetrel.Generator\"" Version=\"")([0-9\.]*)(\"")", $replacement
        Set-Content -Path $projectFile.FullName -Value $content
    }
    RunIntegrationTests "file-snapshot-infrastructure" "Scand.StormPetrel.FileSnapshotInfrastructure.Test.Integration.slnx"
}

ClearPackageCache "scand.stormpetrel.generator.analyzer"
if (-not $SkipAnalyzerBuild) {
    RunUnitTest "generator-analyzer"
    #Build package in Release mode after unit tests
    BuildPackage "generator-analyzer" "Scand.StormPetrel.Generator.Analyzer/Scand.StormPetrel.Generator.Analyzer.csproj"
}

if (-not $SkipAnalyzerTest) {
    $matchToCount = @{
        "warning SCANDSP1000" = 1
        "SCANDSP1000" = 1                    #Total SCANDSP1000 matches
        "SCANDSP1002" = 1                    #Total SCANDSP1002 matches
        "warning SCANDSP1003: Regex is invalid: \(invalid regex 1" = 2
        "warning SCANDSP1003: Regex is invalid: \(invalid regex 2" = 2
        "SCANDSP1003" = 4                    #Total SCANDSP1003 matches
        "warning SCANDSP2000: Storm Petrel cannot detect baselines to update in 'WhenTestIsNotSuitableForUpdatesThenWarningTest' test method" = 2
        "warning SCANDSP2000: Storm Petrel cannot detect baselines to update in 'WhenTestWithVariablesIsNotSuitableForUpdatesThenWarningTest' test method" = 2
        "SCANDSP2000" = 4                    #Total SCANDSP2000 matches
        "warning SCANDSP3000: Storm Petrel cannot detect baselines to update in 'AttributeListDataMethod'" = 1
        "warning SCANDSP3000: Storm Petrel cannot detect baselines to update in 'DataMethod'" = 1
        "warning SCANDSP3000: Storm Petrel cannot detect baselines to update in 'TestCaseSourceClassDataMethod'" = 1
        "warning SCANDSP3000: Storm Petrel cannot detect baselines to update in 'TestDataSourceDataMethod'" = 1
        "warning SCANDSP3000: Storm Petrel cannot detect baselines to update in 'MsTestDataMethod'" = 1
        "warning SCANDSP3000: Storm Petrel cannot detect baselines to update in 'MsTestDataSourceDataMethod'" = 1
        "warning SCANDSP3000: Storm Petrel cannot detect baselines to update in 'NUnitDataMethod'" = 1
        "warning SCANDSP3000: Storm Petrel cannot detect baselines to update in 'NUnitDataSourceDataMethod'" = 1
        "SCANDSP3000" = 8                    #Total SCANDSP3000 matches
    }
    

    $analyzerIngetrationTestSlnPath = "generator-analyzer/Scand.StormPetrel.Generator.Analyzer.Test.Integration.slnx"
    dotnet build $analyzerIngetrationTestSlnPath 2>&1 | Select-Object -Unique | ForEach-Object {
        $line = $_
        Write-Output $line
        $matchToCount.Keys.Clone() | ForEach-Object {
            $key = $_
            if ($line -match $key) {
                $matchToCount[$key]--
            }
        }
    }
   
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed building $analyzerIngetrationTestSlnPath"
        exit $LASTEXITCODE
    }
    
    $hasError = $false
    foreach ($key in $matchToCount.Keys) {
        if ($matchToCount[$key] -ne 0) {
            $hasError = $true
            Write-Warning "Wrong match count left: (Key: $key, Value: $($matchToCount[$key]))"
        }
    }
    
    if ($hasError) {
        Write-Error "Some patterns do not match. See more details above."
        exit 1
    }
    else {
        Write-Output "All expected patterns were detected"
    }
}

StopDotnetProcess

Write-Output "Build script has been executed successfully."