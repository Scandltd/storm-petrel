[![Scand Storm Petrel Analyzers](assets/logo-128x128-transparent.png)](https://scand.com/products/storm-petrel-expected-baselines-rewriter)
# Scand Storm Petrel Analyzers
* [Overview](#overview)
* [Rules](#rules)
* [Getting Started](#getting-started)
* [Supported Software](#supported-software)
* [CHANGELOG](#changelog)
* [References](#references)

## Overview
[![NuGet Version](http://img.shields.io/nuget/v/Scand.StormPetrel.Generator.Analyzer.svg?style=flat)](https://www.nuget.org/packages/Scand.StormPetrel.Generator.Analyzer)

Analyzers for .NET test projects to ensure their code and configuration are compatible with [Scand Storm Petrel's](../generator/README.md) expected baseline updates.

## Rules

* Storm Petrel Settings File Rules
    * [SCANDSP1000](rules/SCANDSP1000.md) *"Unreferenced Storm Petrel settings JSON file"*.
    * [SCANDSP1001](rules/SCANDSP1001.md) *"Malformed Storm Petrel settings JSON file"*.
    * [SCANDSP1002](rules/SCANDSP1002.md) *"Storm Petrel settings JSON file contains redundant fields"*.
    * [SCANDSP1003](rules/SCANDSP1003.md) *"Invalid Regex pattern in Storm Petrel settings JSON file"*.
* Test Method Rules
    * [SCANDSP2000](rules/SCANDSP2000.md) *"Scand Storm Petrel cannot detect baselines to update in test method"*.
* Test Method Data Source Rules
    * [SCANDSP3000](rules/SCANDSP3000.md) *"Test data source method design unsuitable for baseline updates"*.

## Getting Started

* Add [Scand.StormPetrel.Generator.Analyzer](https://www.nuget.org/packages/Scand.StormPetrel.Generator.Analyzer) NuGet Package reference to your .NET test project.
* Build the project. Analyzer diagnostics are reported in the build output or displayed in the Error List tab of the IDE.

## Supported Software

See [Storm Petrel Generator Supported Software](../generator/README.md#supported-software) for more details.

## CHANGELOG

See [CHANGELOG](CHANGELOG.md) for more details.

## References

At SCAND, we specialize in [building advanced .NET solutions](https://scand.com/technologies/net/) to help businesses develop new or modernize their legacy applications. If you need help getting started with Storm Petrel or support with implementation, we're ready to assist. Whether you're refactoring or rewriting, our team can help solve any challenges you might face. Visit our [page](https://scand.com/contact-us/) to learn more, or reach out for hands-on guidance.