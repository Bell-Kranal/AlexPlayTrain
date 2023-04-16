using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/Upgrade")]
    public class PlayerStaticData : ScriptableObject
    {
        public List<Upgrade> Upgrades;
    }
}