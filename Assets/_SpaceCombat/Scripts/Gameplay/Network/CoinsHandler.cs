using Photon.Pun;
using SpaceCombat.Gameplay.Factory;
using UnityEngine;

namespace SpaceCombat.Gameplay.Network
{
    public class CoinsHandler : MonoBehaviourPun
    {
        private IGameFactory _gameFactory;

        public void Initialize(IGameFactory networkFactory, int coinsCount)
        {
            _gameFactory = networkFactory;

            for (int i = 0; i < coinsCount; i++)
            {
                SpawnCoin();
            }
        }

        private void SpawnCoin()
        {
            GameObject coin = _gameFactory.SpawnCoin();
            coin.GetComponent<Coin>().Initialize(OnCoinDestroy);
        }

        private void OnCoinDestroy()
        {
            SpawnCoin();
        }
    }
}
