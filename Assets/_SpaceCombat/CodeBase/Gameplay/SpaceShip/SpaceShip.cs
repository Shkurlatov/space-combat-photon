using Photon.Pun;
using Photon.Pun.UtilityScripts;
using SpaceCombat.Utilities;
using UnityEngine;

namespace SpaceCombat.Gameplay.Ship
{
    public class SpaceShip : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _destruction;
        [SerializeField] private GameObject _engineTrail;

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
                renderer.material.color = GameConstants.GetColor(_photonView.Owner.GetPlayerNumber());
            }
        }

        [PunRPC]
        public void DestroyShip()
        {
            _collider.enabled = false;
            _renderer.enabled = false;

            _engineTrail.SetActive(false);
            _destruction.Play();
        }
    }
}