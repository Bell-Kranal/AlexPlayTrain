using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Pool;
using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using Input;
using Services.PersistentProgress;
using StaticData;
using UnityEngine;
using IGameFactory = Infrastructure.Factory.IGameFactory;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string BootstrapScene = "BootstrapScene";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.Load(BootstrapScene, onLoaded: EnterLoadLevel);

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IPool<TreeHitEffect>>(new EffectService<TreeHitEffect>());
            _services.RegisterSingle<IPool<UpgradeEffect>>(new EffectService<UpgradeEffect>());
            _services.RegisterSingle<IEffectPools>(new EffectPools(_services.Single<IPool<TreeHitEffect>>(), _services.Single<IPool<UpgradeEffect>>()));
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IStaticDataLoader>(new StaticDataLoaderService());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>(), _services.Single<IStaticDataLoader>(), _services.Single<IPool<TreeHitEffect>>(), _services.Single<IPool<UpgradeEffect>>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }

        private IInputService GetInputService()
        {
            if (Application.isMobilePlatform)
            {
                return new MobileInputService();
            }
            
            return new StandaloneInputService();
        }
    }
}