using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Pool
{
    [RequireComponent(typeof(ParticleSystem))]
    public class TreeHitEffect : MonoBehaviour
    {
        private IPool<TreeHitEffect> _pool;
        private ParticleSystem _particles;

        private void Awake()
        {
            _pool = AllServices.Container.Single<IPool<TreeHitEffect>>();
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