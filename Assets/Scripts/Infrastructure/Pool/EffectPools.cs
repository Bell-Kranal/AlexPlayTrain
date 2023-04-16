using UnityEngine;

namespace Infrastructure.Pool
{
    public class EffectPools : IEffectPools
    {
        private readonly IPool<TreeHitEffect> _hitPoolEffect;
        private readonly IPool<UpgradeEffect> _upgradePoolEffect;

        public EffectPools(IPool<TreeHitEffect> hitPoolEffect, IPool<UpgradeEffect> upgradePoolEffect)
        {
            _hitPoolEffect = hitPoolEffect;
            _upgradePoolEffect = upgradePoolEffect;
        }

        public void PlayHitEffect(Vector3 position)
        {
            TreeHitEffect hit = _hitPoolEffect.PoolElement;
            hit.transform.position = position;
            hit.Play();
        }

        public void PlayUpgradeEffect() =>
            _upgradePoolEffect.PoolElement.Play();
    }
}