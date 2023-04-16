using System;
using UnityEngine;

namespace Trees
{
    public class TreeHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _currentHP;
        
        private float _maxHP;

        public float CurrentHP
        {
            get => _currentHP;
            set
            {
                if (value <= _maxHP)
                {
                    _currentHP = value;
                }
            }
        }

        public float MaxHP
        {
            get => _maxHP;
            set => _maxHP = value;
        }
        
        public event Action HealthChanged;
        
        public void TakeDamage(float damage)
        {
            CurrentHP -= damage;

            HealthChanged?.Invoke();
        }

        public void ResetHP() =>
            CurrentHP = MaxHP;
    }
}