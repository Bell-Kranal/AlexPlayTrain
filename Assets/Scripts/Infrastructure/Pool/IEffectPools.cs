using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Pool
{
    public interface IEffectPools : IService
    {
        public void PlayHitEffect(Vector3 position);
        public void PlayUpgradeEffect();
    }
}