using UnityEngine;

namespace _SpaceCombat.CodeBase.Infrastructure.GameConfigs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/RoomConfigs", fileName = "RoomConfigs")]
    public class RoomConfigsSO : ScriptableObject
    {
        [Range(2, 8)]
        public int MaxPlayersAmount;
    }
}
