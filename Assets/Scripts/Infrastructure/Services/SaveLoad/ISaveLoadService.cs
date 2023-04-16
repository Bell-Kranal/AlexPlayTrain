using Data;

namespace Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        public void SaveProgress();
        public PlayerProgress LoadProgress();
    }
}