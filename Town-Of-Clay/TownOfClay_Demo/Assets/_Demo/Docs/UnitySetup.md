# Unity 2D setup

For Unity 6 with the **Universal 2D** template (URP 2D Renderer). Do these once in the editor; they can't be shipped as text files.

## Packages
Input System, TextMeshPro (import TMP Essentials when asked), 2D Tilemap Extras. URP, 2D Sprite and Tilemap come with the template.
Project Settings > Player > Active Input Handling: **Input System Package (New)** (or Both while you migrate).

## Sprite import (Sprites_16 and Sprites_32)
Make a preset and apply it to both art folders:
- Texture Type: Sprite (2D and UI), Sprite Mode: Single for objects, **Multiple** for animation strips and tile sheets
- Pixels Per Unit: **16** for Sprites_16, **32** for Sprites_32 (one tile = one unit either way)
- Filter Mode: **Point (no filter)**, Compression: **None**, Generate Mip Maps: off
- Slice strips by cell size (the frame size in the file name, e.g. `_2x3_6f` = 6 frames of 32x48 at 16 PPU); pivot **Bottom Center** for anything that stands on the floor

## Camera
- Add a **Pixel Perfect Camera** (URP) to the main camera: Assets PPU 16 (or 32), Reference Resolution **640x360**
  (the graduation cutscene and UI are authored at that size; zoom-2 shots are 320x180 drawn at 2x)
- Grid Snapping: Upscale Render Texture or Pixel Snapping, whichever looks cleaner with your character sprites

## Sorting (3/4 top-down)
- On the **Renderer 2D** asset (Settings/): Transparency Sort Mode = **Custom Axis**, Transparency Sort Axis = **(0, 1, 0)**
- Sorting Layers: Ground, Decor, World, Overhead, FX, UI
- `YSorter` sets per-object order where the axis sort isn't enough; tall objects use a **Sorting Group** with the pivot at their base

## Tilemaps
- One Grid per scene, cell size 1x1; Tilemaps for Ground, Paths, Walls, Decor (and Collision with a Tilemap Collider 2D + Composite Collider 2D)
- Autotile sheets use the corner bitmask from the pack README (NW=1, NE=2, SW=4, SE=8); turn them into RuleTiles in Tiles/

## Physics 2D
- Player: Rigidbody2D (Kinematic or Dynamic with gravity 0), CapsuleCollider2D at the feet
- Interactables: a trigger collider on an **Interactable** layer that `PlayerInteractor` checks in front of the player

## Lighting
- URP 2D: one **Global Light 2D** driven by `DayNightCycle`; street lamps get a **Point Light 2D** driven by `TimedLight`
