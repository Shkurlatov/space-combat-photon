using UnityEngine;

namespace SpaceCombat.Infrastructure.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis 
        {
            get
            {
                Vector2 axis = InputAxis();

                if (axis == Vector2.zero)
                    axis = UnityAxis();

                return axis;
            }
        }

        private Vector2 UnityAxis() =>
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}
