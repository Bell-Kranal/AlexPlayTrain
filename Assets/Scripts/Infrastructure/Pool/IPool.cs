using Infrastructure.Services;

namespace Infrastructure.Pool
{
    public interface IPool<T> : IService
    {
        public T PoolElement { get; set; }
    }
}