﻿using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System;
using UnityEngine;

namespace SpaceCombat.Gameplay
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private PhotonView _photonView;

        private bool _isDestroyed;

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
                _spriteRenderer.enabled = false;

                if (_photonView.IsMine)
                {
                    other.GetComponent<PhotonView>().Owner.AddScore(1);

                    PhotonNetwork.Destroy(gameObject);

                    _onCoinDestroy?.Invoke();
                }
            }
        }
    }
}