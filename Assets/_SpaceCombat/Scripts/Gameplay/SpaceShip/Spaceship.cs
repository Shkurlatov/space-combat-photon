using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

namespace SpaceCombat.Gameplay.Ship
{
    public class LookAtCamera
    {

    }
    public class SpaceShip : MonoBehaviour
    {
        public float RotationSpeed = 30.0f;
        public float MovementSpeed = 20.0f;

        public ParticleSystem Destruction;
        public GameObject EngineTrail;

        public bool IsDestroyed;

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
        public void DestroyShip()
        {
            IsDestroyed = true;

            collider.enabled = false;
            renderer.enabled = false;

            //controllable = false;

            EngineTrail.SetActive(false);
            Destruction.Play();

            CheckWinner();
        }

        private void CheckWinner()
        {
            int aliveShipsCount = 0;
            int actorNumber = 0;

            foreach (SpaceShip ship in FindObjectsOfType<SpaceShip>())
            {
                if (!ship.IsDestroyed)
                {
                    aliveShipsCount++;
                    actorNumber = ship.gameObject.GetPhotonView().Owner.ActorNumber;
                }
            }

            if (aliveShipsCount == 1)
            {
                Debug.Log("WINNER PLAYER - " + actorNumber);
            }
        }

        #endregion
    }
}