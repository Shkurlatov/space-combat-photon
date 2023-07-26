using UnityEngine;

namespace _SpaceCombat.CodeBase.Infrastructure.GameConfigs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SpaceShipConfigs", fileName = "SpaceShipConfigs")]
    public class SpaceShipConfigsSO : ScriptableObject
    {
        [Range(20, 50)]
        public float RotationSpeed;

        [Range(15, 40)]
        public float MovementSpeed;

        [Range(0.1f, 2.0f)]
        public float ReloadTime;

        [Range(1, 20)]
        public int ProtectionPoints;
    }
}
