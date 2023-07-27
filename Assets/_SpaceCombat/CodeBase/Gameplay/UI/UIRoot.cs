using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using SpaceCombat.Infrastructure.Factory;
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

        private IUIFactory _uiFactory;

        private bool hasWinner;
        private GameObject _controlHud;

        public void Initialize(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

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

                _uiFactory.InstantiatePopup(alivePlayer, gameObject.transform);
            }
        }

        private void OnCountdownTimerIsExpired()
        {
            _controlHud = _uiFactory.InstantiateControlHud();
        }
    }
}