# FAQs

## How to Test Modified Scand.StormPetrel.SourceGenerator Code?

* Modify and Rebuild:
    * Modify and rebuild the Scand.StormPetrel.Generator code or its components in Visual Studio.
* Optional Steps:
    * Close Visual Studio and terminate the VBCSCompiler.exe process.
    * Clear the NuGet package cache by executing `clear-nuget-package-cache-from-stormpetrel.bat` or using another method. Refer to <a href="https://learn.microsoft.com/en-us/nuget/consume-packages/reinstalling-and-updating-packages">Reinstalling and Updating Packages</a> for more details.
    * Set `"IsDisabled": false` in <a href="Test.Integration.Performance.XUnit/appsettings.StormPetrel.json">Test.Integration.Performance.XUnit/appsettings.StormPetrel.json</a> if performance testing is required.
* Rebuild Test Projects:
    * Rebuild all test projects.
* Execute Tests:
    * Run the `Scand.StormPetrel.Generator.Test.csproj` and `Scand.StormPetrel.Rewriter.Test.csproj` tests.
* Integration Testing Steps (excluding `Scand.StormPetrel.Generator.Test.csproj` and `Scand.StormPetrel.Rewriter.Test.csproj` tests):
    * Execute all tests suffixed with `StormPetrel` in Test Explorer. These tests should fail with a message stating `StormPetrel has regenerated baseline(s) and intentionally fails to not execute test assertions...`. Optionally, use the command line `dotnet test --filter "FullyQualifiedName~StormPetrel"` or a similar approach.
    * Execute all tests again. Ensure the tests are recompiled at this step. All tests should pass as the expected baselines should have been properly overwritten in the previous step.
