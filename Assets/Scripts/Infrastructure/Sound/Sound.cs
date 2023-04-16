using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class SoundService : MonoBehaviour, ISoundPlayer
    {
        protected IAssets AssetProvider; 
        protected AudioSource Audio;
        
        public AudioClip LoadClip(string path) =>
            AssetProvider.LoadSound(path);

        public abstract void PlaySound();
    }
}