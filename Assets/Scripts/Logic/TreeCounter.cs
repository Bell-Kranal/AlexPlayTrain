using Data;
using Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(TMP_Text))]
    public class TreeCounter : MonoBehaviour, ISavedProgress, ICounter
    {
        private const string TreesSprite = "<sprite=0>";
        
        private int _counter;
        private TMP_Text _text;

        public int Counter
        {
            get => _counter;
            set
            {
                _counter += value;
                UpdateText();
            }
        }

        private void Awake() =>
            _text = GetComponent<TMP_Text>();

        public void IncreaseCounter(int value) =>
            Counter = value;

        public void UpdateText() =>
            _text.text = GetText();

        private string GetText() =>
            _counter.ToString() + TreesSprite;

        public void LoadProgress(PlayerProgress progress)
        {
            _counter = progress.State.TreesCounter;
            _text.text = GetText();
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.State.TreesCounter = _counter;
    }
}