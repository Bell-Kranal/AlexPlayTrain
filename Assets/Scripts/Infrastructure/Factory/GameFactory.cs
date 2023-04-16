using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Pool;
using Infrastructure.Sound;
using Logic;
using Player;
using Services.PersistentProgress;
using StaticData;
using Trees;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string TreeParent = "TreeParent";
        
        private readonly IAssets _assets;
        private readonly IStaticDataLoader _dataLoader;
        private readonly IPool<TreeHitEffect> _hitEffectPool;
        private readonly IPool<UpgradeEffect> _upgradeEffectPool;

        public List<ISavedProgressReader> ProgressReader { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private ICounter _treeCounter;
        private AxeTriggers _axeTriggers;
        private LevelUpSound _levelUpSound;

        public GameFactory(IAssets assets, IStaticDataLoader dataLoader, IPool<TreeHitEffect> hitEffectPool, IPool<UpgradeEffect> upgradeEffectPool)
        {
            _assets = assets;
            _dataLoader = dataLoader;
            _hitEffectPool = hitEffectPool;
            _upgradeEffectPool = upgradeEffectPool;
        }

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            GameObject player = InstantiateRegistered(AssetPath.PlayerPrefabPath, initialPoint.transform.position);

            _axeTriggers = player.GetComponentInChildren<AxeTriggers>();
            _levelUpSound = player.GetComponentInChildren<LevelUpSound>();
            
            return player;
        }

        public void CreateHUD()
        {
            GameObject hud = InstantiateRegistered(AssetPath.MobileHUDPath);

            _treeCounter = hud.GetComponentInChildren<ICounter>();
            
            _dataLoader.LoadUpgrades();
            
            UpgradeButton upgradeButton = hud.GetComponentInChildren<UpgradeButton>();
            upgradeButton.Upgraded += _axeTriggers.Upgrade;
            upgradeButton.Upgrades = _dataLoader.GetUpgrades();
            upgradeButton.TreeCounter = _treeCounter;
            upgradeButton.SetLevelUpSound(_levelUpSound);
        }

        public void Cleanup()
        {
            ProgressReader.Clear();
            ProgressWriters.Clear();
        }

        public void CreateTrees()
        {
            _dataLoader.LoadTrees();

            TreeStaticData spruceData = _dataLoader.ForTree(TreeTypeId.Spruce);
            TreeStaticData christmasTree = _dataLoader.ForTree(TreeTypeId.ChristmasTree);
            TreeStaticData simpleTree = _dataLoader.ForTree(TreeTypeId.SimpleTree);

            CircleRandomTreeSpawn(spruceData);
            CircleRandomTreeSpawn(christmasTree);
            CircleRandomTreeSpawn(simpleTree);
        }

        public void CreateParticles(GameObject hitParticlesParent, GameObject upgradeeParticlesParent)
        {
            int hitParticlesCounter = 10;
            int upgradeParticlesCounter = 10;

            CreateParticles(hitParticlesCounter, hitParticlesParent, _hitEffectPool, AssetPath.HitEffect);
            CreateParticles(upgradeParticlesCounter, upgradeeParticlesParent, _upgradeEffectPool, AssetPath.UpgradeEffect);
        }

        private void CreateParticles<TPool>(int size, GameObject parent, IPool<TPool> pool, string path)
        {
            for (int i = 0; i < size; i++)
            {
                pool.PoolElement = InstantiateRegistered(path, Vector3.zero, parent.transform).GetComponent<TPool>();
            }
        }

        private void CircleRandomTreeSpawn(TreeStaticData data)
        {
            int treeCount = Random.Range(20, 35);
            Transform treeParent = GameObject.FindWithTag(TreeParent).transform;
            
            for (int i = 0; i < treeCount; i++)
            {
                float angle = Random.Range(0f, Mathf.PI * 2);
                float distance = Random.Range(2f, 13f);
                Vector3 position = new Vector3(Mathf.Cos(angle) * distance, 0, Mathf.Sin(angle) * distance);
                
                GameObject tree = InstantiateRegistered(data.TreePrefab, position, treeParent);

                InitializeValuesInTree(data, tree);
            }
        }

        private void InitializeValuesInTree(TreeStaticData data, GameObject tree)
        {
            IHealth health = tree.GetComponentInChildren<IHealth>();
            health.MaxHP = data.Health;
            health.CurrentHP = data.Health;

            TreeDestroyer destroyer = tree.GetComponentInChildren<TreeDestroyer>();
            destroyer.Destroyed += _treeCounter.IncreaseCounter;
            destroyer.Price = data.Price;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        private GameObject InstantiateRegistered(string prefabPath, Vector3 at, Transform parent)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at, parent);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at, Transform parent)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity, parent);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }
        
        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }
            
            ProgressReader.Add(progressReader);
        }
    }
}