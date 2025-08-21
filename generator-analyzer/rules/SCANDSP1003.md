# SCANDSP1003

*"Invalid Regex pattern in Storm Petrel settings JSON file"*

⚠️ Warning by default

## Cause

Scand Storm Petrel Generator settings JSON file contains an invalid value for a Regex property.

## Reason for rule

If a Scand Storm Petrel Generator settings JSON file contains an invalid value for a Regex property, its value and any changes to it will be ignored by the Scand Storm Petrel Generator. This behavior may be confusing for users.

## How to fix violations

Update the file content to include only valid Regex patterns according to the [configuration](../../generator/README.md#configuration).

## Examples

### Violates

```jsonc
{
    "IgnoreFilePathRegex": "(ignored"  // Unmatched '(' parenthesis creating invalid Regex
}
```

### Does not violate

```jsonc
{
    "IgnoreFilePathRegex": "ignored"
}
```
