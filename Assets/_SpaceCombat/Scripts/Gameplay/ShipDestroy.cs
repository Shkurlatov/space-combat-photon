using Photon.Pun;
using UnityEngine;

namespace SpaceCombat.Gameplay
{
    public class ShipDestroy : MonoBehaviour
    {
        private PhotonView _photonView;

        private bool _isDestroyed;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void OnEnable()
        {
            _isDestroyed = false;
        }

        public void DestroyShip()
        {
            if (_isDestroyed) 
                return;

            _isDestroyed = true;

            if (_photonView.IsMine)
            {
                gameObject.GetComponent<PhotonView>().RPC("DestroySpaceship", RpcTarget.All);
            }
        }
    }
}