// Kiln.cs  (ClayDemo)
// Kiln Lv1: load, fire overnight, unload. Raises ItemCrafted.
// STUB: declarations only, no fields or methods yet.
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using ClayDemo.Player;
using ClayDemo.Items;
using ClayDemo.Quests;

namespace ClayDemo.Ceramics
{
    public class Kiln : MonoBehaviour, IInteractable { }

    public enum KilnState { Empty, Loaded, Firing, Done }
}
