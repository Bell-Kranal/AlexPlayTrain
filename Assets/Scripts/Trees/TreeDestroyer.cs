using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Trees
{
    [RequireComponent(typeof(IHealth))]
    public class TreeDestroyer : MonoBehaviour, IDestroyable, IEnabler
    {
        [SerializeField] private Transform _tree;
        [SerializeField] private GameObject _stump;
        [SerializeField] private float _scaleDuration;

        public event Action EnabledOrDisabled;
        public event Action<int> Destroyed;
        
        private IHealth _health;
        private Vector3 _firstTreeScale;
        private int _price;

        public int Price
        {
            get => _price;
            set
            {
                if (value > 0)
                {
                    _price = value;
                }
            }
        }

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _firstTreeScale = _tree.localScale;
        }

        private void OnEnable() =>
            _health.HealthChanged += OnHealthChanged;

        private void OnDisable() =>
            _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_health.CurrentHP <= 0f)
            {
                Die();
            }
        }

        public void Die()
        {
            _health.HealthChanged -= OnHealthChanged;
            Destroyed?.Invoke(Price);
            
            ChangeTreeScale(Vector3.zero, DisableTree);
            EnableStump();

            StartCoroutine(Reborn());
        }

        private IEnumerator Reborn()
        {
            EnabledOrDisabled?.Invoke();
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            EnabledOrDisabled?.Invoke();
            
            EnableTree();
            DisableStump();
            ChangeTreeScale(_firstTreeScale, () =>
            {
                _health.ResetHP();
                _health.HealthChanged += OnHealthChanged;
            });
        }

        private void ChangeTreeScale(Vector3 newScale, Action onAnimationEnd = null) =>
            _tree
                .DOScale(newScale, _scaleDuration)
                .OnComplete(
                        () => { onAnimationEnd?.Invoke();}
                    );

        private void DisableTree() =>
            _tree.gameObject.SetActive(false);

        private void EnableTree() =>
            _tree.gameObject.SetActive(true);

        private void EnableStump() =>
            _stump.SetActive(true);
        
        private void DisableStump() =>
            _stump.SetActive(false);
    }
}