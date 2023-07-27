using SpaceCombat.Infrastructure.Services;

namespace SpaceCombat.Infrastructure.Configs
{
    public interface IDataProvider : IService
    {
        void LoadConfigs();
        LobbyConfigs GetLobbyConfigs();
        CombatConfigs GetCombatConfigs();
        SpaceShipConfigs GetSpaceShipConfigs();
    }
}
