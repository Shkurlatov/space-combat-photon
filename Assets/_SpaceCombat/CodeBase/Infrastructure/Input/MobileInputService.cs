using UnityEngine;

namespace SpaceCombat.Infrastructure.Input
{
    public class MobileInputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Fire = "Fire";

        public Vector2 Axis => InputAxis();

        public bool IsAttackButtonUp()
        {
            return SimpleInput.GetButtonUp(Fire);
        }

        private Vector2 InputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }
    }
}
