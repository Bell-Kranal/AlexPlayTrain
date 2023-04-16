using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        private void Awake() =>
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        private void OnApplicationQuit()
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress is saved!");
        }
    }
}