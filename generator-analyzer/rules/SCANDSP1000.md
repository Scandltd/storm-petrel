# SCANDSP1000

*"Unreferenced Storm Petrel settings JSON file"*

⚠️ Warning by default

## Cause

Scand Storm Petrel Generator settings JSON file exists in the test project directory but is not referenced as `C# analyzer additional file` in the project.

## Reason for rule

If a Scand Storm Petrel Generator settings JSON file exists but is not referenced as `C# analyzer additional file` in the test project, its values and any changes to them will be ignored by Scand Storm Petrel Generator. This behavior may be confusing for users.

## How to fix violations

Set the `Build Action` property to `C# analyzer additional file` for the JSON file in Visual Studio:

1. Right-click the file in Solution Explorer
2. Select `Properties`
3. In the Properties window, set `Advanced > Build Action` to `C# analyzer additional file`
4. Save the project

This will add XML similar to the following to your project file in the case of `appsettings.StormPetrel.json` file name:

```xml
  <ItemGroup>
    <AdditionalFiles Include="appsettings.StormPetrel.json" />
  </ItemGroup>
```

Use equivalent functionality in other IDEs.

## Examples

### Violates

An `appsettings.StormPetrel.json` file exists in the test project directory but is not referenced as `C# analyzer additional file` in the project.

### Does not violate

An `appsettings.StormPetrel.json` file exists in the test project directory and is properly referenced as `C# analyzer additional file` in the project.
