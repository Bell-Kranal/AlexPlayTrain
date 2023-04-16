using UnityEngine;

namespace Infrastructure.Sound
{
    public interface ISoundPlayer
    {
        public AudioClip LoadClip(string path);
        public void PlaySound();
    }
}