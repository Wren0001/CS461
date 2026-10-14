// CutsceneShot.cs  (ClayFirstBuild)
// One shot: start, zoom, action, actor, asset, duration.
// STUB: declarations only, no fields or methods yet.
using UnityEngine;
using System.Collections;
using System;

namespace ClayFirstBuild.Cutscenes
{
    [System.Serializable]
    public class CutsceneShot { }

    public enum ShotAction { PlayAnimation, MoveActor, Dialog, Caption, ShowCard, Wait, Camera, Fade, Flash, IrisOut, SpawnFX, GiveItem }
}
