# FAQs

## How to Test Modified Scand.StormPetrel.FileSnapshotInfrastructure Code?

* Modify and Rebuild:
    * Modify and rebuild the Scand.StormPetrel.FileSnapshotInfrastructure code in Visual Studio.
* Optional Steps:
    * Clear the NuGet package cache by executing
    ```
        rmdir /s /q "c:\Users\%USERNAME%\.nuget\packages\scand.stormpetrel.generator\"
    ```
    or using another method. Refer to <a href="https://learn.microsoft.com/en-us/nuget/consume-packages/reinstalling-and-updating-packages">Reinstalling and Updating Packages</a> for more details.
* Rebuild Test Projects:
    * Rebuild all test projects.
* Execute Tests:
    * Execute all tests suffixed with `StormPetrel` in Test Explorer. These tests should fail with a message stating `StormPetrel has regenerated baseline(s) and intentionally fails to not execute test assertions...`.
    Optionally, use the command line `dotnet test --filter "FullyQualifiedName~StormPetrel"` or a similar approach.
    * Execute all tests (both suffixed with `StormPetrel` and not suffixed). All tests should pass as the expected baselines should have been properly overwritten in the previous step.
