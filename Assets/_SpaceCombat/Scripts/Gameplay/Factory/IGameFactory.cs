using UnityEngine;

namespace SpaceCombat.Gameplay.Factory
{
    public interface IGameFactory
    {
        GameObject SpawnCoin();
    }
}