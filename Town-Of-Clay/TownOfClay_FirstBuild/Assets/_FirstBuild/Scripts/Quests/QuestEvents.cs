// QuestEvents.cs (ClayFirstBuild)
// The event bus every other system uses to tell the quest system what the player did.
// Game code calls the static helpers, e.g. QuestEvents.ItemCrafted("cup_celadon").
using System;

namespace ClayFirstBuild.Quests
{
    public enum QuestEventType
    {
        ItemCollected,
        ItemDelivered,
        ItemCrafted,
        TalkedTo,
        LocationEntered,
        CounterRaised,
    }

    [Serializable]
    public struct QuestEvent
    {
        public QuestEventType type;
        public string id;        // item, NPC, location or counter id
        public string targetId;  // who/where an item was delivered to
        public int amount;

        public QuestEvent(QuestEventType type, string id, string targetId = null, int amount = 1)
        {
            this.type = type;
            this.id = id;
            this.targetId = targetId;
            this.amount = amount;
        }

        public override string ToString() =>
            string.IsNullOrEmpty(targetId) ? $"{type}({id} x{amount})" : $"{type}({id} x{amount} -> {targetId})";
    }

    public static class QuestEvents
    {
        public static event Action<QuestEvent> Raised;

        public static void Raise(QuestEvent e) => Raised?.Invoke(e);

        /// <summary>Call after an item lands in the inventory (PlayerInventory, ItemPickup).</summary>
        public static void ItemCollected(string itemId, int amount = 1) =>
            Raise(new QuestEvent(QuestEventType.ItemCollected, itemId, null, amount));

        /// <summary>Call after an item was handed over and removed (PostOfficeCounter, DropOffPoint).</summary>
        public static void ItemDelivered(string itemId, string targetId, int amount = 1) =>
            Raise(new QuestEvent(QuestEventType.ItemDelivered, itemId, targetId, amount));

        /// <summary>Call when something is made (Kiln unload, packing a parcel, RepairJob).</summary>
        public static void ItemCrafted(string itemId, int amount = 1) =>
            Raise(new QuestEvent(QuestEventType.ItemCrafted, itemId, null, amount));

        /// <summary>Call when the player talks to an NPC or reads an object (notice board, kiln).</summary>
        public static void TalkedTo(string targetId) =>
            Raise(new QuestEvent(QuestEventType.TalkedTo, targetId));

        /// <summary>Call from LocationTrigger / scene loads (e.g. "quarry").</summary>
        public static void LocationEntered(string locationId) =>
            Raise(new QuestEvent(QuestEventType.LocationEntered, locationId));

        /// <summary>Call for countable actions that aren't items (e.g. "clay_node_dug").</summary>
        public static void CounterRaised(string counterId, int amount = 1) =>
            Raise(new QuestEvent(QuestEventType.CounterRaised, counterId, null, amount));
    }
}
