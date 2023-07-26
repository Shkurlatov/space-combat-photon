using SpaceCombat.Infrastructure.Services;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject SpawnCoin();
        void Cleanup();
    }
}