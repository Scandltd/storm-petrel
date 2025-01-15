$sourceFile = Join-Path $args[0] "..\README.md"
$destinationFile = $args[0]
$isWin = [System.Environment]::OSVersion.Platform.ToString().StartsWith("Win")

Copy-Item -Path $sourceFile -Destination $destinationFile -Force

$textReplacerBinDir = Join-Path $args[0] "..\..\internal-utils\Scand.StormPetrel.InternalUtils.TextReplacer\bin\Release\net8.0"
$textReplacerProj = Join-Path $args[0] "..\..\internal-utils\Scand.StormPetrel.InternalUtils.TextReplacer\Scand.StormPetrel.InternalUtils.TextReplacer.csproj"

if (-Not (Test-Path -Path $textReplacerBinDir)) {
    Write-Output "Building $textReplacerProj"
    $selfContained = "false"
    if (!$isWin) {
        $selfContained = "true"
    }
    dotnet build -c Release $textReplacerProj --self-contained $selfContained
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Build failed."
        exit $LASTEXITCODE
    }
}

$textReplacerBinFileName = "Scand.StormPetrel.InternalUtils.TextReplacer.exe"
if (!$isWin) {
    $textReplacerBinFileName = "Scand.StormPetrel.InternalUtils.TextReplacer"
}

$textReplacerFile = Get-ChildItem -Path $textReplacerBinDir -Filter $textReplacerBinFileName -Recurse | Select-Object -First 1
$textReplacerExe = ""
if ($textReplacerFile) {
    $textReplacerExe = $textReplacerFile.FullName
} else {
    Write-Error "$textReplacerBinFileName is not found in $textReplacerBinDir"
    exit 1
}

$replacementJson = Join-Path $args[0] "README.Replacement.json"
$readmeFile = Join-Path $args[0] "README.md"

Write-Output "Replacing hyperlinks"
& $textReplacerExe $replacementJson $readmeFile
if ($LASTEXITCODE -ne 0) {
    Write-Error "The replacement failed."
    exit $LASTEXITCODE
}
