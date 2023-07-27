using UnityEngine;

namespace SpaceCombat.Infrastructure.Configs
{
    public class DataProvider : IDataProvider
    {
        private LobbyConfigs _lobbyConfigs;
        private CombatConfigs _combatConfigs;
        private SpaceShipConfigs _spaceShipConfigs;

        public void LoadConfigs()
        {
            _lobbyConfigs = Resources.Load<LobbyConfigs>(ConfigsPath.LOBBY_CONFIGS_PATH);
            _combatConfigs = Resources.Load<CombatConfigs>(ConfigsPath.COMBAT_CONFIGS_PATH);
            _spaceShipConfigs = Resources.Load<SpaceShipConfigs>(ConfigsPath.SPACE_SHIP_CONFIGS_PATH);
        }

        public LobbyConfigs GetLobbyConfigs()
        {
            return _lobbyConfigs;
        }

        public CombatConfigs GetCombatConfigs()
        {
            return _combatConfigs;
        }

        public SpaceShipConfigs GetSpaceShipConfigs()
        {
            return _spaceShipConfigs;
        }
    }
}
