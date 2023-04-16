using Infrastructure.AssetManagement;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Sound
{
    public class FireSound : SoundService
    {
        [SerializeField] private float _maxDistance;
        
        private const string Player = "Player";

        private Transform _playerTransform;
        
        private void Awake()
        {
            AssetProvider = AllServices.Container.Single<IAssets>();

            Audio = GetComponent<AudioSource>();
            Audio.clip = LoadClip(AssetPath.FireSound);
            PlaySound();
        }

        private void Update()
        {
            if (_playerTransform == null)
            {
                _playerTransform = GameObject.FindWithTag(Player)?.transform;
                return;
            }
            
            float distance = Vector2.Distance(transform.position, _playerTransform.position);
            Audio.volume = Mathf.Clamp01(0.5f - distance / _maxDistance);
        }

        public override void PlaySound() =>
            Audio.Play();
    }
}