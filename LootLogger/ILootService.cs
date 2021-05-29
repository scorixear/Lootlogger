
using SUNLootLogger.Model;

namespace SUNLootLogger
{
    public interface ILootService
    {
        void AddLootForPlayer(Loot loot, string playerName);
        void SaveLootsToFile();

        void UploadLoots();

    }
}
