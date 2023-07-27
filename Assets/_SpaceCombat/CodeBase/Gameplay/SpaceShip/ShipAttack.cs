using Photon.Pun;
using SpaceCombat.Gameplay.Combat;
using SpaceCombat.Infrastructure.Input;
using UnityEngine;

namespace SpaceCombat.Gameplay.Ship
{
    public class ShipAttack : MonoBehaviour
    {
        public float ReloadTime = 0.5f;

        private PhotonView _photonView;
        private IInputService _input;

        private float shootingTimer;

        public GameObject BulletPrefab;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public void Initialize(IInputService input)
        {
            _input = input;
        }

        public void Update()
        {
            if ((_input.IsAttackButtonUp() || Input.GetButton("Jump")) && shootingTimer <= 0.0)
            {
                shootingTimer = ReloadTime;

                _photonView.RPC("Fire", RpcTarget.AllViaServer, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation);
            }

            if (shootingTimer > 0.0f)
            {
                shootingTimer -= Time.deltaTime;
            }
        }
        
        [PunRPC]
        public void Fire(Vector3 position, Quaternion rotation, PhotonMessageInfo info)
        {
            float lag = (float) (PhotonNetwork.Time - info.SentServerTime);

            GameObject bullet = Instantiate(BulletPrefab, position, rotation);
            bullet.GetComponent<Bullet>().InitializeBullet(rotation * (Vector3.forward), Mathf.Abs(lag));
            bullet = Instantiate(BulletPrefab, position, rotation);
            bullet.GetComponent<Bullet>().InitializeBullet(rotation * (Vector3.forward + Vector3.right * 0.1f), Mathf.Abs(lag));
            bullet = Instantiate(BulletPrefab, position, rotation);
            bullet.GetComponent<Bullet>().InitializeBullet(rotation * (Vector3.forward + Vector3.left * 0.1f), Mathf.Abs(lag));
        }
    }
}