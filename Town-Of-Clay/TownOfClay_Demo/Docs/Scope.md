# Demo scope (~10%)

## The pitch
*"Three days in your new studio."* You arrive on Moving Day, the post office has lost Marigold's teacups,
and by Day 3 you've thrown, glazed, fired and posted your first commission.

## The numbers
| Part of the game | Full game | Demo | Share |
|---|---|---|---|
| Quests | 19 | 2 (Lost Letters, Teacups by Post) | 11% |
| Skills | 5, Lv1-10, paths at 5 and 10 | Ceramics only, Lv1-3, no paths | 6% of skill levels (3 of 50) |
| Professions / wing stations | 30 | 0 | 0% |
| Playable screens (scenes) | ~30 | 4: Studio, Kiln Row street, Town Plaza, Post Office | ~13% |
| Named NPCs | ~13 | 2 (the Postmaster; Marigold by letter) | ~15% |
| Seasons | 4 x 28 days | 3 days of Spring | 3% of a year |
| Pottery forms | ~10 + psychedelic | 3 (cup, bowl, vase) | ~25% |
| Glazes | open-ended chemistry | 3 pre-made (Clear, Celadon, Iron Red) | small |
| Kiln levels | 5 | 1 | 20% |
| Script files | 184 | 80 | 43% (the foundation is most of the code) |
| **Overall content** | | | **~10%** |

The code share is higher than the content share on purpose: the demo still needs the player, clock,
inventory, quests, dialog and UI foundations. Those carry straight into the first build.

## In
- **Studio (start state):** Kiln Lv1, the starting studio wheel, the glaze table, the moving boxes, the shipping outbox, and the upstairs flat with the bed.
- **Wheel minigame:** all 6 throw stages (Lump -> Finished), PERFECT/GOOD/MISS words, clay splatter on a miss. No combos.
- **Glazing:** pick one of 3 pre-made glazes. No mixing, no test tiles.
- **Firing:** one overnight **single firing** (glaze straight onto greenware) so a piece goes wheel -> kiln -> done in one night.
  The full game's bisque step is cut to fit 3 days.
- **Ceramics skill:** XP from throws and firings; Lv2 unlocks bowls, Lv3 vases. Level-up uses `fx_sparkle_burst`.
- **Selling:** the shipping outbox pays out overnight (`fx_coin_pop` next morning).
- **Mail:** mailbox with the flag animation, letters, the post office counter, parcels (Standard postage only).
- **Town:** Kiln Row (studio exterior, lamps, grass, butterflies), the Town Plaza (fountain, notice board, red post box), the Post Office.
- **Day/night:** a fixed spring light gradient; street lamps turn on at dusk.
- **Quests 1 and 2**, the quest tracker, ! / ? markers, the notice-board offer popup.
- **Intro/outro:** the title card, "Chapter 1: Moving Day" card, and a Thanks-for-Playing card after Day 3 (`TitleCards`).

## Out (all of these stay in the full game)
Other skills (Mining, Tinkering, Restoration, Volunteering) and all professions · the graduation cutscene (only its
title and chapter cards are used) · saving (each run is a fresh 3 days) · seasons, weather, festivals · friendship
hearts and gifting · NPC schedules (the Postmaster stays behind the counter) · stamina · shops and the art store ·
status effects (Marigold's tea is just an item here) · quarry and mine · bus · basement, apothecary, thrift store,
trattoria, City Hall, church, cemetery, homes · consignment, commissions board · the shop next door and studio wing ·
psychedelic overlays · rotations / furniture placement.

## The 3 days
| Day | Morning | Afternoon | Evening |
|---|---|---|---|
| 1 Moving Day | Wake in the studio. Tutorial throw (a cup). | Plaza: notice board -> **Lost Letters**. Find the 3 letters on Kiln Row and the plaza. | Return them -> 150g, stamp sheet, parcel sending. Load the kiln, sleep. |
| 2 | Mailbox flag up: **Marigold's letter** starts Teacups by Post. Unload the cup. | Throw 3 more cups (Ceramics Lv2 by now; bowls open up). Glaze them Celadon, plus anything you like. | Fire overnight. Sell extras in the outbox. |
| 3 | Unload. Pack 4 celadon cups at the post office. | Post them -> 240g, chamomile tea. Free time: vases if you hit Lv3. | Sleep -> Thanks-for-Playing card with your pieces made and gold earned. |

## How it grows into the first build
Every demo script keeps its name in the first build (`TownOfClay_FirstBuild/`), except three demo-only files:
`DemoSession` (the 3-day frame), `CeramicsSkill` (replaced by a real `SkillSystem`) and `TitleCards` (replaced by the
full cutscene cards). The first build is a separate folder too; you copy code across by hand when you're ready.
