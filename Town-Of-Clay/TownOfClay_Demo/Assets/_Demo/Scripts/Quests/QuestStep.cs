// QuestStep.cs (ClayDemo)
// Base class for one step of a quest. Steps run in order; each keeps its progress in a StepProgress.
using System;
using UnityEngine;

namespace ClayDemo.Quests
{
    [Serializable]
    public class StepProgress
    {
        public int count;
        public bool done;
    }

    public abstract class QuestStep : ScriptableObject
    {
        [Tooltip("Text shown in the quest tracker, e.g. 'Find dropped letters'.")]
        public string trackerText;

        /// <summary>How many are needed (1 for yes/no steps).</summary>
        public virtual int Required => 1;

        /// <summary>Called when the step becomes current. Collect steps check the inventory here.</summary>
        public virtual void OnStart(StepProgress p, IQuestServices s) { }

        /// <summary>Called for every quest event while this step is current.</summary>
        public abstract void OnEvent(QuestEvent e, StepProgress p, IQuestServices s);

        public virtual string ProgressText(StepProgress p) =>
            Required > 1 ? $"{trackerText} {Mathf.Min(p.count, Required)}/{Required}" : trackerText;

        protected void AddCount(StepProgress p, int amount)
        {
            p.count += Mathf.Max(1, amount);
            p.done = p.count >= Required;
        }

        /// <summary>Exact id match, or a prefix match when the wanted id ends in '*' (e.g. "mailbox_*").</summary>
        protected static bool Match(string wanted, string got)
        {
            if (string.IsNullOrEmpty(wanted) || string.IsNullOrEmpty(got)) return false;
            if (wanted.EndsWith("*")) return got.StartsWith(wanted.Substring(0, wanted.Length - 1));
            return wanted == got;
        }
    }
}
