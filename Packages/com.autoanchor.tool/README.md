# Auto Anchor Tool [![Unity 2022.3+](https://img.shields.io/badge/Unity-2022.3%2B-blue.svg)](https://unity.com/releases/editor/whats-new/2022.3.0) [![GitHub Release](https://img.shields.io/github/v/release/mixadze1/unity-auto-anchors?display_name=tag&sort=semver)](https://github.com/mixadze1/unity-auto-anchors/releases) [![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](LICENSE.md)

A Unity Package Manager tool for placing UI `RectTransform` anchors without changing the element's visual bounds.

## Features

- Anchor to visible bounds or geometric center without moving the UI element.
- Global, configurable shortcuts for Border and Center commands.
- Multi-object `RectTransform` processing in one action.

## Installation

Add the following entry to the target project's `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.mixadze.autoanchor": "https://github.com/mixadze1/unity-auto-anchors.git#upm"
  }
}
```

The `upm` branch contains only the package files at the repository root, so no
`?path=` suffix is required. Install a specific release by replacing `upm` with a
tag created from the same package branch.

## Requirements

- Unity **2022.3** or later
- Git installed and available on the machine path

## Commands

| Command | Default Scene view shortcut | Result |
| --- | --- | --- |
| `Tools/AutoAnchor/Border` | `Shift+[` | Moves the minimum and maximum anchors to the selected element's visible bounds in its parent. |
| `Tools/AutoAnchor/Center` | `Shift+]` | Collapses both anchors to the selected element's geometric center in its parent while preserving its visible bounds. |

Shortcuts use Unity's global Shortcut Manager, so they work with the Hierarchy, Inspector, Scene view, or another editor window focused, as long as at least one `RectTransform` is selected.

Both commands support multi-object selection: every selected `RectTransform` is processed independently in one action.

## Configuration

Choose `Tools/AutoAnchor/Settings` to open the settings window. It automatically creates `Assets/AutoAnchorConfig.asset` on first use and lets you edit both shortcut combinations, reset their defaults, or select the underlying ScriptableObject. Each field and button has a tooltip.

Keep one `AutoAnchorConfig` asset per project. If multiple assets exist, the tool uses the first one by asset GUID.

## Package layout

- `Runtime` contains the public `AutoAnchorConfig` and shortcut data type, so configuration can be referenced by other project code.
- `Editor` contains menu commands, Scene view shortcut handling, undo support, and prefab override recording.

## License

Distributed under the [MIT License](LICENSE.md).
