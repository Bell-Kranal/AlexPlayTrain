using UnityEngine;

namespace Input
{
    public class MobileInputService : InputService
    {
        public override Vector3 GetAxis => new Vector3(SimpleInput.GetAxis(Horizontal), 0f, SimpleInput.GetAxis(Vertical));
    }
}