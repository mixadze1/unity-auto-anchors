# Changelog

## ma-auto-anchors-v1.0.1 - 2026-07-11

- Moved Border and Center shortcuts to Unity's global Shortcut Manager.
- Shortcuts now work when a `RectTransform` is selected in the Hierarchy or Inspector, without Scene view focus.

## ma-auto-anchors-v1.0.0 - 2026-07-11

- Initial public UPM release.
- `Tools/AutoAnchor/Border` anchors a selected UI element to its visible parent bounds.
- `Tools/AutoAnchor/Center` collapses anchors to the RectTransform geometric center while preserving visible bounds.
- `Tools/AutoAnchor/Settings` manages the `AutoAnchorConfig` ScriptableObject and shortcut bindings.
