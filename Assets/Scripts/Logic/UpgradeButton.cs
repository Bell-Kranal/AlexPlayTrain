using System;
using System.Collections.Generic;
using Data;
using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Sound;
using Services.PersistentProgress;
using StaticData;
using TMPro;
using UnityEngine;

namespace Logic
{
    public class UpgradeButton : MonoBehaviour, IGenericUpgrade<int>, ISavedProgress
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _image;
        [SerializeField] private GameObject _maxLevel;
        
        public ICounter TreeCounter;
        public List<Upgrade> Upgrades;
        
        public event Action<int> Upgraded;
        
        private const string Sprite = "<sprite=0>";

        private int _currenLevel;
        private LevelUpSound _sound;
        private ISaveLoadService _saveLoadService;

        private void Awake() =>
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        public void Upgrade()
        {
            if (_currenLevel + 1 < Upgrades.Count && TreeCounter.Counter >= Upgrades[_currenLevel].Price)
            {
                TreeCounter.Counter = -Upgrades[_currenLevel].Price;
                _currenLevel++;
                
                SetText();
                _sound.PlaySound();
                _saveLoadService.SaveProgress();
                Upgraded?.Invoke(Upgrades[_currenLevel - 1].Damage);

                if (_currenLevel + 1 >= Upgrades.Count)
                {
                    DisableUpgrade();
                }
            }
        }


        private void SetText() =>
            _text.text = Upgrades[_currenLevel].Price.ToString() + Sprite;

        public void LoadProgress(PlayerProgress progress)
        {
            _currenLevel = progress.WorldData.CurrentLevel;
            
            if (_currenLevel + 1 >= Upgrades.Count)
            {
                DisableUpgrade();
            }
            else
            {
                SetText();
            }
        }

        private void DisableUpgrade()
        {
            _text.gameObject.SetActive(false);
            _image.gameObject.SetActive(false);

            _maxLevel.gameObject.SetActive(true);
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.CurrentLevel = _currenLevel;

        public void SetLevelUpSound(LevelUpSound sound) =>
            _sound = sound;
    }
}