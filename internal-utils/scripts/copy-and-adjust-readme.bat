xcopy "%1..\README.md" "%1*" /Y
if not exist "%1..\..\internal-utils\Scand.StormPetrel.InternalUtils.TextReplacer\bin\Release\net8.0\Scand.StormPetrel.InternalUtils.TextReplacer.exe" (
  dotnet build -c Release "%1..\..\internal-utils\Scand.StormPetrel.InternalUtils.TextReplacer\Scand.StormPetrel.InternalUtils.TextReplacer.csproj"
)
"%1..\..\internal-utils\Scand.StormPetrel.InternalUtils.TextReplacer\bin\Release\net8.0\Scand.StormPetrel.InternalUtils.TextReplacer.exe" "%1\README.Replacement.json" "%1\README.md"