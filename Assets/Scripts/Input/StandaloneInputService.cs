using UnityEngine;

namespace Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector3 GetAxis
        {
            get
            {
                Vector3 direction = new Vector3(SimpleInput.GetAxis(Horizontal), 0f, SimpleInput.GetAxis(Vertical));

                if (direction == Vector3.zero)
                {
                    return new Vector3(UnityEngine.Input.GetAxis(Horizontal), 0f, UnityEngine.Input.GetAxis(Vertical));
                }

                return direction;
            }
        }
    }
}