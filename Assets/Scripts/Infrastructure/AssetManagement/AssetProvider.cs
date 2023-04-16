using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(playerPrefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(playerPrefab, at, Quaternion.identity);
        }

        public GameObject Instantiate(string path, Vector3 at, Transform parent)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(playerPrefab, at, Quaternion.identity, parent);
        }

        public AudioClip LoadSound(string path) =>
            Resources.Load<AudioClip>(path);
    }
}