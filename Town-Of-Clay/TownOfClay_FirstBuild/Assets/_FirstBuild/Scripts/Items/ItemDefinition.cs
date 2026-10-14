// ItemDefinition.cs  (ClayFirstBuild)
// Id, name, 16 + 32 icons, category, stack size, base price.
// STUB: declarations only, no fields or methods yet.
using UnityEngine;
using System;
using System.Collections.Generic;

namespace ClayFirstBuild.Items
{
    [CreateAssetMenu(menuName = "Clay First Build/ItemDefinition")]
    public class ItemDefinition : ScriptableObject { }

    public enum ItemCategory { Pottery, Material, Mail, Gift }
}
