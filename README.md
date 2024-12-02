[![Scand Storm Petrel Expected Baselines Rewriter](generator/assets/logo-128x128-transparent.png)](https://scand.com/products/storm-petrel-expected-baselines-rewriter)
# Storm Petrel Expected Baselines Rewriter

## [Scand.StormPetrel.Generator.Abstraction](abstraction/README.md)
[![NuGet Version](http://img.shields.io/nuget/v/Scand.StormPetrel.Generator.Abstraction.svg?style=flat)](https://www.nuget.org/packages/Scand.StormPetrel.Generator.Abstraction)

.NET abstractions of the [Scand.StormPetrel.Generator](generator/README.md). They can be implemented in projects such as [Scand.StormPetrel.FileSnapshotInfrastructure](file-snapshot-infrastructure/README.md) or in custom libraries/test projects.

## [Scand.StormPetrel.Generator](generator/README.md)
[![NuGet Version](http://img.shields.io/nuget/v/Scand.StormPetrel.Generator.svg?style=flat)](https://www.nuget.org/packages/Scand.StormPetrel.Generator)

.NET Incremental Generator that creates modified copies of unit and/or integration tests to update expected baselines in original tests, automating baseline creation and accelerating test development.

[![Primary Use Case](generator/assets/primary-use-case.gif)](generator/assets/primary-use-case.gif)

## [Scand.StormPetrel.FileSnapshotInfrastructure](file-snapshot-infrastructure/README.md)
[![NuGet Version](http://img.shields.io/nuget/v/Scand.StormPetrel.FileSnapshotInfrastructure.svg?style=flat)](https://www.nuget.org/packages/Scand.StormPetrel.FileSnapshotInfrastructure)

.NET library that implements [Scand.StormPetrel.Generator.Abstraction](abstraction/README.md) to rewrite expected baseline files with actual snapshots (HTML, JSON, XML, images, or other bytes). This can be utilized in Snapshot Unit Testing when snapshots are stored as individual files in the file system.

## Media
### Blogs

- [Baseline Testing: What It Is, Why It Matters, and the Best Free Tools to Get Started](https://scand.com/company/blog/baseline-testing-importance-and-tools/)
- [Snapshot Testing in .NET with Scand Storm Petrel](https://scand.com/company/blog/snapshot-testing-in-net-with-scand-storm-petrel/)
- [Unit Testing with C# and .NET: The Complete Guide](https://medium.com/@scand.com/unit-testing-with-c-and-net-the-complete-guide-273e960ead75)

## References

At SCAND, we specialize in [building advanced .NET solutions](https://scand.com/technologies/net/) to help businesses develop new or modernize their legacy applications. If you need help getting started with Storm Petrel or support with implementation, we're ready to assist. Whether you're refactoring or rewriting, our team can help solve any challenges you might face. Visit our [page](https://scand.com/contact-us/) to learn more, or reach out for hands-on guidance.