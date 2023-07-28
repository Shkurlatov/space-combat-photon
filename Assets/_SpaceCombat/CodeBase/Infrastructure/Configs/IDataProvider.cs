using SpaceCombat.Infrastructure.Services;

namespace SpaceCombat.Infrastructure.Configs
{
    public interface IDataProvider : IService
    {
        void LoadConfigs();
        CombatConfigs GetCombatConfigs();
        SpaceShipConfigs GetSpaceShipConfigs();
    }
}
