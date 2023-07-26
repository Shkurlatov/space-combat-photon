using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

namespace SpaceCombat.Gameplay.Ship
{
    public class SpaceShip : MonoBehaviour
    {
        public float RotationSpeed = 30.0f;
        public float MovementSpeed = 20.0f;

        public ParticleSystem Destruction;
        public GameObject EngineTrail;

        private PhotonView _photonView;
        private Collider _collider;
        private Renderer _renderer;

        public void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _collider = GetComponent<Collider>();
            _renderer = GetComponent<Renderer>();
        }

        public void Start()
        {
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = AsteroidsGame.GetColor(_photonView.Owner.GetPlayerNumber());
            }
        }

        [PunRPC]
        public void DestroyShip()
        {
            _collider.enabled = false;
            _renderer.enabled = false;

            EngineTrail.SetActive(false);
            Destruction.Play();
        }
    }
}