// ClayProcessingStation.cs  (ClayFirstBuild)
// Slaking tub -> sieve -> wild clay.
// STUB: declarations only, no fields or methods yet.
using UnityEngine;
using System.Collections.Generic;
using ClayFirstBuild.Player;
using ClayFirstBuild.Items;
using ClayFirstBuild.Quests;

namespace ClayFirstBuild.Mining
{
    public class ClayProcessingStation : MonoBehaviour, IInteractable { }

    public enum ProcessStep { Slake, Sieve, Dry }
}
