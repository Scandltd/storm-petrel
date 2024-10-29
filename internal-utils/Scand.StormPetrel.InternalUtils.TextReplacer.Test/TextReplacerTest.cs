namespace Scand.StormPetrel.InternalUtils.TextReplacer.Test
{
    public class TextReplacerTest
    {
        [Theory]
        [InlineData("primary",
@"[![Scand Storm Petrel Generator Abstraction](assets/logo-128x128-transparent.png)](https://scand.com/products/storm-petrel-expected-baselines-rewriter)
# Scand Storm Petrel Generator Abstraction
* [Overview](#overview)
* [Getting Started](#getting-started)
* [References](#references)

## Overview

Abstractions for Scand Storm Petrel Incremental Generator.

## Getting Started
To create NuGet package or .NET project with abstractions custom implementation:
* Reference Scand.StormPetrel.Generator.Abstraction NuGet package in .NET project.
* Implement any or all of Scand.StormPetrel.Generator.Abstraction interfaces in the project.

Then the project can be referenced in .NET test project and configured according to [Scand.StormPetrel.Generator](../generator/README.md) documentation to have the customization applied.


## References",
@"[![Scand Storm Petrel Generator Abstraction](https://raw.githubusercontent.com/Scandltd/storm-petrel/main/abstraction/assets/logo-128x128-transparent.png)](https://scand.com/products/storm-petrel-expected-baselines-rewriter)
# Scand Storm Petrel Generator Abstraction
* [Overview](#overview)
* [Getting Started](#getting-started)
* [References](#references)

## Overview

Abstractions for Scand Storm Petrel Incremental Generator.

## Getting Started
To create NuGet package or .NET project with abstractions custom implementation:
* Reference Scand.StormPetrel.Generator.Abstraction NuGet package in .NET project.
* Implement any or all of Scand.StormPetrel.Generator.Abstraction interfaces in the project.

Then the project can be referenced in .NET test project and configured according to [Scand.StormPetrel.Generator](https://github.com/Scandltd/storm-petrel/blob/main/abstraction/../generator/README.md) documentation to have the customization applied.


## References"
            )]
        [InlineData("simplified",
@"[![Scand Storm Petrel Generator Abstraction](assets/logo-128x128-transparent.png)](https://scand.com/products/storm-petrel-expected-baselines-rewriter)
# Scand Storm Petrel Generator Abstraction
* [Overview](#overview)
* [Getting Started](#getting-started)
* [References](#references)

## Overview

Abstractions for Scand Storm Petrel Incremental Generator.

## Getting Started
To create NuGet package or .NET project with abstractions custom implementation:
* Reference Scand.StormPetrel.Generator.Abstraction NuGet package in .NET project.
* Implement any or all of Scand.StormPetrel.Generator.Abstraction interfaces in the project.

Then the project can be referenced in .NET test project and configured according to [Scand.StormPetrel.Generator](../generator/README.md) documentation to have the customization applied.


## References",
@"[![Scand Storm Petrel Generator Abstraction](https://raw.githubusercontent.com/Scandltd/storm-petrel/main/abstraction/assets/logo-128x128-transparent.png)](https://scand.com/products/storm-petrel-expected-baselines-rewriter)
# Scand Storm Petrel Generator Abstraction
* [Overview](#overview)
* [Getting Started](#getting-started)
* [References](#references)

## Overview

Abstractions for Scand Storm Petrel Incremental Generator.

## Getting Started
To create NuGet package or .NET project with abstractions custom implementation:
* Reference Scand.StormPetrel.Generator.Abstraction NuGet package in .NET project.
* Implement any or all of Scand.StormPetrel.Generator.Abstraction interfaces in the project.

Then the project can be referenced in .NET test project and configured according to [Scand.StormPetrel.Generator](https://github.com/Scandltd/storm-petrel/blob/main/abstraction/../generator/README.md) documentation to have the customization applied.


## References"
            )]
        public void ReplaceTest(string configId, string inputText, string expected)
        {
            //Arrange
            var config = GetConfig(configId);

            //Act
            var actual = TextReplacer.Replace(config, inputText);

            //Assert
            Assert.Equal(expected, actual);

            static ConfigModel GetConfig(string id) => id switch
            {
                "primary" => new()
                {
                    BaseUrls =
                    [
                        "https://raw.githubusercontent.com/Scandltd/storm-petrel/main/abstraction/",
                        "https://github.com/Scandltd/storm-petrel/blob/main/abstraction/",
                    ],
                    Replacements =
                    [
                        new ()
                        {
                            Regex = @"(]\()(assets)([^\)]*)(\))",
                            ReplacePattern = "$1{BaseUrls[0]}$2$3$4",
                        },
                        new ()
                        {
                            Regex = @"(]\()((?!http|#)[^\)]*)(\))",
                            ReplacePattern = "$1{BaseUrls[1]}$2$3",
                        },
                    ],
                },
                "simplified" => new()
                {
                    RootFolderName = "abstraction",
                },
                _ => throw new InvalidOperationException(),
            };
        }
    }
}