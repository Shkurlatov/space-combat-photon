using Photon.Pun;
using SpaceCombat.Infrastructure.Factory;
using UnityEngine;

namespace SpaceCombat.Gameplay.Network
{
    public class CoinsHandler : MonoBehaviourPun
    {
        private IGameFactory _gameFactory;

        public void Initialize(IGameFactory networkFactory, int coinsCount)
        {
            _gameFactory = networkFactory;

            if (PhotonNetwork.IsMasterClient)
            {
                for (int i = 0; i < coinsCount; i++)
                {
                    SpawnCoin();
                } 
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
