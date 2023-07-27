using Photon.Pun;
using SpaceCombat.Infrastructure.Configs;
using SpaceCombat.Infrastructure.GameResources;
using SpaceCombat.Utilities;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IDataProvider _dataProvider;

        private SpaceSize _screenSize;

        public GameFactory(IAssetProvider assetProvider, IDataProvider dataProvider)
        {
            _assetProvider = assetProvider;
            _dataProvider = dataProvider;
        }

        public void SetScreenSize()
        {
            _screenSize = new SpaceSize(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
        }

        public GameObject InstantiateSpaceShip()
        {
            return PhotonNetwork.InstantiateRoomObject(AssetPath.SPACE_SHIP_PATH, Vector3.zero, Quaternion.identity, 0);
        }

        public GameObject InstantiateCoin()
        {
            float positionX = Random.Range(-_screenSize.HalfWidth, _screenSize.HalfWidth);
            float positionZ = Random.Range(-_screenSize.HalfHeight, _screenSize.HalfHeight);

            return PhotonNetwork.InstantiateRoomObject(AssetPath.COIN_PATH, new Vector3(positionX, 0, positionZ), Quaternion.identity, 0);
        }

        public GameObject InstantiateBullet()
        {
            return _assetProvider.Instantiate(AssetPath.BULLET_PATH);
        }

        public void Cleanup()
        {

        }
    }
}
