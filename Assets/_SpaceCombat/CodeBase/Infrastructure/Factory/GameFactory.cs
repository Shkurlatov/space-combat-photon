using SpaceCombat.Gameplay.Combat;
using SpaceCombat.Gameplay.Hud;
using SpaceCombat.Gameplay.Ship;
using SpaceCombat.Infrastructure.Configs;
using SpaceCombat.Infrastructure.GameResources;
using SpaceCombat.Infrastructure.Input;
using SpaceCombat.Utilities;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IInputService _input;
        private readonly IAssetProvider _assetProvider;
        private readonly IDataProvider _dataProvider;

        private SpaceSize _screenSize;

        public GameFactory(IInputService input, IAssetProvider assetProvider, IDataProvider dataProvider)
        {
            _input = input;
            _assetProvider = assetProvider;
            _dataProvider = dataProvider;
        }

        public void SetScreenSize()
        {
            _screenSize = new SpaceSize(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
        }

        public void InstantiateSpaceShip(int playerCount, int playerNumber)
        {
            float angularStart = (360.0f / playerCount) * playerNumber;
            float positionX = 20.0f * Mathf.Sin(angularStart * Mathf.Deg2Rad);
            float positionZ = 20.0f * Mathf.Cos(angularStart * Mathf.Deg2Rad);

            Vector3 position = new Vector3(positionX, 0.0f, positionZ);
            Quaternion rotation = Quaternion.Euler(0.0f, angularStart, 0.0f);

            SpaceShipConfigs shipConfigs = _dataProvider.GetSpaceShipConfigs();

            GameObject spaceShip = _assetProvider.InstantiateGlobal(AssetPath.SPACE_SHIP_PATH, position, rotation);

            //spaceShip.GetComponent<ShipProtection>().Initialize(shipConfigs.ProtectionPoints);

            //spaceShip.GetComponent<ShipHud>().Initialize(shipConfigs.ProtectionPoints);
            //spaceShip.GetComponent<ShipHud>().enabled = true;

            spaceShip.GetComponent<ShipMovement>().Initialize(_input, _screenSize, shipConfigs.RotationSpeed, shipConfigs.MovementSpeed);
            spaceShip.GetComponent<ShipMovement>().enabled = true;

            spaceShip.GetComponent<ShipAttack>().Initialize(this, _input, shipConfigs.ReloadTime);
            spaceShip.GetComponent<ShipAttack>().enabled = true;
        }

        public GameObject InstantiateCoin()
        {
            float positionX = Random.Range(-_screenSize.HalfWidth, _screenSize.HalfWidth);
            float positionZ = Random.Range(-_screenSize.HalfHeight, _screenSize.HalfHeight);

            return _assetProvider.InstantiateGlobal(AssetPath.COIN_PATH, new Vector3(positionX, 0, positionZ));
        }

        public void InstantiateBullets(Vector3 position, Quaternion rotation, float lag)
        {
            CombatConfigs combatConfigs = _dataProvider.GetCombatConfigs();

            GameObject bullet1 = _assetProvider.Instantiate(AssetPath.BULLET_PATH, position, rotation);
            bullet1.GetComponent<Bullet>().Initialize(
                rotation * (Vector3.forward), combatConfigs.BulletSpeed, Mathf.Abs(lag), combatConfigs.BulletDestroyTime);

            GameObject bullet2 = _assetProvider.Instantiate(AssetPath.BULLET_PATH, position, rotation);
            bullet2.GetComponent<Bullet>().Initialize(
                rotation * (Vector3.forward + Vector3.right * 0.1f), combatConfigs.BulletSpeed, Mathf.Abs(lag), combatConfigs.BulletDestroyTime);

            GameObject bullet3 = _assetProvider.Instantiate(AssetPath.BULLET_PATH, position, rotation);
            bullet3.GetComponent<Bullet>().Initialize(
                rotation * (Vector3.forward + Vector3.left * 0.1f), combatConfigs.BulletSpeed, Mathf.Abs(lag), combatConfigs.BulletDestroyTime);
        }

        public void Cleanup()
        {

        }
    }
}
