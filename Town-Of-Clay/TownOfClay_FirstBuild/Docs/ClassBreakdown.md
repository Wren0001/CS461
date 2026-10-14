# Class breakdown: TownOfClay_FirstBuild

**140 script files.** The quest system (`Quests/`, `UI/QuestTrackerUI`, `UI/QuestOfferPopup`, `Editor/QuestAssetBuilder`) is written and working; see `QuestSystem.md`. Everything else is a stub: `using` lines, namespace `ClayFirstBuild.*`, and every class / struct / enum / interface
declared with an empty body. ScriptableObjects have `[CreateAssetMenu(menuName = "Clay First Build/...")]`, enums have their values,
and `IInteractable` / `ISaveable` are declared empty with their planned members as comments.
One class per file for anything that is a MonoBehaviour or ScriptableObject (file name = class name).
Packages: Input System, TextMeshPro, 2D Tilemap Extras (URP 2D from the template).

Namespace rules carried over from the main design: the time folder is `ClayFirstBuild.Clock` (never `.Time`),
the inventory folder is `ClayFirstBuild.Items` with class `PlayerInventory`, and inside `ClayFirstBuild.*` files write `Image`
(not `UI.Image`) for uGUI.

### Core/  (`ClayFirstBuild.Core`)
| File | Declares | What it does |
|---|---|---|
| `GameManager.cs` | `GameManager` | Lives in Boot.unity. New Game -> graduation intro; Continue -> loads the save. |
| `ServiceLocator.cs` | `ServiceLocator` | Register<T>() / Get<T>() so systems find each other. |
| `SceneLoader.cs` | `SceneLoader`, `TransitionStyle` | Fades out, loads a scene, puts the player on a SpawnPoint id. |
| `SpawnPoint.cs` | `SpawnPoint` | Named spawn marker (from_street, from_plaza, post_office_door). |
| `Direction.cs` | `Direction`, `DirectionExtensions` | Up/Down/Left/Right + Vector2 conversion, shared by player and NPCs. |
| `GameIds.cs` | `ItemIds`, `NpcIds`, `LocationIds`, `UnlockIds` | String ids in one place so quest typos can't happen. |
| `UnlockRegistry.cs` | `UnlockRegistry` | Unlocks for the 4 quests: parcel_sending, lift-free quarry access, repair_wheel_drive, glaze recipes. |
| `SaveSystem.cs` | `SaveSystem`, `SaveFile` | JSON save/load; asks every ISaveable for its data. One slot + autosave on sleep. |
| `ISaveable.cs` | `ISaveable` | SaveKey, CaptureState(), RestoreState(). Planned members are comments for now. |
| `GameSettings.cs` | `GameSettings`, `ArtResolution` | 16 vs 32 art set, volumes, text speed. |
| `SpriteSetResolver.cs` | `SpriteSetResolver` | Swaps 16 vs 32 sprites from GameSettings. |

### Clock/  (`ClayFirstBuild.Clock`)
| File | Declares | What it does |
|---|---|---|
| `GameClock.cs` | `GameClock` | In-game minutes; MinuteChanged / HourChanged / DayStarted; pauses in menus. |
| `Calendar.cs` | `Calendar`, `Season`, `Weekday` | Day / season / year math (28-day seasons). Content ships for Spring only. |
| `DayNightCycle.cs` | `DayNightCycle` | Global Light2D by hour; darker tint in storms. |
| `SleepHandler.cs` | `SleepHandler` | Bed, 2 AM pass-out, autosave and end-of-day summary. |
| `SeasonManager.cs` | `SeasonManager` | Season framework in place; the first build ships Spring content only (28 days). |
| `WeatherSystem.cs` | `WeatherSystem`, `WeatherType` | Sunny / Rain / Storm roll, rain overlay, puddles. |

### Player/  (`ClayFirstBuild.Player`)
| File | Declares | What it does |
|---|---|---|
| `IInteractable.cs` | `IInteractable` | Prompt, CanInteract(), Interact(). Planned members are comments for now. |
| `PlayerController.cs` | `PlayerController` | Input -> Rigidbody2D movement, tracks facing. |
| `PlayerInteractor.cs` | `PlayerInteractor` | Finds the IInteractable in front of the player and calls it. |
| `PlayerAnimator.cs` | `PlayerAnimator` | Feeds speed/facing to the Animator for your 16x32 sprites. |
| `PlayerStamina.cs` | `PlayerStamina` | Current/max stamina; throws, swings and repairs drain it. |
| `ToolUser.cs` | `ToolUser`, `ToolType` | Uses the held tool (pickaxe, wrench) on the facing tile. |

### Items/  (`ClayFirstBuild.Items`)
| File | Declares | What it does |
|---|---|---|
| `ItemDefinition.cs` | `ItemDefinition`, `ItemCategory` | Id, name, 16 + 32 icons, category, stack size, base price. |
| `ItemDatabase.cs` | `ItemDatabase` | Id -> ItemDefinition. |
| `ItemStack.cs` | `ItemStack` | Item id + count (+ PotteryPiece for unique pieces). |
| `PlayerInventory.cs` | `PlayerInventory` | 24 slots. Raises QuestEvents.ItemCollected. |
| `ItemPickup.cs` | `ItemPickup` | Item lying in the world (dropped letters, raw clay). |
| `Hotbar.cs` | `Hotbar` | Selected slot, scroll / number keys. |

### Ceramics/  (`ClayFirstBuild.Ceramics`)
| File | Declares | What it does |
|---|---|---|
| `PotteryPiece.cs` | `PotteryPiece`, `PieceStage` | One piece: form, clay, glaze, quality. Stages Greenware -> Bisque -> Glazed -> Fired. |
| `PotteryForm.cs` | `PotteryForm` | Cup, Bowl, Vase, Planter, Mug (Ceramics Lv1-4; Lv5 is the path choice). |
| `GlazeDefinition.cs` | `GlazeDefinition` | Recipe + result colour. Clear, Celadon, Iron Red, Shino; Cobalt/Copper are later. |
| `WheelMinigame.cs` | `WheelMinigame`, `HitGrade` | Stages 0-5, PERFECT/GOOD/MISS timing. No combos in the demo. |
| `ThrowResult.cs` | `ThrowResult` | Score, grade counts, final quality. |
| `PotteryWheel.cs` | `PotteryWheel` | The starting studio wheel; opens the minigame. |
| `GlazeStation.cs` | `GlazeStation` | The glaze table: pick a glaze for a greenware piece. |
| `Kiln.cs` | `Kiln`, `KilnState` | Kiln Lv1 (+ upgrade to Lv2). Overnight firing; breaks on Day 8 (quest 15). |
| `ClayBody.cs` | `ClayBody` | Stoneware (bought) + Wild Red (quest 13). |
| `GlazeMixer.cs` | `GlazeMixer` | Simple recipe mixing: 2 ingredients -> known glaze. Unlocked recipes only. |

### Skills/  (`ClayFirstBuild.Skills`)
| File | Declares | What it does |
|---|---|---|
| `SkillType.cs` | `SkillType` | Ceramics, Mining, Tinkering. |
| `SkillDefinition.cs` | `SkillDefinition` | Name, icon, XP curve, max level, per-level unlocks, the two level-5 paths. |
| `SkillPerk.cs` | `SkillPerk` | Level + unlock id. |
| `SkillProfession.cs` | `SkillProfession` | Level-5 path only in this build (Wheelwright, Glaze Chemist, Prospector, Geologist, Mechanic, Inventor). |
| `ProfessionModifier.cs` | `ProfessionModifier` | Key + value, e.g. wheel_window_mult = 1.15. |
| `SkillSystem.cs` | `SkillSystem` | XP per skill; cap Lv5; ChoiceNeeded at 5; GetModifier(key). |

### Economy/  (`ClayFirstBuild.Economy`)
| File | Declares | What it does |
|---|---|---|
| `Wallet.cs` | `Wallet` | Gold, TrySpend(), Changed event. |
| `ShippingOutbox.cs` | `ShippingOutbox` | Drop finished pieces in; paid out overnight (fx_coin_pop next morning). |
| `ShopDefinition.cs` | `ShopDefinition`, `ShopStockEntry` | Stock, prices, opening hours. |
| `Shop.cs` | `Shop` | Craft Superstore counter: clay, glaze materials, repair parts. |
| `PriceCalculator.cs` | `PriceCalculator` | Base price x quality (x profession modifiers later). |

### Mail/  (`ClayFirstBuild.Mail`)
| File | Declares | What it does |
|---|---|---|
| `LetterDefinition.cs` | `LetterDefinition` | Sender, body, attachments, optional quest to start (Marigold's letter). |
| `MailManager.cs` | `MailManager` | Delivers the scripted demo letters each morning. |
| `PlayerMailbox.cs` | `PlayerMailbox` | Flag up when mail waits; opens LetterScreen. |
| `Parcel.cs` | `Parcel`, `PostageType` | Contents + recipient. Standard postage only. |
| `PostOfficeCounter.cs` | `PostOfficeCounter` | Hand in letters, pack and send a parcel (needs parcel_sending). |

### Quests/  (`ClayFirstBuild.Quests`) ✅ written
| File | Declares | What it does |
|---|---|---|
| `QuestEvents.cs` ✅ | `QuestEvents`, `QuestEvent`, `QuestEventType` | Static event bus: ItemCollected, ItemDelivered, ItemCrafted, TalkedTo. |
| `QuestStep.cs` ✅ | `QuestStep`, `StepProgress` | Base step + saved progress. |
| `QuestDefinition.cs` ✅ | `QuestDefinition`, `QuestSource` | Id, title, giver, steps, rewards. |
| `QuestManager.cs` ✅ | `QuestManager`, `QuestStatus`, `QuestMarker` | Active/completed quests, listens to QuestEvents, markers. |
| `QuestGiver.cs` ✅ | `QuestGiver` | Shows ! / ? over an NPC or the notice board. |
| `IQuestServices.cs` | `IQuestServices` | What quests need from the game (items, gold, unlocks, current day, friendship, status effects). |
| `DebugQuestServices.cs` | `DebugQuestServices` | ✅ Test stand-in for inventory, wallet and unlocks. |
| `QuestEventTester.cs` | `QuestEventTester` | ✅ Fire quest events from the Inspector. |

### Quests/Steps/  (`ClayFirstBuild.Quests.Steps`)
| File | Declares | What it does |
|---|---|---|
| `CollectItemStep.cs` ✅ | `CollectItemStep` | Have N of an item. |
| `DeliverItemStep.cs` ✅ | `DeliverItemStep` | Hand N of an item to a target. |
| `CraftItemStep.cs` ✅ | `CraftItemStep` | Make N of an item (fired cup_celadon, package_small). |
| `TalkToStep.cs` ✅ | `TalkToStep` | Talk to an NPC or read an object. |
| `VisitLocationStep.cs` ✅ | `VisitLocationStep` | Enter a location (quarry). |
| `CounterStep.cs` ✅ | `CounterStep` | Count events (raw_red_clay dug). |

### Quests/Rewards/  (`ClayFirstBuild.Quests.Rewards`)
| File | Declares | What it does |
|---|---|---|
| `QuestReward.cs` ✅ | `QuestReward` | Base reward. |
| `GoldReward.cs` ✅ | `GoldReward` | Adds gold. |
| `ItemReward.cs` ✅ | `ItemReward` | Gives an item. |
| `UnlockReward.cs` ✅ | `UnlockReward` | Sets an unlock id. |
| `FriendshipReward.cs` ✅ | `FriendshipReward` | Adds hearts. |
| `StatusEffectReward.cs` ✅ | `StatusEffectReward` | Applies a status (Cozy). |

### World/  (`ClayFirstBuild.World`)
| File | Declares | What it does |
|---|---|---|
| `Door.cs` | `Door` | Plays the door strip, swaps the building tiles, closes after ~1 s. |
| `TimedLight.cs` | `TimedLight` | Street lamps: _turn_on at dusk, loop _glow, _off by day. |
| `AmbientAnimator.cs` | `AmbientAnimator` | Loops a strip from a random start frame (grass, butterflies, fountain). |
| `DropOffPoint.cs` | `DropOffPoint` | Red post box etc.; raises ItemDelivered. |
| `YSorter.cs` | `YSorter` | Sorting order by Y. |
| `LocationTrigger.cs` | `LocationTrigger` | Zone that raises LocationEntered. |
| `FacedObject.cs` | `FacedObject` | Up/down/left/right sprites for studio furniture. |

### Dialog/  (`ClayFirstBuild.Dialog`)
| File | Declares | What it does |
|---|---|---|
| `DialogDefinition.cs` | `DialogDefinition` | Lines for one conversation. |
| `DialogLine.cs` | `DialogLine`, `PortraitSide` | Speaker, expression, portrait side, text. |
| `DialogRunner.cs` | `DialogRunner` | Plays a dialog into the DialogBox. |

### NPCs/  (`ClayFirstBuild.NPCs`)
| File | Declares | What it does |
|---|---|---|
| `NpcDefinition.cs` | `NpcDefinition` | Id, name, portrait, loved/liked gifts, schedule. |
| `NpcInteractable.cs` | `NpcInteractable` | Stands in place; talk -> dialog / quest offer / turn-in. |
| `NpcSchedule.cs` | `NpcSchedule`, `ScheduleEntry` | Two or three spots per day (morning / afternoon / evening). |
| `NpcController.cs` | `NpcController` | Walks between schedule spots on fixed waypoints (no A*). |

### UI/  (`ClayFirstBuild.UI`)
| File | Declares | What it does |
|---|---|---|
| `UIScreen.cs` | `UIScreen` | Base screen: Open(), Close(), pauses the clock. |
| `ScreenManager.cs` | `ScreenManager` | One screen at a time; back/escape. |
| `ScreenFader.cs` | `ScreenFader` | Fade to/from black. |
| `HUD.cs` | `HUD` | Clock, day, gold, quest tracker slot. |
| `DialogBox.cs` | `DialogBox` | Bottom box, 128x128 portrait, typewriter text. |
| `InventoryScreen.cs` | `InventoryScreen` | Slots + a Ceramics XP bar. |
| `ItemSlotUI.cs` | `ItemSlotUI` | Icon, count, tooltip. (No drag-and-drop in the demo.) |
| `LetterScreen.cs` | `LetterScreen` | Reads a letter. |
| `PostOfficeScreen.cs` | `PostOfficeScreen` | Pack a parcel and send it (Standard only). |
| `WheelMinigameUI.cs` | `WheelMinigameUI` | Timing bar + PERFECT/GOOD/MISS words. |
| `QuestTrackerUI.cs` ✅ | `QuestTrackerUI` | Active quest + step text. |
| `QuestOfferPopup.cs` ✅ | `QuestOfferPopup` | Accept/decline (notice board). |
| `NotificationBubble.cs` | `NotificationBubble` | Pop-in/float bubbles (mail, level up). |
| `CutsceneUI.cs` | `CutsceneUI` | Letterbox + skip ring. |
| `CutsceneCards.cs` | `CutsceneCards`, `CutsceneCardType` | Title, caption, name card, flash, polaroid, iris, chapter card. |
| `SkillsTab.cs` | `SkillsTab` | Three skill bars + one path slot each. |
| `ProfessionChoicePopup.cs` | `ProfessionChoicePopup` | 'Choose your path' at level 5. |
| `ShopScreen.cs` | `ShopScreen` | Buy / sell. |
| `QuestLogScreen.cs` | `QuestLogScreen` | Active + completed quests. |
| `TinkeringScreen.cs` | `TinkeringScreen` | Repair steps for kiln_err. |
| `BusRouteScreen.cs` | `BusRouteScreen` | Pick a destination. |

### Mining/  (`ClayFirstBuild.Mining`)
| File | Declares | What it does |
|---|---|---|
| `MineableNode.cs` | `MineableNode` | HP, hit feedback, drops, Mining XP. |
| `ClayNode.cs` | `ClayNode` | Red clay nodes in the quarry pit (fx_clay_chunks). |
| `DropTable.cs` | `DropTable`, `DropEntry` | Weighted drops. |
| `QuarryController.cs` | `QuarryController` | Respawns nodes daily, raises LocationEntered(quarry). |
| `ClayProcessingStation.cs` | `ClayProcessingStation`, `ProcessStep` | Slaking tub -> sieve -> wild clay. |

### Tinkering/  (`ClayFirstBuild.Tinkering`)
| File | Declares | What it does |
|---|---|---|
| `Machine.cs` | `Machine`, `MachineCondition` | Sits on the kiln/wheel: condition, BrokeDown event, repair marker. |
| `BreakdownDefinition.cs` | `BreakdownDefinition` | kiln_err only: symptom, parts, difficulty. |
| `ScriptedBreakdown.cs` | `ScriptedBreakdown` | Breaks the kiln on a fixed day (Day 8) instead of random breakdowns. |
| `RepairJob.cs` | `RepairJob` | Parts check -> success -> Tinkering XP + ItemCrafted(kiln_repaired). |

### Community/  (`ClayFirstBuild.Community`)
| File | Declares | What it does |
|---|---|---|
| `FriendshipSystem.cs` | `FriendshipSystem` | Hearts per NPC (0-5 in this build), talked-today flag. |

### Travel/  (`ClayFirstBuild.Travel`)
| File | Declares | What it does |
|---|---|---|
| `SceneDoor.cs` | `SceneDoor` | Door into another scene + spawn point. |
| `BusRoute.cs` | `BusRoute`, `BusStopEntry` | One route: Main Street <-> Clay Quarry. |
| `BusStop.cs` | `BusStop` | Opens the route screen; checks for a ticket. |

### StatusEffects/  (`ClayFirstBuild.StatusEffects`)
| File | Declares | What it does |
|---|---|---|
| `StatusEffectDefinition.cs` | `StatusEffectDefinition` | Cozy + Calm are assets, not classes. |
| `StatModifier.cs` | `StatModifier`, `StatType` | e.g. ThrowWindow +10%. |
| `ActiveStatus.cs` | `ActiveStatus` | Effect + time left. |
| `StatusEffectManager.cs` | `StatusEffectManager` | Apply/remove, ticks with the clock. |

### Cutscenes/  (`ClayFirstBuild.Cutscenes`)
| File | Declares | What it does |
|---|---|---|
| `CutsceneDefinition.cs` | `CutsceneDefinition` | Cutscene as data; asset Data/Cutscenes/graduation_intro (S1-S11). |
| `CutsceneShot.cs` | `CutsceneShot`, `ShotAction` | One shot: start, zoom, action, actor, asset, duration. |
| `CutscenePlayer.cs` | `CutscenePlayer` | Runs shots; freezes player/clock; hold SPACE to skip. |
| `CutsceneActor.cs` | `CutsceneActor` | graduate / dean: plays strips, walks to marks. |
| `CameraDirector.cs` | `CameraDirector` | Zoom 1 (640x360) vs zoom 2 (320x180 at 2x). |
| `CrowdFlashSpawner.cs` | `CrowdFlashSpawner` | 2-3 camera flashes in S2. |
| `ConfettiOverlay.cs` | `ConfettiOverlay` | Confetti over the cap toss (S9). |
| `DiplomaNameWriter.cs` | `DiplomaNameWriter` | Player's name on cu_diploma (S6) and in the dean's S4 line. |
| `GraduationCutscene.cs` | `GraduationCutscene` | Scene script for Graduation.unity. |
| `IntroSequence.cs` | `IntroSequence` | Graduation -> chapter card -> bus arrives -> control; gives keepsakes. |

### Editor/  (`ClayFirstBuild.EditorTools`)
| File | Declares | What it does |
|---|---|---|
| `IdValidator.cs` | `IdValidator` | Menu item: checks quest/step ids against the ItemDatabase and NPCs. |
| `SpriteSetChecker.cs` | `SpriteSetChecker` | EditorWindow: lists sprites in Sprites_16 with no match in Sprites_32 (or the other way round). |

## Build order
1. Core (`GameManager`, `ServiceLocator`, `SaveSystem`, `ISaveable`, `Direction`, `UnlockRegistry`) + Clock
2. Player (+ `PlayerStamina`, `ToolUser`) and World
3. Items + Economy (`Wallet`, `Shop`)
4. Dialog + UI basics, then Quests -> test Lost Letters
5. Ceramics -> `SkillSystem` + `SkillProfession` (Lv5 paths)
6. Mail -> Teacups by Post
7. Travel (`BusStop`) + Mining -> First Trip to the Quarry
8. Tinkering -> The Kiln That Says ERR
9. Cutscenes last (graduation intro as the New Game start), then Weather, StatusEffects, Friendship, NPC schedules
