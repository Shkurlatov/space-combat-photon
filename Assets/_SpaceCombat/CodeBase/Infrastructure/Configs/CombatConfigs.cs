using UnityEngine;

namespace SpaceCombat.Infrastructure.Configs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CombatConfigs", fileName = "CombatConfigs")]
    public class CombatConfigs : ScriptableObject
    {
        [Range(4, 20)]
        public int CoinsAmount;

        [Range(80.0f, 200.0f)]
        public float BulletSpeed;

        [Range(1.0f, 4.0f)]
        public float BulletDestroyTime;
    }
}
