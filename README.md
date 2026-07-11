# Auto Anchor Tool [![Unity 2022.3+](https://img.shields.io/badge/Unity-2022.3%2B-blue.svg)](https://unity.com/releases/editor/whats-new/2022.3.0) [![GitHub Release](https://img.shields.io/github/v/release/mixadze1/unity-auto-anchors?display_name=tag&sort=semver)](https://github.com/mixadze1/unity-auto-anchors/releases) [![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](LICENSE)

Auto Anchor is a lightweight Unity Editor package for positioning UI `RectTransform` anchors without changing the element's visible bounds.

## Features

- **Border** — moves anchors to the element's visible bounds in its parent.
- **Center** — collapses anchors to the geometric center while preserving visual bounds.
- **Global shortcuts** — work from the Hierarchy, Inspector, Scene view, and other editor windows.
- **Multi-object selection** — processes every selected `RectTransform` independently.
- **ScriptableObject configuration** — rebind shortcuts from `Tools/AutoAnchor/Settings`.

## Installation

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

To pin a production build, replace `upm` with a release tag, for example
`ma-auto-anchors-v1.0.4`.

## Commands

| Command | Default shortcut | What it does |
| --- | --- | --- |
| `Tools/AutoAnchor/Border` | `Shift+[` | Places anchor min/max on the selected element's visible bounds in its parent. |
| `Tools/AutoAnchor/Center` | `Shift+]` | Collapses both anchors to the selected element's geometric center while preserving its visible bounds. |
| `Tools/AutoAnchor/Settings` | — | Opens shortcut configuration. |

The shortcuts work globally in the Unity Editor: select one or more `RectTransform` objects in the Hierarchy, Inspector, or Scene view, then invoke the command. Multi-object selection is processed independently for every selected `RectTransform`.

## Configuration

Open `Tools/AutoAnchor/Settings`. The tool creates `Assets/AutoAnchorConfig.asset` when needed and lets you rebind Border and Center shortcuts. The settings window applies changes to the active Unity shortcut profile.

## System Requirements

- Unity **2022.3** or later
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

- [ma-auto-anchors-v1.0.4](https://github.com/mixadze1/unity-auto-anchors/tree/ma-auto-anchors-v1.0.4) — MIT licensing and package documentation badges.
- [ma-auto-anchors-v1.0.3](https://github.com/mixadze1/unity-auto-anchors/tree/ma-auto-anchors-v1.0.3) — dedicated `upm` branch and the public package name `com.mixadze.autoanchor`.
- [ma-auto-anchors-v1.0.2](https://github.com/mixadze1/unity-auto-anchors/tree/ma-auto-anchors-v1.0.2) — public documentation and multi-object workflow.
- [ma-auto-anchors-v1.0.1](https://github.com/mixadze1/unity-auto-anchors/tree/ma-auto-anchors-v1.0.1) — global editor shortcuts and settings integration.
- [ma-auto-anchors-v1.0.0](https://github.com/mixadze1/unity-auto-anchors/tree/ma-auto-anchors-v1.0.0) — initial package release.

## License

Distributed under the [MIT License](LICENSE).
