using UnityEngine;

namespace Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        
        public abstract Vector3 GetAxis { get; }
    }
}