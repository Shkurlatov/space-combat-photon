using Photon.Pun;
using SpaceCombat.Gameplay.Factory;

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
                _gameFactory.SpawnCoin();
            }
        }

        [PunRPC]
        public void RespawnCoin()
        {
            _gameFactory.SpawnCoin();
        }
    }
}
