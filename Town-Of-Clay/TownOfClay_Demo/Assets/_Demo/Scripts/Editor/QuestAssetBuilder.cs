// QuestAssetBuilder.cs (ClayDemo)
// Menu: Tools > Clay Demo > Create Quest Assets.
// Builds one QuestDefinition asset per quest in Data/Quests, with its steps and rewards stored inside
// the same asset. Re-running rebuilds them from this file, so edit quest content here or in the
// Inspector, not both.
using System.IO;
using UnityEditor;
using UnityEngine;
using ClayDemo.Quests;
using ClayDemo.Quests.Steps;
using ClayDemo.Quests.Rewards;

namespace ClayDemo.EditorTools
{
    public static class QuestAssetBuilder
    {
        const string Folder = "Assets/_Demo/Data/Quests";

        [MenuItem("Tools/Clay Demo/Create Quest Assets")]
        public static void Build()
        {
            EnsureFolder(Folder);

            // 1. Lost Letters (notice board on the plaza; turned in to the Postmaster)
            var q1 = Quest("lost_letters", "Lost Letters",
                "The Postmaster dropped three letters between the post office and Kiln Row. Find them and bring them back.",
                QuestSource.NoticeBoard, "postmaster", "notice_board", offerOnStart: true, requiresTurnIn: true);
            TalkTo(q1, "notice_board", "Read the notice board");
            Collect(q1, "letter", 3, "Find dropped letters");
            Deliver(q1, "letter", 3, "post_office", "Return the letters");
            Gold(q1, 150);
            Item(q1, "stamp_sheet", 1, "Stamp sheet");
            Unlock(q1, "parcel_sending", "Parcel sending");
            Done(q1);

            // 2. Teacups by Post (started by Marigold's letter: MailManager calls StartQuest)
            var q2 = Quest("teacups_by_post", "Teacups by Post",
                "Marigold's teacup order went missing again. Throw four cups, glaze them celadon, and post them to her.",
                QuestSource.Letter, "marigold", "", offerOnStart: false, requiresTurnIn: false);
            Craft(q2, "cup_celadon", 4, "Throw and glaze celadon cups");
            Craft(q2, "package_small", 1, "Pack the cups");
            Deliver(q2, "package_small", 1, "marigold", "Post them Standard");
            Gold(q2, 240);
            Item(q2, "tea_chamomile", 1, "Chamomile tea");
            Done(q2);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"[QuestAssetBuilder] Quest assets written to {Folder}. On the QuestManager, right-click > 'Fill list with every quest in the project'.");
        }

        // ------------------------------------------------------------ helpers
        static QuestDefinition Quest(string id, string title, string description, QuestSource source, string giverId,
                                     string offeredAt, bool offerOnStart, bool requiresTurnIn)
        {
            string path = $"{Folder}/{id}.asset";
            if (File.Exists(path)) AssetDatabase.DeleteAsset(path);
            var q = ScriptableObject.CreateInstance<QuestDefinition>();
            q.name = id;
            q.id = id;
            q.title = title;
            q.description = description;
            q.source = source;
            q.giverId = giverId;
            q.offeredAt = offeredAt;
            q.offerOnStart = offerOnStart;
            q.requiresTurnIn = requiresTurnIn;
            AssetDatabase.CreateAsset(q, path);
            return q;
        }

        static T Sub<T>(QuestDefinition q, string name) where T : ScriptableObject
        {
            var o = ScriptableObject.CreateInstance<T>();
            o.name = name;
            AssetDatabase.AddObjectToAsset(o, q);
            return o;
        }

        static void TalkTo(QuestDefinition q, string target, string text)
        { var s = Sub<TalkToStep>(q, $"{q.steps.Count + 1:00} TalkTo {target}"); s.targetId = target; s.trackerText = text; q.steps.Add(s); }

        static void Collect(QuestDefinition q, string item, int n, string text)
        { var s = Sub<CollectItemStep>(q, $"{q.steps.Count + 1:00} Collect {item}"); s.itemId = item; s.required = n; s.trackerText = text; q.steps.Add(s); }

        static void Deliver(QuestDefinition q, string item, int n, string target, string text)
        { var s = Sub<DeliverItemStep>(q, $"{q.steps.Count + 1:00} Deliver {item}"); s.itemId = item; s.required = n; s.targetId = target; s.trackerText = text; q.steps.Add(s); }

        static void Craft(QuestDefinition q, string item, int n, string text)
        { var s = Sub<CraftItemStep>(q, $"{q.steps.Count + 1:00} Craft {item}"); s.itemId = item; s.required = n; s.trackerText = text; q.steps.Add(s); }

        static void Gold(QuestDefinition q, int amount)
        { var r = Sub<GoldReward>(q, $"Reward {amount}g"); r.amount = amount; q.rewards.Add(r); }

        static void Item(QuestDefinition q, string item, int count, string display)
        { var r = Sub<ItemReward>(q, $"Reward item {item}"); r.itemId = item; r.count = count; r.displayName = display; q.rewards.Add(r); }

        static void Unlock(QuestDefinition q, string unlock, string display)
        { var r = Sub<UnlockReward>(q, $"Reward unlock {unlock}"); r.unlockId = unlock; r.displayName = display; q.rewards.Add(r); }

        static void Done(QuestDefinition q) => EditorUtility.SetDirty(q);

        static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path)) return;
            string parent = Path.GetDirectoryName(path).Replace('\\', '/');
            EnsureFolder(parent);
            AssetDatabase.CreateFolder(parent, Path.GetFileName(path));
        }
    }
}
