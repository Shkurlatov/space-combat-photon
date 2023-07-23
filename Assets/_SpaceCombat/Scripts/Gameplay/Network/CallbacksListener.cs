using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

namespace SpaceCombat.Gameplay.Network
{

    public class CallbacksListener : MonoBehaviourPunCallbacks
    {
        private PhotonView _photonView;
        private int _ownerNumber;
        private int _ownerScore;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _ownerNumber = _photonView.OwnerActorNr;
            _ownerScore = 0;
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (targetPlayer.ActorNumber == _ownerNumber && _ownerScore != targetPlayer.GetScore())
            {
                Debug.Log("targetPlayer == _owner");
                _ownerScore = targetPlayer.GetScore();
                _photonView.RPC("RespawnCoin", RpcTarget.MasterClient);
            }
        }
    }
}
