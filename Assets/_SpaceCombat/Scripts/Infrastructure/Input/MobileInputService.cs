using UnityEngine;

namespace SpaceCombat.Infrastructure.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => InputAxis();
    }
}
