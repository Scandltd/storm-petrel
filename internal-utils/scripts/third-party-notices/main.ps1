#Prerequisites:
# dotnet tool install --global dotnet-project-licenses

function Create-Default {
    param (
        [Parameter(Mandatory=$true)]
        [string]$FilePath
    )

    $content = @"
 `| Reference                      `| Version  `| License Type `| License                                                       `| 
 `| ------------------------------ `| -------- `| ------------ `| ------------------------------------------------------------- `| 
 `| No third-party references      `|          `|              `|                                                               `| 
"@

    try {
        if (-not [System.IO.File]::Exists($FilePath)) {
            [System.IO.File]::WriteAllLines($FilePath, $content, [System.Text.UTF8Encoding]::new($false))
            Write-Host "File created successfully at: $FilePath" -ForegroundColor Green
        } else {
            Write-Host "File already exists: $FilePath" -ForegroundColor Green
        }
    }
    catch {
        Write-Error "Failed to create file: $_"
    }
}

Write-Output "Execute from storm-petrel repository root directory!!!"
Write-Output "The script creates THIRD_PARTY_NOTICES.md and THIRD_PARTY_NOTICES_DEV.md for all projects"

Create-Default "abstraction/THIRD_PARTY_NOTICES.md"
Create-Default "abstraction/THIRD_PARTY_NOTICES_DEV.md"
Create-Default "file-snapshot-infrastructure/THIRD_PARTY_NOTICES.md"
Create-Default "file-snapshot-infrastructure/THIRD_PARTY_NOTICES_DEV.md"
Create-Default "generator/THIRD_PARTY_NOTICES.md"
Create-Default "generator/THIRD_PARTY_NOTICES_DEV.md"

dotnet-project-licenses --input abstraction/Scand.StormPetrel.Generator.Abstraction.sln --unique --md --packages-filter internal-utils/scripts/third-party-notices/packages-filter.json --output-directory abstraction --outfile THIRD_PARTY_NOTICES.md

dotnet-project-licenses --input abstraction/Scand.StormPetrel.Generator.Abstraction.sln --unique --md --packages-filter internal-utils/scripts/third-party-notices/packages-filter.json --output-directory abstraction --outfile THIRD_PARTY_NOTICES_DEV.md

dotnet-project-licenses --input file-snapshot-infrastructure/Scand.StormPetrel.FileSnapshotInfrastructure.sln --unique --md --projects-filter internal-utils/scripts/third-party-notices/fsi-projects-filter.json --packages-filter internal-utils/scripts/third-party-notices/packages-filter.json --output-directory file-snapshot-infrastructure --outfile THIRD_PARTY_NOTICES.md

dotnet-project-licenses --input file-snapshot-infrastructure/Scand.StormPetrel.FileSnapshotInfrastructure.sln --unique --md --projects-filter internal-utils/scripts/third-party-notices/fsi-projects-filter-dev.json --packages-filter internal-utils/scripts/third-party-notices/packages-filter.json --output-directory file-snapshot-infrastructure --outfile THIRD_PARTY_NOTICES_DEV.md

dotnet-project-licenses --input generator/Scand.StormPetrel.sln --unique --md --projects-filter internal-utils/scripts/third-party-notices/generator-projects-filter.json --packages-filter internal-utils/scripts/third-party-notices/packages-filter.json --output-directory generator --outfile THIRD_PARTY_NOTICES.md

dotnet-project-licenses --input generator/Scand.StormPetrel.sln --unique --md --projects-filter internal-utils/scripts/third-party-notices/generator-projects-filter-dev.json --packages-filter internal-utils/scripts/third-party-notices/packages-filter.json --output-directory generator --outfile THIRD_PARTY_NOTICES_DEV.md
