// Disable test parallelization to avoid tests instability. The instability root cause is:
// - `Test.Integration.WpfApp.MainWindow` constructor calls auto-generated `InitializeComponent` which calls `System.Windows.Application.LoadComponent` which is not thread-safe [1].
// - Current test project has more than one test class (including StormPetrel generated classes) instantiating the `MainWindow`.
// - The test classes are executed by xUnit in parallel by default what sometimes results in an exception due to `LoadComponent` thread-unsafety.
// References:
// [1] https://learn.microsoft.com/en-us/dotnet/api/system.windows.application.loadcomponent?view=windowsdesktop-9.0#system-windows-application-loadcomponent(system-object-system-uri)
[assembly: CollectionBehavior(DisableTestParallelization = true)]
