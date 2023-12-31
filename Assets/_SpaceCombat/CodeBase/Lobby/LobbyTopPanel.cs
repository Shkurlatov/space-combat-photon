﻿using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceCombat.Lobby
{
    public class LobbyTopPanel : MonoBehaviour
    {
        private readonly string connectionStatusMessage = "    Connection Status: ";

        [Header("UI References")]
        public Text ConnectionStatusText;

        public void Update()
        {
            ConnectionStatusText.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
        }
    }
}