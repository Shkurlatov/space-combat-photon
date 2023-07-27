using UnityEngine;

namespace SpaceCombat.Infrastructure.Configs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/RoomConfigs", fileName = "RoomConfigs")]
    public class LobbyConfigs : ScriptableObject
    {
        [Range(2, 8)]
        public int MaxPlayersAmount;
    }
}
