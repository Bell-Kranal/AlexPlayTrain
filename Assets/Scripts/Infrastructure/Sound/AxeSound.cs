using Infrastructure.AssetManagement;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Sound
{
    public class AxeSound : SoundService
    {
        private void Awake()
        {
            AssetProvider = AllServices.Container.Single<IAssets>();
            
            Audio = GetComponent<AudioSource>();
            Audio.clip = LoadClip(AssetPath.AxeSound);
        }

        public override void PlaySound() =>
            Audio.Play();
    }
}