using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using UnityEngine;

namespace SpaceCombat.Gameplay.Ship
{
    public class LookAtCamera
    {

    }
    public class Spaceship : MonoBehaviour
    {
        public float RotationSpeed = 30.0f;
        public float MovementSpeed = 20.0f;

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

        #region PUN CALLBACKS

        [PunRPC]
        public void DestroySpaceship()
        {
            collider.enabled = false;
            renderer.enabled = false;

            //controllable = false;

            EngineTrail.SetActive(false);
            Destruction.Play();

            if (_photonView.IsMine)
            {
                StartCoroutine(DestroyAfterDestruction());
            }
        }

        private IEnumerator DestroyAfterDestruction()
        {
            yield return new WaitForSeconds(1f);

            PhotonNetwork.Destroy(gameObject);
        }

        #endregion
    }
}