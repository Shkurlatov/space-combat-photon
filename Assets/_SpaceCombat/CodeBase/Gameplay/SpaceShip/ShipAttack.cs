using Photon.Pun;
using SpaceCombat.Gameplay.Combat;
using SpaceCombat.Infrastructure.GameResources;
using SpaceCombat.Infrastructure.Input;
using UnityEngine;

namespace SpaceCombat.Gameplay.Ship
{
    public class ShipAttack : MonoBehaviour
    {
        private const string JUMP = "Jump";
        private const string FIRE = "Fire";

        private PhotonView _photonView;

        private IInputService _input;
        private float _reloadTime;
        private string _bulletPath;

        private float _shootingTimer;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _bulletPath = AssetPath.BULLET_PATH;
        }

        public void Initialize(IInputService input, float reloadTime)
        {
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
            float lag = (float)(PhotonNetwork.Time - info.SentServerTime);

            // Bullet factory need to be implemented

            GameObject bullet = Instantiate(Resources.Load<GameObject>(_bulletPath), position, rotation);
            bullet.GetComponent<Bullet>().Initialize(rotation * (Vector3.forward), Mathf.Abs(lag));

            bullet = Instantiate(Resources.Load<GameObject>(_bulletPath), position, rotation);
            bullet.GetComponent<Bullet>().Initialize(rotation * (Vector3.forward + Vector3.right * 0.1f), Mathf.Abs(lag));

            bullet = Instantiate(Resources.Load<GameObject>(_bulletPath), position, rotation);
            bullet.GetComponent<Bullet>().Initialize(rotation * (Vector3.forward + Vector3.left * 0.1f), Mathf.Abs(lag));
        }
    }
}