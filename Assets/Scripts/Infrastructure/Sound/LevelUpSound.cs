using Infrastructure.AssetManagement;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Sound
{
    public class LevelUpSound : SoundService
    {
        private void Awake()
        {
            AssetProvider = AllServices.Container.Single<IAssets>();

            Audio = GetComponent<AudioSource>();
            Audio.clip = LoadClip(AssetPath.LevelUp);
        }

        public override void PlaySound() =>
            Audio.Play();
    }
}