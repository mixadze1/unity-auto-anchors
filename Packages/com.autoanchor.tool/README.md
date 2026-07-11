# Auto Anchor Tool

A Unity Package Manager tool for placing UI `RectTransform` anchors without changing the element's visual bounds.

## Installation

Add the following entry to the target project's `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.autoanchor.tool": "https://github.com/mixadze1/unity-auto-anchors.git?path=/Packages/com.autoanchor.tool#ma-auto-anchors-v1.0.0"
  }
}
```

The `#ma-auto-anchors-v1.0.0` suffix pins the package to this release. Omit it only when you intentionally want the latest default-branch version.

## Commands

| Command | Default Scene view shortcut | Result |
| --- | --- | --- |
| `Tools/AutoAnchor/Border` | `Shift+[` | Moves the minimum and maximum anchors to the selected element's visible bounds in its parent. |
| `Tools/AutoAnchor/Center` | `Shift+]` | Collapses both anchors to the selected element's geometric center in its parent while preserving its visible bounds. |

Shortcuts are handled while the Scene view has focus and a `RectTransform` is selected.

## Configuration

Choose `Tools/AutoAnchor/Settings` to open the settings window. It automatically creates `Assets/AutoAnchorConfig.asset` on first use and lets you edit both shortcut combinations, reset their defaults, or select the underlying ScriptableObject. Each field and button has a tooltip.

Keep one `AutoAnchorConfig` asset per project. If multiple assets exist, the tool uses the first one by asset GUID.

## Package layout

- `Runtime` contains the public `AutoAnchorConfig` and shortcut data type, so configuration can be referenced by other project code.
- `Editor` contains menu commands, Scene view shortcut handling, undo support, and prefab override recording.
