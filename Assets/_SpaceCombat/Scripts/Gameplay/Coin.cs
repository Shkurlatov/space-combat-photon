using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System;
using UnityEngine;

namespace SpaceCombat.Gameplay
{
    public class Coin : MonoBehaviour
    {
        private bool _isDestroyed;

        private PhotonView _photonView;

        private Action _onCoinDestroy;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public void Initialize(Action onCoinDestroy)
        {
            _onCoinDestroy = onCoinDestroy;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isDestroyed)
            {
                return;
            }

            if (other.gameObject.CompareTag("SpaceShip"))
            {
                _isDestroyed = true;

                if (_photonView.IsMine)
                {
                    other.GetComponent<PhotonView>().Owner.AddScore(1);

                    PhotonNetwork.Destroy(gameObject);

                    _onCoinDestroy?.Invoke();
                }
                else
                {
                    GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }
}