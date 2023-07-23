using System.Collections;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;

namespace SpaceCombat.Gameplay.Ship
{
    public class Spaceship : MonoBehaviour
    {
        public float RotationSpeed = 90.0f;
        public float MovementSpeed = 2.0f;
        public float MaxSpeed = 0.2f;

        public ParticleSystem Destruction;
        public GameObject EngineTrail;
        //public GameObject BulletPrefab;

        private PhotonView _photonView;

#pragma warning disable 0109
        //private new Rigidbody rigidbody;
        private new Collider collider;
        private new Renderer renderer;
#pragma warning restore 0109

        //private float rotation = 0.0f;
        //private float acceleration = 0.0f;
        //private float shootingTimer = 0.0f;

        //private bool controllable = true;

        #region UNITY

        public void Awake()
        {
            _photonView = GetComponent<PhotonView>();

            //rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
            renderer = GetComponent<Renderer>();
        }

        public void Start()
        {
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = AsteroidsGame.GetColor(_photonView.Owner.GetPlayerNumber());
            }
        }

        #endregion

        #region COROUTINES

        private IEnumerator WaitForRespawn()
        {
            yield return new WaitForSeconds(AsteroidsGame.PLAYER_RESPAWN_TIME);

            _photonView.RPC("RespawnSpaceship", RpcTarget.AllViaServer);
        }

        #endregion

        #region PUN CALLBACKS

        [PunRPC]
        public void DestroySpaceship()
        {
            //rigidbody.velocity = Vector3.zero;
            //rigidbody.angularVelocity = Vector3.zero;

            collider.enabled = false;
            renderer.enabled = false;

            //controllable = false;

            EngineTrail.SetActive(false);
            Destruction.Play();

            if (_photonView.IsMine)
            {
                object lives;
                if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(AsteroidsGame.PLAYER_LIVES, out lives))
                {
                    PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { AsteroidsGame.PLAYER_LIVES, ((int)lives <= 1) ? 0 : ((int)lives - 1) } });

                    if (((int)lives) > 1)
                    {
                        StartCoroutine("WaitForRespawn");
                    }
                }
            }
        }

        [PunRPC]
        public void RespawnSpaceship()
        {
            collider.enabled = true;
            renderer.enabled = true;

            //controllable = true;

            EngineTrail.SetActive(true);
            Destruction.Stop();
        }

        #endregion
    }
}