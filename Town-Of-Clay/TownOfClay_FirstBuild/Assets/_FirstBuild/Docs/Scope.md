# First build scope (~20%)

## The pitch
*"Your first spring."* Graduate, move to town, open the studio, take the bus out for wild clay, keep the kiln
running. One full 28-day season you can save, quit and continue.

## The numbers
| Part of the game | Full game | First build | Share |
|---|---|---|---|
| Quests | 19 | 4 (1, 2, 13, 15) | 21% |
| Skills | 5, Lv1-10 | 3 (Ceramics, Mining, Tinkering), Lv1-5 | 30% of skill levels (15 of 50) |
| Paths / professions | 10 Lv5 + 20 Lv10 | 6 Lv5 paths (perks only, no wing) | 20% |
| Playable screens | ~30 | 8: Studio, Kiln Row, Town Plaza, Post Office, Craft Superstore, Mr. Oakes's lane, bus stop (Main Street), Clay Quarry | ~27% |
| Named NPCs | ~13 | 4 + 1 by letter (Postmaster, Mr. Oakes, Quarry Foreman, Superstore clerk; Marigold) | ~35% |
| Seasons | 4 | Spring (framework for all 4) | 25% of a year |
| Pottery forms | ~10 + psychedelic | 5 (cup, bowl, vase, planter, mug) | ~40% |
| Kiln levels | 5 | 2 | 40% |
| Mine levels | 6 + quarry | quarry only | ~14% |
| Script files | 184 | 140 | 76% (foundation + 3 skills) |
| **Overall content** | | | **~20%** |

## In
- Everything from the demo, without its 3-day limit, plus:
- **Graduation intro:** the full 11-shot cutscene (S1-S11), skippable, gives the starting keepsakes.
- **Save / load:** one slot, autosave on sleep, Continue from the main menu.
- **Calendar:** 28-day Spring with weekdays; the season system exists but Summer-Winter have no content yet.
- **Weather:** sunny / rain / storm, rain overlay, puddles, snails on rainy days.
- **Stamina** and a hotbar with tools (pickaxe, wrench).
- **Ceramics Lv1-5:** proper Greenware -> Bisque -> Glazed -> Fired, two-ingredient glaze mixing with unlocked recipes,
  kiln Lv1 with one upgrade to Lv2, planters and mugs at Lv4.
- **Mining Lv1-5 (quarry only):** clay nodes, the slaking tub / sieve / drying, wild red clay body.
- **Tinkering Lv1-5:** one scripted breakdown (the kiln ERR on Day 8) and the repair screen.
- **Lv5 path choice for each skill** (Wheelwright / Glaze Chemist, Prospector / Geologist, Mechanic / Inventor)
  with their modifiers. No Lv10 professions, no wing stations, no respec.
- **Shops:** the Craft Superstore (clay, glaze materials, kiln parts, the kiln Lv2 upgrade).
- **Friendship:** hearts 0-5, talk once a day. No gifting yet.
- **NPC schedules:** 2-3 spots a day on fixed waypoints.
- **Bus:** one route, Main Street <-> Clay Quarry.
- **Status effects:** Cozy (Marigold's chamomile tea) and Calm, to prove out the system.

## Out (later builds)
Restoration and Volunteering skills · Lv6-10 and all Lv10 professions · the shop next door, City Hall property desk,
studio wing · the mine (levels 1-6) and the lift · Summer, Fall, Winter content · festivals · gifting · consignment,
commissions board · apothecary, thrift store, trattoria, City Hall, church, cemetery, homes, basement den ·
random breakdowns · psychedelic overlays · furniture placement mode.

## The season
| Days | What happens |
|---|---|
| 1 | Graduation intro -> bus arrives -> studio. **Lost Letters** on the notice board. |
| 2 | Marigold's letter: **Teacups by Post**. |
| 3-7 | Settle in: Superstore opens, Ceramics Lv2-3, first sales. Mr. Oakes says hi on his lane. |
| 5+ | Mr. Oakes: **First Trip to the Quarry** (bus, pickaxe, wild clay). Mining XP starts. |
| 8 | The kiln shows the repair marker: **The Kiln That Says ERR**. Tinkering XP starts. |
| 9-28 | Open play: reach Lv5 in each skill, pick paths, save up for the kiln Lv2 upgrade. Season-end summary on Day 28. |

## Kept compatible with the full design
Ids, quest step types, skill modifier keys and asset names all match the full game's docs, so content written
here (quest assets, item definitions, skill JSON) can be moved forward by hand later.
