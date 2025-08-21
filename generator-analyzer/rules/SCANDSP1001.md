# SCANDSP1001

*"Malformed Storm Petrel settings JSON file"*

⚠️ Warning by default

## Cause

Scand Storm Petrel Generator settings JSON file contains malformed JSON text.

## Reason for rule

If a Scand Storm Petrel Generator settings JSON file contains malformed JSON text, its values and any changes to them will be ignored by the Scand Storm Petrel Generator. This behavior may be confusing for users.

## How to fix violations

Update the file content to contain well-formed JSON.

## Examples

### Violates

```jsonc
{
    "IsDisabled: true  // Missing closing double-quote for the IsDisabled property name
}
```

### Does not violate

```jsonc
{
    "IsDisabled": true
}
```