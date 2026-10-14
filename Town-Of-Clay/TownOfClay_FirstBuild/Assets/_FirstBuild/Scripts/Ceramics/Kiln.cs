// Kiln.cs  (ClayFirstBuild)
// Kiln Lv1 (+ upgrade to Lv2). Overnight firing; breaks on Day 8 (quest 15).
// STUB: declarations only, no fields or methods yet.
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using ClayFirstBuild.Player;
using ClayFirstBuild.Items;
using ClayFirstBuild.Quests;

namespace ClayFirstBuild.Ceramics
{
    public class Kiln : MonoBehaviour, IInteractable { }

    public enum KilnState { Empty, Loaded, Firing, Done }
}
