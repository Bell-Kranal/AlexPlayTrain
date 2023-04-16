using System;

namespace Trees
{
    public interface IDestroyable
    {
        public event Action<int> Destroyed;
        
        public void Die();
    }
}