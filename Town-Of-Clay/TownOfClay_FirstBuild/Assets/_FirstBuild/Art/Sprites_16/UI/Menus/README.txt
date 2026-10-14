Throwing minigame - top-down pop-the-lock wheel that pops up beside the character's head (world space, 16px grid)
Widget canvas 56x64 world px (32x32 pack: 112x128). Place it so the bubble tail points at the head:
  tail_left  -> widget up-right of the head (offset about +14, -34 from the character sprite's top-left)
  tail_right -> mirror it to the left side when there is no room on the right.
Draw order: bubble_tail_*, ring (or ring_flash_hit / ring_flash_miss for ~2 frames), target_mark (rotated to the target angle),
wheel spin strip (animations/wheel_topdown_spin_stageN_*), rib_marker (on the ring), lock_pips_Nof10 at the bottom.
Ring: centre (28, 27) inside the widget, radius 21. Rib/target position = (28 + 21cos a, 27 + 21sin a).
Target size (difficulty): targets/target_<size>_<deg>deg_36angles.png - 36 frames (one per 10 deg, frame 0 = 3 o'clock,
going clockwise), each frame a full 56x64 overlay at the widget origin. Sizes and hit windows (full arc width):
  xs 10 deg (hardest), s 18, m 28 (default), l 42, xl 60 (easiest). Hit test: abs(angle_diff(rib, target)) <= deg/2.
  Pick the frame round(target_deg / 10) % 36; snap your target angles to 10 deg steps.
target_<size>_<deg>deg_at_top and rib_marker_at_top are single pieces at -90 deg for quick use.
Clay stages from above (advance every 2 locks): 0 lump, 1 centered, 2 opened, 3 walls pulled, 4 shaped, 5 finished vase.
Previous full-screen version: clay_game_backup/.
