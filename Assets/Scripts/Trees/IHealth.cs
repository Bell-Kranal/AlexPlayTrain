using System;

namespace Trees
{
    public interface IHealth
    {
        public float CurrentHP { get; set; }
        public float MaxHP { get; set; }
        public event Action HealthChanged;
        public void TakeDamage(float damage);
        public void ResetHP();
    }
}