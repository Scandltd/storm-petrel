# SCANDSP1002

*"Storm Petrel settings JSON file contains redundant fields"*

⚠️ Warning by default

## Cause

Scand Storm Petrel Generator settings JSON file contains redundant JSON fields.

## Reason for rule

If a Scand Storm Petrel Generator settings JSON file contains redundant fields, their values and any changes to them will be ignored by the Scand Storm Petrel Generator. This behavior may be confusing for users.

## How to fix violations

Update the file content to include only the fields specified in the [configuration](../../generator/README.md#configuration).

## Examples

### Violates

```jsonc
{
    "IsDisabld": true  // Typo in the IsDisabled property name
}
```

### Does not violate

```jsonc
{
    "IsDisabled": true
}
```