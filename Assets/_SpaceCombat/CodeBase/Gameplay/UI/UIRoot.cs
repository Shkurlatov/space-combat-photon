using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using SpaceCombat.Utilities;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace SpaceCombat.Gameplay.UI
{
    public class UIRoot : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject ControlHudPrefab;
        [SerializeField] private GameObject PopupPrefab;

        private bool hasWinner;
        private GameObject _controlHud;

        public override void OnEnable()
        {
            base.OnEnable();

            CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (hasWinner)
            {
                return;
            }

            int shipProtection = (int)targetPlayer.CustomProperties[AsteroidsGame.SHIP_PROTECTION];

            if (shipProtection <= 0)
            {
                if (targetPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    _controlHud.SetActive(false);
                }

                CheckWinner();
            }
        }

        private void CheckWinner()
        {
            List<int> alivePlayerNumbers = new List<int>();
            Player alivePlayer = null;

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                int shipProtection = (int)player.CustomProperties[AsteroidsGame.SHIP_PROTECTION];

                if (shipProtection > 0)
                {
                    alivePlayerNumbers.Add(player.ActorNumber);
                    alivePlayer = player;
                }
            }

            if (alivePlayerNumbers.Count == 1)
            {
                hasWinner = true;

                ShowWinnerPopup(alivePlayer);
            }
        }

        private void ShowWinnerPopup(Player winner)
        {
            int collectedCoins = winner.GetScore();
            Color textColor = AsteroidsGame.GetColor(winner.ActorNumber - 1);
            string colorName = AsteroidsGame.GetColorName(winner.ActorNumber - 1);

            string popupText = $"Winner - {colorName} Player !\n\nCollected  {collectedCoins}  Coins";

            Popup popup = Instantiate(PopupPrefab, transform).GetComponent<Popup>();
            popup.UpdateText(popupText, textColor);
        }

        private void OnCountdownTimerIsExpired()
        {
            _controlHud = Instantiate(ControlHudPrefab);
        }
    }
}