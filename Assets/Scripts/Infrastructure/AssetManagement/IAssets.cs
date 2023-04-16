using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        public GameObject Instantiate(string path);
        public GameObject Instantiate(string path, Vector3 at);
        public GameObject Instantiate(string path, Vector3 at, Transform parent);
        public AudioClip LoadSound(string path);
    }
}