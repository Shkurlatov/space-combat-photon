using Photon.Pun;
using UnityEngine;

namespace SpaceCombat.Gameplay.Network
{
    public class CoinsHandler : MonoBehaviourPun
    {
        public void Initialize()
        {

        }

        [PunRPC]
        public void RespawnCoin()
        {
            Debug.Log("RespawnCoin");
        }
    }
}
