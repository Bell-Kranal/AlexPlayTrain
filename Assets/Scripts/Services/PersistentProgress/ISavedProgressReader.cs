using Data;

namespace Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        public void LoadProgress(PlayerProgress progress);
    }
}