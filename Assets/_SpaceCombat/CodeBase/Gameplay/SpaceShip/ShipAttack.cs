using Photon.Pun;
using SpaceCombat.Infrastructure.Factory;
using SpaceCombat.Infrastructure.Input;
using UnityEngine;

namespace SpaceCombat.Gameplay.Ship
{
    public class ShipAttack : MonoBehaviour
    {
        private const string JUMP = "Jump";
        private const string FIRE = "Fire";

        private PhotonView _photonView;

        private IGameFactory _gameFactory;
        private IInputService _input;
        private float _reloadTime;

        private float _shootingTimer;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public void Initialize(IGameFactory gameFactory, IInputService input, float reloadTime)
        {
            _gameFactory = gameFactory;
            _input = input;
            _reloadTime = reloadTime;
        }

        public void Update()
        {
            if ((_input.IsAttackButtonUp() || Input.GetButton(JUMP)) && _shootingTimer <= 0.0)
            {
                _shootingTimer = _reloadTime;

                _photonView.RPC(FIRE, RpcTarget.AllViaServer, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation);
            }

            if (_shootingTimer > 0.0f)
            {
                _shootingTimer -= Time.deltaTime;
            }
        }
        
        [PunRPC]
        public void Fire(Vector3 position, Quaternion rotation, PhotonMessageInfo info)
        {
            float lag = (float) (PhotonNetwork.Time - info.SentServerTime);

            _gameFactory.InstantiateBullets(position, rotation, lag);
        }
    }
}