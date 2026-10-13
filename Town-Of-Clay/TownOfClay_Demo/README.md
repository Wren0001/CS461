# Town of Clay: Demo (~10% scope)

A **3-in-game-day vertical slice** of Town of Clay. It shows the core loop once, start to finish:
throw on the wheel, glaze, fire the kiln, and use what you made to help someone in town.

This folder is its own Unity project. It shares no folders, scripts or assets with the full game project,
uses its own namespace (`ClayDemo.*`), and its own root (`Assets/_Demo/`), so it can never collide with it.

| Doc | What's in it |
|---|---|
| `Docs/Scope.md` | What's in, what's cut, the numbers, the 3-day flow |
| `Docs/QuestCatalog.md` | The 2 demo quests, cut down to fit 3 days |
| `Docs/ClassBreakdown.md` | Every script file in this build |
| `Docs/FolderStructure.md` | The folder tree |
| `Docs/QuestSystem.md` | The working quest system: setup, test play-through, how to hook up the other systems |
| `Docs/UnitySetup.md` | Unity 2D settings: sprite import, Pixel Perfect Camera, Y-sorting, tilemaps, lights |
| `Tools/asset_manifest.txt` | Which existing pack files this build uses |

## Getting started
1. Create a new Unity 6 project from the **Universal 2D** template and copy this folder's `Assets/` and `Tools/` into it.
2. Install **Input System, TextMeshPro, 2D Tilemap Extras** (URP comes with the 2D URP template).
3. Pull the art from the packs you already have (it copies by name, nothing is redrawn):
   `python3 Tools/copy_assets.py --pack16 <path>/assets_16x16 --pack32 <path>/assets_32x32 --dry-run`
   then run it again without `--dry-run`. Any pattern that matches nothing is listed so you can fix the name.
4. The **quest system is written** (setup and a play-through in `Docs/QuestSystem.md`); every other script is a **stub** (usings, namespace, empty declarations). Build order is at the end of `Docs/ClassBreakdown.md`.

The same docs are also inside `Assets/_Demo/Docs/`, like the full project's `_Project/Docs/`.
