using System.Collections.Generic;
using Infrastructure.Services;
using Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public GameObject CreatePlayer(GameObject initialPoint);
        public void CreateHUD();
        public List<ISavedProgressReader> ProgressReader { get; }
        public List<ISavedProgress> ProgressWriters { get; }
        public void Cleanup();
        public void CreateTrees();
        public void CreateParticles(GameObject hitParticlesParent, GameObject upgradeParticlesParent);
    }
}