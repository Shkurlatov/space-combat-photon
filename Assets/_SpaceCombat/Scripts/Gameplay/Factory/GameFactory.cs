using Photon.Pun;
using UnityEngine;

namespace SpaceCombat.Gameplay.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly ScreenSize _screenSize;

        public GameFactory()
        {
            _screenSize = new ScreenSize(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
        }

        public void SpawnCoin()
        {
            float positionX = Random.Range(-_screenSize.HalfWidth, _screenSize.HalfWidth);
            float positionZ = Random.Range(-_screenSize.HalfHeight, _screenSize.HalfHeight);

            PhotonNetwork.Instantiate("Coin", new Vector3(positionX, 0, positionZ), Quaternion.identity, 0);
        }
    }
}
