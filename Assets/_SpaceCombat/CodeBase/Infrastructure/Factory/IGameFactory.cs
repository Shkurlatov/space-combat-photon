using SpaceCombat.Infrastructure.Services;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void SetScreenSize();
        GameObject InstantiateSpaceShip();
        GameObject InstantiateCoin();
        GameObject InstantiateBullet();
        void Cleanup();
    }
}