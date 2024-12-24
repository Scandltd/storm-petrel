param(
    [string]$SignToolPath = ".\SignTool.exe",
    [string]$NupkgFilePath = "",
    [string]$CertificateThumbprintForSignTool = "",
    [string]$CertificateFingerprintForNugetSign = ""    #CAUTION: At the current moment the same SHA1 value can be used for both "thumb/finger print" parameters.
                                                        #However, NU3043 warning will be promoted to an error around the .NET 10 timeframe: https://learn.microsoft.com/en-us/nuget/reference/errors-and-warnings/nu3043
)

$directoryPath = Split-Path -Path "$NupkgFilePath" -Parent
$fileName = Split-Path -Path "$NupkgFilePath" -Leaf
$isAbstractionFileName = $fileName -match "Scand\.StormPetrel\.Generator\.Abstraction\."

$currentDateTime = Get-Date
$directoryPathForResult = ""
do {
    $currentDateTimeAsString = $currentDateTime.ToString("yyyy-MM-ddTHH-mm-ss")
    $directoryPathForResult = "$directoryPath/Scand.StormPetrel.Sign.Result-$currentDateTimeAsString"
    $currentDateTime = $currentDateTime.AddSeconds(1)
} while (Test-Path -Path $directoryPathForResult -PathType Any)

$nupkgUnzippedPath = "$directoryPathForResult/nupkg-unzipped"
New-Item -Path "$directoryPathForResult" -ItemType Directory
New-Item -Path "$nupkgUnzippedPath" -ItemType Directory

Write-Output "Unzipping to $nupkgUnzippedPath"
Copy-Item -Path "$NupkgFilePath" -Destination "$directoryPathForResult/tmp.zip"
Expand-Archive -LiteralPath "$directoryPathForResult/tmp.zip" -DestinationPath $nupkgUnzippedPath
Remove-Item -Path "$directoryPathForResult/tmp.zip"

$filesToSign = Get-ChildItem -Path $nupkgUnzippedPath -Filter "Scand.StormPetrel.*.dll" -File -Recurse
foreach ($fileToSign in $filesToSign) {
    $fileToSignFullPath = "$($fileToSign.FullName)"
    if (-not $isAbstractionFileName -and $fileToSign.Name -eq "Scand.StormPetrel.Generator.Abstraction.dll") {
        & "$SignToolPath" verify /pa "$fileToSignFullPath"
        if ($LASTEXITCODE -ne 0) {
            Write-Error "$fileToSignFullPath must already be signed in context of building of $fileName"
            exit $LASTEXITCODE
        }
        continue
    }
    Write-Output "Signing $fileToSignFullPath"
    & "$SignToolPath" sign /sha1 $CertificateThumbprintForSignTool /td sha256 /fd sha256 /as /v /tr http://timestamp.globalsign.com/tsa/r6advanced1 "$fileToSignFullPath"
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Failed to sign $fileToSignFullPath"
        exit $LASTEXITCODE
    }
}

Write-Output "Zipping new nuget package"
$newFilePath = "$directoryPathForResult/$fileName"
Compress-Archive -Path "$nupkgUnzippedPath/*" -DestinationPath "$newFilePath.zip"
Move-Item -Path "$newFilePath.zip" -Destination "$newFilePath"

Write-Output "Signing new nuget package"
dotnet nuget sign "$newFilePath" --certificate-fingerprint $CertificateFingerprintForNugetSign --timestamper http://timestamp.globalsign.com/tsa/r6advanced1
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to sign NuGet file $newFilePath"
    exit $LASTEXITCODE
}

Write-Output "New package has been signed successfully, see the result in $newFilePath."