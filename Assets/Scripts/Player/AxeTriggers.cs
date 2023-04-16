using Data;
using Infrastructure.Pool;
using Infrastructure.Services;
using Infrastructure.Sound;
using Services.PersistentProgress;
using Trees;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AxeSound))]
    public class AxeTriggers : MonoBehaviour, ISavedProgress
    {
        public bool CanAttack = false;

        private IEffectPools _effectPools;
        private AxeSound _hitSound;
        private int _damage;

        private void Awake()
        {
            _effectPools = AllServices.Container.Single<IEffectPools>();
            _hitSound = GetComponent<AxeSound>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!CanAttack)
                return;
            
            if (other.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(_damage);
                _hitSound.PlaySound();
                _effectPools.PlayHitEffect(other.transform.position);
            }
        }

        public void Upgrade(int newDamage)
        {
            if (newDamage > _damage)
            {
                _damage = newDamage;
                _effectPools.PlayUpgradeEffect();
            }
        }

        public void LoadProgress(PlayerProgress progress) =>
            _damage = progress.State.Damage;

        public void UpdateProgress(PlayerProgress progress) =>
            progress.State.Damage = _damage;
    }
}