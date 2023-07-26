using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace SpaceCombat.Gameplay.UI
{
    public class UIRoot : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject PopupPrefab;

        private bool hasWinner;

        private List<Player> players;

        public void Awake()
        {
            players = new List<Player>();

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                players.Add(player);
            }
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (!hasWinner)
            {
                int winnerNumber = (int)targetPlayer.CustomProperties[AsteroidsGame.WINNER_NUMBER];

                if (winnerNumber > 0)
                {
                    hasWinner = true;

                    ShowWinnerPopup(winnerNumber);
                }
            }
        }

        private void ShowWinnerPopup(int winnerNumber)
        {
            foreach (Player player in players)
            {
                if (player != null && player.ActorNumber == winnerNumber)
                {
                    int collectedCoins = player.GetScore();
                    Color textColor = AsteroidsGame.GetColor(winnerNumber - 1);
                    string colorName = AsteroidsGame.GetColorName(winnerNumber - 1);

                    string popupText = $"Winner - {colorName} Player!\n\nCollected {collectedCoins} Coins";

                    Popup popup = Instantiate(PopupPrefab, transform).GetComponent<Popup>();
                    popup.UpdateText(popupText, textColor);
                }
            }
        }
    }
}