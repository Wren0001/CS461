# Town of Clay: First Build (~20% scope)

The **first real build**: one full spring season with save/load, three skills, the graduation intro, and 4 quests
that take the player from the studio to the quarry and back. It's the foundation the rest of the game gets
added onto, but it is a **separate project folder**: no shared folders, scripts or assets with the full game
project, its own namespace (`ClayFirstBuild.*`) and its own root (`Assets/_FirstBuild/`).

| Doc | What's in it |
|---|---|
| `Docs/Scope.md` | What's in, what's cut, the numbers, the season's flow |
| `Docs/QuestCatalog.md` | The 4 quests with first-build changes |
| `Docs/ClassBreakdown.md` | Every script file in this build |
| `Docs/FolderStructure.md` | The folder tree |
| `Docs/QuestSystem.md` | The working quest system: setup, test play-through, how to hook up the other systems |
| `Docs/UnitySetup.md` | Unity 2D settings: sprite import, Pixel Perfect Camera, Y-sorting, tilemaps, lights |
| `Tools/asset_manifest.txt` | Which existing pack files this build uses |

## Getting started
1. Create a new Unity 6 project from the **Universal 2D** template and copy this folder's `Assets/` and `Tools/` into it.
2. Install **Input System, TextMeshPro, 2D Tilemap Extras**.
3. **The 16x16 art is already in** `Assets/_FirstBuild/Art/Sprites_16/` (806 files, copied by name from the 16x16 pack; nothing redrawn).
   For the 32x32 set, run `python3 Tools/copy_assets.py --pack32 <path>/assets_32x32` (add `--dry-run` first to preview).
   Pottery is split by clay body (`Items/Pottery/Stoneware`, `Tintable`, `Terracotta`) because every body uses the same file names.
4. The **quest system is written** (setup and a play-through in `Docs/QuestSystem.md`); every other script is a **stub**. Build order is at the end of `Docs/ClassBreakdown.md`.

The same docs are also inside `Assets/_FirstBuild/Docs/`, like the full project's `_Project/Docs/`.
