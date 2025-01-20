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

Write-Output "Copying to $directoryPathForResult"
Copy-Item -Path "$NupkgFilePath" -Destination "$directoryPathForResult/$fileName"

$relativePaths = [System.Collections.ArrayList]::new()
$zip = [System.IO.Compression.ZipFile]::OpenRead("$directoryPathForResult/$fileName")

foreach ($entry in $zip.Entries) {
    Write-Output $entry.FullName
    if (!($entry.Name -match "Scand\.StormPetrel\..*dll")) {
        continue
    }
    $extractFileRelativePath = $entry.FullName
    #Ensure Unix-like path
    if ($extractFileRelativePath -match "\\") {
        Write-Error "Unexpected back slash in $extractFileRelativePath"
        $zip.Dispose()
        exit 1
    }
    $relativePaths.Add($extractFileRelativePath)
    $entryToExtract = $zip.GetEntry($extractFileRelativePath)
    if ($extractFileRelativePath.Length -eq $entry.Name.Length) {
        Write-Error "Unexpected case: Scand.StormPetrel dll is in the root"
        $zip.Dispose()
        exit 1
    }
    $extractFileRelativeDir = $extractFileRelativePath.Substring(0, $extractFileRelativePath.Length - $entry.Name.Length)
    $extractFileDir = "$nupkgUnzippedPath\$extractFileRelativeDir"
    if (!([System.IO.Directory]::Exists($extractFileDir))) {
        Write-Output "Creating directory $extractFileDir"
        [System.IO.Directory]::CreateDirectory($extractFileDir)
    }
    [System.IO.Compression.ZipFileExtensions]::ExtractToFile($entryToExtract, "$nupkgUnzippedPath\$extractFileRelativePath")
}
$zip.Dispose()

foreach ($relativePath in $relativePaths) {
    $fileToSignFullPath = "$nupkgUnzippedPath/$relativePath"
    if ($relativePath.EndsWith("Scand.StormPetrel.Generator.Abstraction.dll")) {
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

Write-Output "Re-packing new nuget package"
$newFilePath = "$directoryPathForResult/$fileName"
$zip = [System.IO.Compression.ZipFile]::Open($newFilePath, [System.IO.Compression.ZipArchiveMode]::Update)
foreach ($relativePath in $relativePaths) {
    if ($relativePath.EndsWith("Scand.StormPetrel.Generator.Abstraction.dll")) {
        #Already signed, no need to re-archive
        continue
    }
    foreach ($entry in $zip.Entries) {
        if ($entry.FullName -eq $relativePath) {
            $entryToDelete = $entry
        }
    }
    $entryToDelete.Delete()
    $fileToSignFullPath = "$nupkgUnzippedPath/$relativePath"
    [System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile($zip, $fileToSignFullPath, $relativePath)
}
$zip.Dispose()

Write-Output "Signing new nuget package"
dotnet nuget sign "$newFilePath" --certificate-fingerprint $CertificateFingerprintForNugetSign --timestamper http://timestamp.globalsign.com/tsa/r6advanced1
if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to sign NuGet file $newFilePath"
    exit $LASTEXITCODE
}

Write-Output "New package has been signed successfully, see the result in $newFilePath."