# Stops processes by input process name.
# The script is useful in some development scenarios,
# e.g. when need to be sure that no dotnet processes exist where StormPetrel analyzer can be already loaded.

param(
    [string]$ProcName = "dotnet"
)

$processes = Get-Process $ProcName -ErrorAction SilentlyContinue | Select-Object Id, StartTime

if ($processes.Count -gt 0) {
    Write-Output "Stopping processes:"
    try {
        $processes | ForEach-Object {
            Write-Output "Terminating PID $($_.Id)"
            Stop-Process -Id $_.Id -Force -ErrorAction Stop
        }
    }
    catch {
        Write-Warning "Stopping failed: $_"
        $exitCode = 1
    }
    Write-Output "Stopping processes done!"
} else {
    Write-Output "No processes found"
}
