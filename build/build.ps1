param(
    [bool]$SkipAbstraction = $false,                                #To build the package, copy it to generator and file-snapshot-infrastructure
    [bool]$SkipGeneratorBuild = $false,                             #To build the package, execute its unit tests, and copy the package to file-snapshot-infrastructure
    [bool]$SkipGeneratorTest = $false,                              #To build and execute the package integration tests
    [bool]$SkipGeneratorTestPerformance = $false,                   #To build and execute the package performance tests in context of the integration tests. Can be utilized for development purposes to speed up the build
    [bool]$SkipFileSnapshotInfrastructureBuild = $false,            #To build the package and execute its unit tests
    [bool]$SkipFileSnapshotInfrastructureTest = $false,             #To build and execute the package integration tests
    [string]$FSIIntegrationTestGeneratorVersion = "2.2.1"           #To execute File Snapshot Infrastructure integration tests with specific version of the generator
)

$isWin = [System.Environment]::OSVersion.Platform.ToString().StartsWith("Win")

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
}

ClearPackageCache "scand.stormpetrel.generator"
if (-not $SkipGeneratorBuild) {
    RunUnitTest "generator"
    #Build package in Release mode after unit tests
    BuildPackage "generator" "Scand.StormPetrel.Generator/Scand.StormPetrel.Generator.csproj"
    CopyTo "generator/bin/Scand.StormPetrel.Generator.2.*" "file-snapshot-infrastructure/bin"
}

if (-not $SkipGeneratorTest) {
    $solutionFileName = "Scand.StormPetrel.Test.Integration.sln"
    if ($SkipGeneratorTestPerformance) {
        #Create temporary sln file where no performance integration tests project
        $solutionFileName = "Scand.StormPetrel.Test.Integration.NoPerformance.Build.Temp.sln"
        $content = Get-Content -Path "generator/Scand.StormPetrel.Test.Integration.sln" -Raw
        $performanceProjectPattern = "(Project[^\r\n]*Test\.Integration\.Performance[^\r\n]*[\r\n]*[^\r\n]*EndProject[\r\n]*)|([^\r\n]*A7504986-1E11-483C-8AF4-395CF2E25ED1[^\r\n]*[\r\n]*)"
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
        $netFrameworkProjectPattern = "(Project[^\r\n]*Test\.Integration\.NETFramework[^\r\n]*[\r\n]*[^\r\n]*EndProject[\r\n]*)|([^\r\n]*CB999DAC-9870-4DFE-87D9-4F26A0B6538E[^\r\n]*[\r\n]*)"
        $content = $content -replace $netFrameworkProjectPattern, ""
        Set-Content -Path "generator/$solutionFileName" -Value $content
    }

    #Copy *.cs files to execute integration tests under next version of .NET
    Recursively-Copy-Cs-Files "generator/Test.Integration.XUnit" "generator/Test.Integration.XUnit.NetNextVersion" -IgnoreFileNames "Utils.cs", "Utils.IgnoredMembersMiddleware.cs"
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
         $solutionFileName = "Scand.StormPetrel.FileSnapshotInfrastructure.Test.Integration.sln"
         $content = Get-Content -Path "file-snapshot-infrastructure/$solutionFileName" -Raw
         $winformsProjectPattern = "(Project[^\r\n]*Test\.Integration\.WinFormsApp[^\r\n]*[\r\n]*[^\r\n]*EndProject[\r\n]*)|([^\r\n]*0CC818D1-A409-41C5-A1D5-C9603F37FBDC[^\r\n]*[\r\n]*)"
         $testProjectPattern = "(Project[^\r\n]*Test\.Integration\.WinFormsAppTest[^\r\n]*[\r\n]*[^\r\n]*EndProject[\r\n]*)|([^\r\n]*D1255A63-B3E0-4488-8D1B-C4687FE4E64D[^\r\n]*[\r\n]*)"
         $wpfProjectPattern = "(Project[^\r\n]*Test\.Integration\.WpfApp[^\r\n]*[\r\n]*[^\r\n]*EndProject[\r\n]*)|([^\r\n]*38F13E7C-F75B-4E20-A093-88F4EF2E471E[^\r\n]*[\r\n]*)"
         $testWpfProjectPattern = "(Project[^\r\n]*Test\.Integration\.WpfAppTest[^\r\n]*[\r\n]*[^\r\n]*EndProject[\r\n]*)|([^\r\n]*12DBAA41-31DA-455A-8E42-A9B68E16114C[^\r\n]*[\r\n]*)"
         $content = $content -replace $testProjectPattern, "" -replace $winformsProjectPattern, "" -replace $wpfProjectPattern, "" -replace $testWpfProjectPattern, ""
         Set-Content -Path "file-snapshot-infrastructure/$solutionFileName" -Value $content
    }
    #Update 2.0.0 Scand.StormPetrel.Generator package references in integration test projects
    if ($FSIIntegrationTestGeneratorVersion -ne "2.0.0") {
        $testProjectFiles = Get-ChildItem -Path "file-snapshot-infrastructure" -Recurse -File | Where-Object { $_.Name -match "^Test\.Integration\..*\.csproj`$" }
        foreach ($projectFile in $testProjectFiles) {
            $content = Get-Content -Path $projectFile.FullName -Raw
            $replacement = "`${1}$FSIIntegrationTestGeneratorVersion`$3"
            $content = $content -replace "(<PackageReference Include=\""Scand.StormPetrel.Generator\"" Version=\"")(2\.0\.0)(\"")", $replacement
            Set-Content -Path $projectFile.FullName -Value $content
        }
    }
    RunIntegrationTests "file-snapshot-infrastructure" "Scand.StormPetrel.FileSnapshotInfrastructure.Test.Integration.sln"
}

StopDotnetProcess

Write-Output "Build script has been executed successfully."