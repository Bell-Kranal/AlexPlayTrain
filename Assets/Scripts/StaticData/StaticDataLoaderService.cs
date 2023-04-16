using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StaticData
{
    public class StaticDataLoaderService : IStaticDataLoader
    {
        private Dictionary<TreeTypeId,TreeStaticData> _trees;
        private List<Upgrade> _uprgades;
        
        private const string StaticDataTrees = "StaticData/Trees";
        private const string StaticUpgradeData = "StaticData/Player/PlayerUpgrade";

        public void LoadTrees()
        {
            _trees =
                Resources
                    .LoadAll<TreeStaticData>(StaticDataTrees)
                    .ToDictionary(x => x.TreeTypeId, x => x);
        }
        
        public void LoadUpgrades()
        {
            _uprgades =
                Resources
                    .Load<PlayerStaticData>(StaticUpgradeData).Upgrades;
        }

        public TreeStaticData ForTree(TreeTypeId typeId) => 
            _trees.TryGetValue(typeId, out TreeStaticData staticData)
                ? staticData
                : null;

        public List<Upgrade> GetUpgrades() =>
            _uprgades;
    }
}