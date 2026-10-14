# First build quests

Format: **Step type** `id` (count) - tracker text. ⏰ = deadline. Changes from the full game are in *italics*.

### 1. Lost Letters - `lost_letters`
Source: NoticeBoard · giver `postmaster`
1. TalkTo `notice_board` - Read the notice board
2. Collect `letter` (3) - Find dropped letters *(by the Superstore, under the Main Street bench, by the studio door)*
3. Deliver `letter` (3) -> `post_office` - Return the letters

-> Gold 150 · Item `stamp_sheet` · Unlock `parcel_sending`

### 2. Teacups by Post - `teacups_by_post`
Source: Letter · giver `marigold` · requiresTurnIn off
1. Craft `cup_celadon` (4) - Throw and glaze celadon cups
2. Craft `package_small` - Pack the cups
3. Deliver `package_small` -> `marigold` - Post them Standard

-> Gold 240 · Item `tea_chamomile` (gives Cozy) · Friendship marigold +1

### 13. First Trip to the Quarry - `first_quarry_trip`
Source: NPC · giver `mr_oakes` · from Day 5
1. Collect `bus_ticket_return` - Buy a return ticket
2. VisitLocation `quarry`
3. Collect `pickaxe` - Borrow a pickaxe *(from the Quarry Foreman's tool rack)*
4. Collect `raw_red_clay` (5) - Dig red clay
5. Craft `wild_clay` - Slake and sieve it

-> Item `pickaxe` · Item `wild_clay_sack` · *Mining skill starts*

### 15. The Kiln That Says ERR - `kiln_err`
Source: Object (kiln shows repair marker) · *fixed on Day 8 (`ScriptedBreakdown`)* · requiresTurnIn off
1. TalkTo `kiln` - Inspect the kiln
2. Collect `thermocouple_probe` · Collect `fuse` - Buy parts *(Craft Superstore only; Dot's thrift store isn't in this build)*
3. Craft `kiln_repaired` - Repair it at the Tinkering screen

-> Unlock `repair_wheel_drive` · Tinkering XP from `RepairJob`

Step types used: CollectItem, DeliverItem, CraftItem, TalkTo, VisitLocation, Counter.
Rewards used: Gold, Item, Unlock, Friendship, StatusEffect.
