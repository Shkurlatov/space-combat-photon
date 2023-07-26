using Photon.Pun;
using SpaceCombat.Infrastructure.GameConfigs;
using SpaceCombat.Infrastructure.GameResources;
using SpaceCombat.Utilities;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly ScreenSize _screenSize;

        private readonly IAssetProvider _assetProvider;
        private readonly IDataProvider _gameStaticData;

        public GameFactory(
            IAssetProvider assetProvider,
            IDataProvider gameStaticData)
        {
            _assetProvider = assetProvider;
            _gameStaticData = gameStaticData;

            _screenSize = new ScreenSize(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
        }

        public GameObject SpawnCoin()
        {
            float positionX = Random.Range(-_screenSize.HalfWidth, _screenSize.HalfWidth);
            float positionZ = Random.Range(-_screenSize.HalfHeight, _screenSize.HalfHeight);

            return PhotonNetwork.InstantiateRoomObject("Game/Coin", new Vector3(positionX, 0, positionZ), Quaternion.identity, 0);
        }

        public void Cleanup()
        {

        }
    }
}
