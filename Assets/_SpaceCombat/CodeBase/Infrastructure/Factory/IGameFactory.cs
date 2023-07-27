using SpaceCombat.Infrastructure.Services;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void SetScreenSize();
        void InstantiateSpaceShip(int playerCount, int playerNumber);
        GameObject InstantiateCoin();
        void InstantiateBullets(Vector3 position, Quaternion rotation, float lag);
        void Cleanup();
    }
}