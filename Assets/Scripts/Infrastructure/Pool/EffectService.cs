using System.Collections.Generic;

namespace Infrastructure.Pool
{
    public class EffectService<T> : IPool<T>
    {
        private Queue<T> _pool;

        public T PoolElement
        {
            get => _pool.Dequeue();
            set => _pool.Enqueue(value);
        }

        public EffectService() =>
            _pool = new Queue<T>();
    }
}