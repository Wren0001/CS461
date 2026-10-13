// IQuestServices.cs (ClayDemo)
// What the quest system needs from the rest of the game. DebugQuestServices implements it for testing;
// later a GameServices class in Core/ implements it with the real inventory, wallet and unlocks.
namespace ClayDemo.Quests
{
    public interface IQuestServices
    {
        int CountItem(string itemId);
        void GiveItem(string itemId, int count);
        void AddGold(int amount);
        void Unlock(string unlockId);
        bool IsUnlocked(string unlockId);
    }
}
