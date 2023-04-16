using Cinemachine;
using Infrastructure.Factory;
using Logic;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string Initialpoint = "InitialPoint";
        private const string CinemachineCamera = "CinemachineCamera";
        private const string Pool = "Pool";
        private const string UpgradePlayerParticles = "UpgradePlayerParticles";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            GameObject player = _gameFactory.CreatePlayer(GameObject.FindWithTag(Initialpoint));
            _gameFactory.CreateHUD();
            _gameFactory.CreateTrees();
            _gameFactory.CreateParticles(GameObject.FindWithTag(Pool), GameObject.FindWithTag(UpgradePlayerParticles));
            
            CameraFollow(player);
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReader)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private static void CameraFollow(GameObject player)
        {
            CinemachineVirtualCamera cinemachine = GameObject.FindWithTag(CinemachineCamera).GetComponent<CinemachineVirtualCamera>();
            cinemachine.Follow = player.transform;
            cinemachine.LookAt = player.transform;
        }
    }
}