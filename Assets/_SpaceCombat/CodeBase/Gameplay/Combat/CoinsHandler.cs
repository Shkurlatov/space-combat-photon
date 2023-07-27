using Photon.Pun;
using SpaceCombat.Infrastructure.Factory;
using UnityEngine;

namespace SpaceCombat.Gameplay.Combat
{
    public class CoinsHandler
    {
        private readonly IGameFactory _gameFactory;

        public CoinsHandler(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void SeedSpace(int coinsAmount)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                for (int i = 0; i < coinsAmount; i++)
                {
                    SpawnCoin();
                } 
            }
        }

        private void SpawnCoin()
        {
            GameObject coin = _gameFactory.InstantiateCoin();
            coin.GetComponent<Coin>().Initialize(OnCoinDestroy);
        }

        private void OnCoinDestroy()
        {
            SpawnCoin();
        }
    }
}
