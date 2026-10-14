// GameSettings.cs  (ClayFirstBuild)
// 16 vs 32 art set, volumes, text speed.
// STUB: declarations only, no fields or methods yet.
using UnityEngine;
using System;
using System.Collections.Generic;

namespace ClayFirstBuild.Core
{
    [CreateAssetMenu(menuName = "Clay First Build/GameSettings")]
    public class GameSettings : ScriptableObject { }

    public enum ArtResolution { Px16, Px32 }
}
