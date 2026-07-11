# Auto Anchor Tool

An embedded Unity package for placing UI `RectTransform` anchors without changing the element's visual bounds.

## Commands

| Command | Default Scene view shortcut | Result |
| --- | --- | --- |
| `Tools/Auto Anchor/Anchor to Bounds` | `Shift+[` | Moves the minimum and maximum anchors to the selected element's visible bounds in its parent. |
| `Tools/Auto Anchor/Anchor to Center` | `Shift+]` | Collapses both anchors to the selected element's geometric center in its parent while preserving its visible bounds. |

Shortcuts are handled while the Scene view has focus and a `RectTransform` is selected.

## Configuration

Choose `Tools/Auto Anchor/Create Configuration` to create `Assets/AutoAnchorConfig.asset`. Select that asset and edit its two shortcut entries in the Inspector. The package uses `Shift+[` and `Shift+]` until a configuration asset is created.

Keep one `AutoAnchorConfig` asset per project. If multiple assets exist, the tool uses the first one by asset GUID.

## Package layout

- `Runtime` contains the public `AutoAnchorConfig` and shortcut data type, so configuration can be referenced by other project code.
- `Editor` contains menu commands, Scene view shortcut handling, undo support, and prefab override recording.
