using SpaceCombat.Infrastructure.GameResources;
using SpaceCombat.Infrastructure.Configs;
using UnityEngine;
using Photon.Realtime;
using SpaceCombat.Gameplay.UI;
using SpaceCombat.Utilities;
using Photon.Pun.UtilityScripts;

namespace SpaceCombat.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IDataProvider _dataProvider;

        public UIFactory(IAssetProvider assetProvider, IDataProvider dataProveder)
        {
            _assetProvider = assetProvider;
            _dataProvider = dataProveder;
        }

        public GameObject InstantiateLobbyManager()
        {
            return _assetProvider.Instantiate(AssetPath.LOBBY_MANAGER_PATH);
        }

        public GameObject InstantiateUIRoot()
        {
            return _assetProvider.Instantiate(AssetPath.UI_ROOT_PATH);
        }

        public GameObject InstantiateControlHud()
        {
            return _assetProvider.Instantiate(AssetPath.CONTROL_HUD_PATH);
        }

        public void InstantiatePopup(Player winner, Transform parent)
        {
            int collectedCoins = winner.GetScore();
            Color textColor = GameConstants.GetColor(winner.ActorNumber - 1);
            string colorName = GameConstants.GetColorName(winner.ActorNumber - 1);
            string popupText = $"Winner - {colorName} Player !\n\nCollected  {collectedCoins}  Coins";

            Popup popup = _assetProvider.Instantiate(AssetPath.POPUP_PATH, parent).GetComponent<Popup>();
            popup.UpdateText(popupText, textColor);
        }
    }
}
