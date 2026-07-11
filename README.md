# Auto Anchor Tool for Unity

Auto Anchor is a lightweight Unity Editor package for positioning UI `RectTransform` anchors without changing the element's visible bounds.

## Install from Git

Add this dependency to the target project's `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.mixadze.autoanchor": "https://github.com/mixadze1/unity-auto-anchors.git#upm"
  }
}
```

Unity Package Manager downloads the package from the dedicated `upm` branch. Reopen
Unity or open Package Manager to resolve it.

## Commands

| Command | Default shortcut | What it does |
| --- | --- | --- |
| `Tools/AutoAnchor/Border` | `Shift+[` | Places anchor min/max on the selected element's visible bounds in its parent. |
| `Tools/AutoAnchor/Center` | `Shift+]` | Collapses both anchors to the selected element's geometric center while preserving its visible bounds. |
| `Tools/AutoAnchor/Settings` | — | Opens shortcut configuration. |

The shortcuts work globally in the Unity Editor: select one or more `RectTransform` objects in the Hierarchy, Inspector, or Scene view, then invoke the command. Multi-object selection is processed independently for every selected `RectTransform`.

## Configuration

Open `Tools/AutoAnchor/Settings`. The tool creates `Assets/AutoAnchorConfig.asset` when needed and lets you rebind Border and Center shortcuts. The settings window applies changes to the active Unity shortcut profile.

## Requirements

- Unity 2022.3 or later
- Git installed and available on the machine path, so Unity can resolve the Git package URL

## Package layout

```text
Packages/com.autoanchor.tool/
├── Runtime/  # AutoAnchorConfig ScriptableObject and shortcut data
├── Editor/   # commands, global shortcuts, settings window
├── README.md
└── CHANGELOG.md
```

## Releases

- [ma-auto-anchors-v1.0.3](https://github.com/mixadze1/unity-auto-anchors/tree/ma-auto-anchors-v1.0.3) — dedicated `upm` branch and the public package name `com.mixadze.autoanchor`.
- [ma-auto-anchors-v1.0.2](https://github.com/mixadze1/unity-auto-anchors/tree/ma-auto-anchors-v1.0.2) — public documentation and multi-object workflow.
- [ma-auto-anchors-v1.0.1](https://github.com/mixadze1/unity-auto-anchors/tree/ma-auto-anchors-v1.0.1) — global editor shortcuts and settings integration.
- [ma-auto-anchors-v1.0.0](https://github.com/mixadze1/unity-auto-anchors/tree/ma-auto-anchors-v1.0.0) — initial package release.
