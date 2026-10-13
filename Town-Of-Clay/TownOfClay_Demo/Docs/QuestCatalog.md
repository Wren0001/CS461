# Demo quests

Format: **Step type** `id` (count) - tracker text. Rewards after the arrow. Changes from the full game are in *italics*.

### 1. Lost Letters - `lost_letters`
Source: NoticeBoard · giver `postmaster`
1. TalkTo `notice_board` - Read the notice board
2. Collect `letter` (3) - Find dropped letters *(by the studio door, under the plaza bench, by the post box: the
   apothecary and art store aren't in the demo)*
3. Deliver `letter` (3) -> `post_office` - Return the letters

-> Gold 150 · Item `stamp_sheet` · Unlock `parcel_sending`

### 2. Teacups by Post - `teacups_by_post`
Source: Letter · giver `marigold` · arrives morning of Day 2 · requiresTurnIn off
1. Craft `cup_celadon` (4) - Throw and glaze celadon cups *(single firing)*
2. Craft `package_small` - Pack the cups
3. Deliver `package_small` -> `marigold` - Post them Standard

-> Gold 240 · Item `tea_chamomile` *(no Cozy status or friendship in the demo)*

Step types used: CollectItem, DeliverItem, CraftItem, TalkTo. Rewards used: Gold, Item, Unlock.
