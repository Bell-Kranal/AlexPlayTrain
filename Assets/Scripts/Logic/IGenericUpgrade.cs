using System;

namespace Logic
{
    public interface IGenericUpgrade<out T> : IUpgrade
    {
        public event Action<T> Upgraded;
        public void Upgrade();
    }
}