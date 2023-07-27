using SpaceCombat.Infrastructure.GameResources;
using SpaceCombat.Infrastructure.Configs;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IDataProvider _dataProvider;

        public UIFactory(IAssetProvider assetProvider, IDataProvider staticData)
        {
            _assetProvider = assetProvider;
            _dataProvider = staticData;
        }

        public GameObject InstantiateLobbyManager()
        {
            return _assetProvider.Instantiate(AssetPath.LOBBY_MANAGER_PATH);
        }

        public void InstantiateUIRoot()
        {
            _assetProvider.Instantiate(AssetPath.UI_ROOT_PATH);
        }

        public void InstantiatePopup()
        {
            //var config = _staticData.ForWindow(WindowId.Shop);
            //WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
            //window.Construct(_progressService);
        }
    }
}
