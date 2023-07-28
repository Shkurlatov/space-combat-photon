using SpaceCombat.Infrastructure.Services;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void SetScreenSize();
        void InstantiateSpaceShip(int playerCount, int playerNumber);
        GameObject InstantiateCoin();
        void Cleanup();
    }
}