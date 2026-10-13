// DebugQuestServices.cs (ClayDemo)
// A stand-in for the inventory, wallet and unlocks so quests can be played and tested now.
// Replace with a real GameServices (Core/) once PlayerInventory, Wallet and UnlockRegistry have code.
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClayDemo.Quests
{
    public class DebugQuestServices : MonoBehaviour, IQuestServices
    {
        [Serializable]
        public class Entry
        {
            public string id;
            public int amount;
        }

        [SerializeField] int gold;
        [SerializeField] List<Entry> items = new List<Entry>();
        [SerializeField] List<string> unlocks = new List<string>();
        [SerializeField] bool log = true;

        public int Gold => gold;

        public int CountItem(string itemId)
        {
            var e = items.Find(x => x.id == itemId);
            return e != null ? e.amount : 0;
        }

        public void GiveItem(string itemId, int count)
        {
            Add(items, itemId, count);
            Log($"+{count} {itemId}");
        }

        /// <summary>Removes items (for testing deliveries). Returns false if there aren't enough.</summary>
        public bool TakeItem(string itemId, int count)
        {
            var e = items.Find(x => x.id == itemId);
            if (e == null || e.amount < count) return false;
            e.amount -= count;
            if (e.amount == 0) items.Remove(e);
            return true;
        }

        public void AddGold(int amount) { gold += amount; Log($"+{amount}g (now {gold}g)"); }

        public void Unlock(string unlockId)
        {
            if (!unlocks.Contains(unlockId)) unlocks.Add(unlockId);
            Log($"Unlocked {unlockId}");
        }

        public bool IsUnlocked(string unlockId) => unlocks.Contains(unlockId);

        static void Add(List<Entry> list, string id, int amount)
        {
            var e = list.Find(x => x.id == id);
            if (e == null) list.Add(new Entry { id = id, amount = amount });
            else e.amount += amount;
        }

        void Log(string msg) { if (log) Debug.Log($"[DebugServices] {msg}", this); }
    }
}
