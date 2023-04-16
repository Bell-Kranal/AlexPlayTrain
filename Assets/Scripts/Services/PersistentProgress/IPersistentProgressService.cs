using Data;
using Infrastructure.Services;

namespace Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        public PlayerProgress Progress { get; set; }
    }
}