using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Pool
{
    public class UpgradeEffect : MonoBehaviour
    {
        private IPool<UpgradeEffect> _pool;
        private ParticleSystem _particles;

        private void Awake()
        {
            _pool = AllServices.Container.Single<IPool<UpgradeEffect>>();
            _particles = GetComponent<ParticleSystem>();
        }

        public void Play()
        {
            _particles.Play();

            Invoke(nameof(ReturnToPool), _particles.main.duration);
        }

        private void ReturnToPool() => 
            _pool.PoolElement = this;
    }
}