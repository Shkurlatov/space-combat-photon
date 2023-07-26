using SpaceCombat.Infrastructure.GameResources;
using SpaceCombat.Infrastructure.GameConfigs;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IDataProvider _staticData;
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IDataProvider staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        public void CreatePopup()
        {
            //var config = _staticData.ForWindow(WindowId.Shop);
            //WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
            //window.Construct(_progressService);
        }

        public GameObject CreateMainLobbyPanel()
        {
            return _assetProvider.Instantiate(AssetPath.LOBBY_MAIN_PANEL);
        }

        public void CreateUIRoot()
        {
            _uiRoot = _assetProvider.Instantiate(AssetPath.UI_ROOT).transform;
        }
    }
}
