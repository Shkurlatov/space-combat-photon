﻿using Photon.Pun;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace SpaceCombat.Gameplay.Ship
{
    public class ShipProtection : MonoBehaviour
    {
        public int Points = 10; 

        private PhotonView _photonView;

        private bool _isDestroyed;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public void TakeDamage()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (_isDestroyed)
                    return;

                Points--;

                UpdateShipProtectionProperty();

                if (Points < 1)
                {
                    DestroyShip();
                }
            }
        }

        private void UpdateShipProtectionProperty()
        {
            _photonView.Owner.SetCustomProperties(
                new Hashtable 
                { 
                    { AsteroidsGame.SHIP_PROTECTION, Points } 
                });
        }

        private void DestroyShip()
        {
            _isDestroyed = true;

            gameObject.GetComponent<PhotonView>().RPC("DestroyShip", RpcTarget.All);
        }
    }
}