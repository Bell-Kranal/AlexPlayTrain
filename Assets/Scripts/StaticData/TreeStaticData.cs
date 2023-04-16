using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "TreeData", menuName = "StaticData/Tree")]
    public class TreeStaticData : ScriptableObject
    {
        public TreeTypeId TreeTypeId;

        [Range(1, 30)]
        public int Health;
        [Range(1, 50)]
        public int Price;

        public GameObject TreePrefab;
    }
}