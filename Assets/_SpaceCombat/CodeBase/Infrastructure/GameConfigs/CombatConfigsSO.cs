using UnityEngine;

namespace _SpaceCombat.CodeBase.Infrastructure.GameConfigs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CombatConfigs", fileName = "CombatConfigs")]
    public class CombatConfigsSO : ScriptableObject
    {
        [Range(4, 20)]
        public int CoinsAmount;

        [Range(80.0f, 200.0f)]
        public float BulletSpeed;

        [Range(1.0f, 4.0f)]
        public float BulletDestroyTime;
    }
}
