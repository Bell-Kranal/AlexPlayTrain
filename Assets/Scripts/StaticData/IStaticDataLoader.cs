using System.Collections.Generic;
using Infrastructure.Services;

namespace StaticData
{
    public interface IStaticDataLoader : IService
    {
        public void LoadTrees();
        public TreeStaticData ForTree(TreeTypeId typeId);
        public void LoadUpgrades();
        public List<Upgrade> GetUpgrades();
    }
}