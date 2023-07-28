using UnityEngine;

namespace SpaceCombat.Infrastructure.Configs
{
    public class DataProvider : IDataProvider
    {
        private CombatConfigs _combatConfigs;
        private SpaceShipConfigs _spaceShipConfigs;

        public void LoadConfigs()
        {
            _combatConfigs = Resources.Load<CombatConfigs>(ConfigsPath.COMBAT_CONFIGS_PATH);
            _spaceShipConfigs = Resources.Load<SpaceShipConfigs>(ConfigsPath.SPACE_SHIP_CONFIGS_PATH);
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
