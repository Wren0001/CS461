# Quest system (first build)

The quest system is **written and working** in this build. Everything else in `Scripts/` is still a stub.
It is a reduced version of the full game's quest system: only the parts the 4 first-build quests needs.

## What's in it
| File | What it does |
|---|---|
| `Quests/QuestEvents.cs` | The event bus. Other systems call `QuestEvents.ItemCollected("letter")`, `ItemDelivered(...)`, `ItemCrafted(...)`, `TalkedTo(...)`, `LocationEntered(...)`, `CounterRaised(...)`. |
| `Quests/IQuestServices.cs` | What quests need from the game: count/give items, add gold, unlocks, current day, friendship, status effects. |
| `Quests/QuestDefinition.cs` | A quest asset: id, title, giver, where it's offered, steps in order, rewards, plus `availableFromDay` and prerequisite quests. |
| `Quests/QuestStep.cs` | Base step + `StepProgress`. |
| `Quests/Steps/` | Collect Item, Deliver Item, Craft Item, Talk To, Visit Location, Counter |
| `Quests/Rewards/` | Gold, Item, Unlock, Friendship, Status Effect |
| `Quests/QuestManager.cs` | Tracks every quest (Locked, Available, Active, ReadyToTurnIn, Complete), advances steps, grants rewards, gives the '!'/'?' marker; saves and loads with `CaptureState` / `RestoreState`. |
| `Quests/QuestGiver.cs` | On an NPC or object: shows the marker; `Interact()` handles talk, offer and turn-in. |
| `Quests/DebugQuestServices.cs` | Test stand-in for inventory, wallet and unlocks, the day counter (right-click > Next day), friendship and status effects. Swap for a real `GameServices` later. |
| `Quests/QuestEventTester.cs` | Fire events, start or turn in quests from the Inspector (right-click the component). |
| `UI/QuestTrackerUI.cs` | HUD tracker (TextMeshPro): quest title + current step with count. |
| `UI/QuestOfferPopup.cs` | Accept/decline panel for `ui_quest_offer`. |
| `Editor/QuestAssetBuilder.cs` | Menu **Tools > Clay First Build > Create Quest Assets** builds this build's 4 quests in `Data/Quests`. |

Steps run in order. Ids can end in `*` for a prefix match (e.g. `mailbox_*`).
A quest with **requiresTurnIn** waits for you to talk to its giver ('?' marker); otherwise it finishes by itself.
Quests started by a letter or an object have **offerOnStart** off and are started from code with `QuestManager.Instance.StartQuest(id)`.

## Set it up (5 minutes)
1. Install TextMeshPro (Window > TextMeshPro > Import TMP Essential Resources).
2. **Tools > Clay First Build > Create Quest Assets.**
3. In `Boot.unity` add an empty GameObject **Quests** with `QuestManager`, `DebugQuestServices` and `QuestEventTester`.
   On `QuestManager`, right-click the component header > **Fill list with every quest in the project**.
4. Optional UI: a Canvas with a `QuestTrackerUI` (root panel + two TMP texts) and a `QuestOfferPopup`
   (on an always-active object, with a child panel, three TMP texts and Accept/Decline buttons).
5. Optional markers: give the Postmaster and the notice board a `QuestGiver` (ids `postmaster`, `notice_board`) with a child
   SpriteRenderer for the marker, and the `quest_marker_new` / `quest_marker_turn_in` sprites.

## Play through it with the tester (no other systems needed)
Enter Play mode, select **Quests**, set the tester's fields, then right-click the `QuestEventTester` header > **Raise event**:
| # | type | id | targetId | amount |
|---|---|---|---|---|
| 1 | TalkedTo | notice_board | | | 
| | *(Lost Letters is offered at the notice board; with no popup, a `QuestGiver` accepts it straight away. With only the tester: right-click > **Start quest** with questId `lost_letters`, then raise step 1.)* | | | |
| 2 | ItemCollected | letter | | 3 |
| 3 | ItemDelivered | letter | post_office | 3 |
| 4 | TalkedTo | postmaster | | *(turn in: right-click > **Turn in quest**)* |
| 5 | *Start quest* `teacups_by_post` (Marigold's letter) | | | |
| 6 | ItemCrafted | cup_celadon | | 4 |
| 7 | ItemCrafted | package_small | | 1 |
| 8 | ItemDelivered | package_small | marigold | 1 |
| 9 | *Right-click `DebugQuestServices` > **Next day** until day 5:* First Trip to the Quarry becomes available | | | |
| 10 | *Start quest* `first_quarry_trip`, then ItemCollected `bus_ticket_return`, LocationEntered `quarry`, ItemCollected `pickaxe`, ItemCollected `raw_red_clay` x5, ItemCrafted `wild_clay`, then **Turn in** | | | |
| 11 | *Start quest* `kiln_err`, then TalkedTo `kiln`, ItemCollected `thermocouple_probe`, ItemCollected `fuse`, ItemCrafted `kiln_repaired` | | | |
Right-click > **Print quest state** at any point. Rewards show up on `DebugQuestServices` in the Inspector.

## Hooking up the real systems later
| System | Call |
|---|---|
| `PlayerInventory.Add` | `QuestEvents.ItemCollected(itemId, count)` |
| `PostOfficeCounter`, `DropOffPoint` | remove the items, then `QuestEvents.ItemDelivered(itemId, targetId, count)` |
| `Kiln` unload, parcel packing, `RepairJob`, `ClayProcessingStation` | `QuestEvents.ItemCrafted(itemId, count)` |
| `NpcInteractable`, notice board, `Kiln` | `GetComponent<QuestGiver>()?.Interact()`, or `QuestEvents.TalkedTo(id)` for objects without a giver |
| `MailManager` (Marigold's letter) | `QuestManager.Instance.StartQuest("teacups_by_post")` |
| `LocationTrigger`, scene loads | `QuestEvents.LocationEntered("quarry")` |
| `ScriptedBreakdown` (day 8) | `QuestManager.Instance.StartQuest("kiln_err")` |
| `GameClock.DayStarted` | `QuestManager.Instance.RefreshAvailability()` |
| `SaveSystem` | store `CaptureState()` under `SaveKey`, pass it back to `RestoreState()` |
| `GameServices` (Core/) | implement `IQuestServices` with `PlayerInventory`, `Wallet`, `UnlockRegistry`, `GameClock`, `FriendshipSystem`, `StatusEffectManager`; assign it as the manager's services provider |
