# Class breakdown: TownOfClay_Demo

**80 script files.** The quest system (`Quests/`, `UI/QuestTrackerUI`, `UI/QuestOfferPopup`, `Editor/QuestAssetBuilder`) is written and working; see `QuestSystem.md`. Everything else is a stub: `using` lines, namespace `ClayDemo.*`, and every class / struct / enum / interface
declared with an empty body. ScriptableObjects have `[CreateAssetMenu(menuName = "Clay Demo/...")]`, enums have their values,
and `IInteractable` are declared empty with their planned members as comments.
One class per file for anything that is a MonoBehaviour or ScriptableObject (file name = class name).
Packages: Input System, TextMeshPro, 2D Tilemap Extras (URP 2D from the template).

Namespace rules carried over from the main design: the time folder is `ClayDemo.Clock` (never `.Time`),
the inventory folder is `ClayDemo.Items` with class `PlayerInventory`, and inside `ClayDemo.*` files write `Image`
(not `UI.Image`) for uGUI.

### Core/  (`ClayDemo.Core`)
| File | Declares | What it does |
|---|---|---|
| `GameManager.cs` | `GameManager` | Lives in Boot.unity. Creates the managers, shows the title card, starts Day 1. |
| `ServiceLocator.cs` | `ServiceLocator` | Register<T>() / Get<T>() so systems find each other. |
| `SceneLoader.cs` | `SceneLoader`, `TransitionStyle` | Fades out, loads a scene, puts the player on a SpawnPoint id. |
| `SpawnPoint.cs` | `SpawnPoint` | Named spawn marker (from_street, from_plaza, post_office_door). |
| `Direction.cs` | `Direction`, `DirectionExtensions` | Up/Down/Left/Right + Vector2 conversion, shared by player and NPCs. |
| `GameIds.cs` | `ItemIds`, `NpcIds`, `LocationIds`, `UnlockIds` | String ids in one place so quest typos can't happen. |
| `UnlockRegistry.cs` | `UnlockRegistry` | Demo unlocks only: parcel_sending, glaze_celadon. |
| `DemoSession.cs` | `DemoSession` | The demo's frame: 3-day limit, no saving, rolls the Thanks-for-Playing card after Day 3. |

### Clock/  (`ClayDemo.Clock`)
| File | Declares | What it does |
|---|---|---|
| `GameClock.cs` | `GameClock` | In-game minutes; MinuteChanged / HourChanged / DayStarted; pauses in menus. |
| `Calendar.cs` | `Calendar`, `Season`, `Weekday` | Day number + weekday. The demo is fixed in Spring, days 1-3. |
| `DayNightCycle.cs` | `DayNightCycle` | Global Light2D colour by hour (one fixed spring gradient). |
| `SleepHandler.cs` | `SleepHandler` | Bed: end the day, kiln finishes, mail arrives. No save in the demo. |

### Player/  (`ClayDemo.Player`)
| File | Declares | What it does |
|---|---|---|
| `IInteractable.cs` | `IInteractable` | Prompt, CanInteract(), Interact(). Planned members are comments for now. |
| `PlayerController.cs` | `PlayerController` | Input -> Rigidbody2D movement, tracks facing. |
| `PlayerInteractor.cs` | `PlayerInteractor` | Finds the IInteractable in front of the player and calls it. |
| `PlayerAnimator.cs` | `PlayerAnimator` | Feeds speed/facing to the Animator for your 16x32 sprites. |

### Items/  (`ClayDemo.Items`)
| File | Declares | What it does |
|---|---|---|
| `ItemDefinition.cs` | `ItemDefinition`, `ItemCategory` | Id, name, 16 + 32 icons, category, stack size, base price. |
| `ItemDatabase.cs` | `ItemDatabase` | Id -> ItemDefinition. |
| `ItemStack.cs` | `ItemStack` | Item id + count (+ PotteryPiece for unique pieces). |
| `PlayerInventory.cs` | `PlayerInventory` | 12 slots (one row). Raises QuestEvents.ItemCollected. |
| `ItemPickup.cs` | `ItemPickup` | Item lying in the world (the three dropped letters). |

### Ceramics/  (`ClayDemo.Ceramics`)
| File | Declares | What it does |
|---|---|---|
| `PotteryPiece.cs` | `PotteryPiece`, `PieceStage` | One piece: form, glaze, quality. Demo stages: Greenware -> Glazed -> Fired (single firing). |
| `PotteryForm.cs` | `PotteryForm` | Cup (Lv1), Bowl (Lv2), Vase (Lv3). One clay body only. |
| `GlazeDefinition.cs` | `GlazeDefinition` | Pre-made glazes: Clear, Celadon, Iron Red. No mixing in the demo. |
| `WheelMinigame.cs` | `WheelMinigame`, `HitGrade` | Stages 0-5, PERFECT/GOOD/MISS timing. No combos in the demo. |
| `ThrowResult.cs` | `ThrowResult` | Score, grade counts, final quality. |
| `PotteryWheel.cs` | `PotteryWheel` | The starting studio wheel; opens the minigame. |
| `GlazeStation.cs` | `GlazeStation` | The glaze table: pick a glaze for a greenware piece. |
| `Kiln.cs` | `Kiln`, `KilnState` | Kiln Lv1: load, fire overnight, unload. Raises ItemCrafted. |

### Skills/  (`ClayDemo.Skills`)
| File | Declares | What it does |
|---|---|---|
| `CeramicsSkill.cs` | `CeramicsSkill` | The only skill in the demo: XP and levels 1-3 (cups, bowls, vases). No paths. |

### Economy/  (`ClayDemo.Economy`)
| File | Declares | What it does |
|---|---|---|
| `Wallet.cs` | `Wallet` | Gold, TrySpend(), Changed event. |
| `ShippingOutbox.cs` | `ShippingOutbox` | Drop finished pieces in; paid out overnight (fx_coin_pop next morning). |

### Mail/  (`ClayDemo.Mail`)
| File | Declares | What it does |
|---|---|---|
| `LetterDefinition.cs` | `LetterDefinition` | Sender, body, attachments, optional quest to start (Marigold's letter). |
| `MailManager.cs` | `MailManager` | Delivers the scripted demo letters each morning. |
| `PlayerMailbox.cs` | `PlayerMailbox` | Flag up when mail waits; opens LetterScreen. |
| `Parcel.cs` | `Parcel`, `PostageType` | Contents + recipient. Standard postage only. |
| `PostOfficeCounter.cs` | `PostOfficeCounter` | Hand in letters, pack and send a parcel (needs parcel_sending). |

### Quests/  (`ClayDemo.Quests`) ✅ written
| File | Declares | What it does |
|---|---|---|
| `QuestEvents.cs` ✅ | `QuestEvents`, `QuestEvent`, `QuestEventType` | Static event bus: ItemCollected, ItemDelivered, ItemCrafted, TalkedTo. |
| `QuestStep.cs` ✅ | `QuestStep`, `StepProgress` | Base step + saved progress. |
| `QuestDefinition.cs` ✅ | `QuestDefinition`, `QuestSource` | Id, title, giver, steps, rewards. |
| `QuestManager.cs` ✅ | `QuestManager`, `QuestStatus`, `QuestMarker` | Active/completed quests, listens to QuestEvents, markers. |
| `QuestGiver.cs` ✅ | `QuestGiver` | Shows ! / ? over an NPC or the notice board. |
| `IQuestServices.cs` | `IQuestServices` | What quests need from the game (items, gold, unlocks). |
| `DebugQuestServices.cs` | `DebugQuestServices` | ✅ Test stand-in for inventory, wallet and unlocks. |
| `QuestEventTester.cs` | `QuestEventTester` | ✅ Fire quest events from the Inspector. |

### Quests/Steps/  (`ClayDemo.Quests.Steps`)
| File | Declares | What it does |
|---|---|---|
| `CollectItemStep.cs` ✅ | `CollectItemStep` | Have N of an item. |
| `DeliverItemStep.cs` ✅ | `DeliverItemStep` | Hand N of an item to a target. |
| `CraftItemStep.cs` ✅ | `CraftItemStep` | Make N of an item (fired cup_celadon, package_small). |
| `TalkToStep.cs` ✅ | `TalkToStep` | Talk to an NPC or read an object. |

### Quests/Rewards/  (`ClayDemo.Quests.Rewards`)
| File | Declares | What it does |
|---|---|---|
| `QuestReward.cs` ✅ | `QuestReward` | Base reward. |
| `GoldReward.cs` ✅ | `GoldReward` | Adds gold. |
| `ItemReward.cs` ✅ | `ItemReward` | Gives an item. |
| `UnlockReward.cs` ✅ | `UnlockReward` | Sets an unlock id. |

### World/  (`ClayDemo.World`)
| File | Declares | What it does |
|---|---|---|
| `Door.cs` | `Door` | Plays the door strip, swaps the building tiles, closes after ~1 s. |
| `TimedLight.cs` | `TimedLight` | Street lamps: _turn_on at dusk, loop _glow, _off by day. |
| `AmbientAnimator.cs` | `AmbientAnimator` | Loops a strip from a random start frame (grass, butterflies, fountain). |
| `DropOffPoint.cs` | `DropOffPoint` | Red post box etc.; raises ItemDelivered. |
| `YSorter.cs` | `YSorter` | Sorting order by Y. |

### Dialog/  (`ClayDemo.Dialog`)
| File | Declares | What it does |
|---|---|---|
| `DialogDefinition.cs` | `DialogDefinition` | Lines for one conversation. |
| `DialogLine.cs` | `DialogLine`, `PortraitSide` | Speaker, expression, portrait side, text. |
| `DialogRunner.cs` | `DialogRunner` | Plays a dialog into the DialogBox. |

### NPCs/  (`ClayDemo.NPCs`)
| File | Declares | What it does |
|---|---|---|
| `NpcDefinition.cs` | `NpcDefinition` | Id, name, portrait. No schedules or gifts in the demo. |
| `NpcInteractable.cs` | `NpcInteractable` | Stands in place; talk -> dialog / quest offer / turn-in. |

### UI/  (`ClayDemo.UI`)
| File | Declares | What it does |
|---|---|---|
| `UIScreen.cs` | `UIScreen` | Base screen: Open(), Close(), pauses the clock. |
| `ScreenManager.cs` | `ScreenManager` | One screen at a time; back/escape. |
| `ScreenFader.cs` | `ScreenFader` | Fade to/from black. |
| `TitleCards.cs` | `TitleCards`, `CardType` | Title card, 'Chapter 1: Moving Day' card, and the end card. |
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

### Editor/  (`ClayDemo.EditorTools`)
| File | Declares | What it does |
|---|---|---|
| `IdValidator.cs` | `IdValidator` | Menu item: checks quest/step ids against the ItemDatabase and NPCs. |
| `SpriteSetChecker.cs` | `SpriteSetChecker` | EditorWindow: lists sprites in Sprites_16 with no match in Sprites_32 (or the other way round). |

Editor scripts live in a folder named `Editor`, so Unity leaves them out of builds; `UnityEditor` is only used there.

## Build order
1. Core (`GameManager`, `ServiceLocator`, `SceneLoader`, `Direction`) + Clock (`GameClock`, `Calendar`)
2. Player + `IInteractable`, then World (`Door`, `YSorter`)
3. Items (`ItemDefinition`, `PlayerInventory`) + Economy `Wallet`
4. Dialog + UI basics (`DialogBox`, `HUD`, `ScreenManager`, `ScreenFader`, `TitleCards`)
5. Ceramics (`WheelMinigame` first, then `GlazeStation`, `Kiln`) + `CeramicsSkill`
6. Mail (`MailManager`, `PostOfficeCounter`) and Quests -> test Lost Letters, then Teacups by Post
7. `DemoSession` last: the 3-day limit and the end card
