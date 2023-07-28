using UnityEngine;

namespace SpaceCombat.Infrastructure.Configs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SpaceShipConfigs", fileName = "SpaceShipConfigs")]
    public class SpaceShipConfigs : ScriptableObject
    {
        [Range(20, 50)]
        public float RotationSpeed;

        [Range(5, 20)]
        public float MovementSpeed;

        [Range(0.1f, 2.0f)]
        public float ReloadTime;

        [Range(1, 20)]
        public int ProtectionPoints;
    }
}
