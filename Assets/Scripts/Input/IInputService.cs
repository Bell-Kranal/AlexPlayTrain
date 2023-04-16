using Infrastructure.Services;
using UnityEngine;

namespace Input
{
    public interface IInputService : IService
    {
        public Vector3 GetAxis { get; }
    }
}
