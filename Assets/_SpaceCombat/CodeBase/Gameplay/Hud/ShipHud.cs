using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using SpaceCombat.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceCombat.Gameplay.Hud
{
    public class ShipHud : MonoBehaviourPunCallbacks
    {
        public int MaxProtection = 10;

        [SerializeField] private Image _protectionImage;
        [SerializeField] private Text _coinsCounterText;

        private PhotonView _photonView;
        private int _actorNumber;

        private void Awake()
        {
            _photonView = GetComponentInParent<PhotonView>();
        }

        private void Start()
        {
            _actorNumber = _photonView.Owner.ActorNumber;
            _protectionImage.fillAmount = 1;
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (targetPlayer.ActorNumber == _actorNumber)
            {
                int currentProtection = (int)targetPlayer.CustomProperties[AsteroidsGame.SHIP_PROTECTION];
                int collectedCoins = targetPlayer.GetScore();

                _protectionImage.fillAmount = (float)currentProtection / MaxProtection;

                if (collectedCoins > 0)
                {
                    _coinsCounterText.text = collectedCoins.ToString();
                }

                if (currentProtection < 1)
                {
                    _protectionImage.enabled = false;
                    _coinsCounterText.enabled = false;
                }
            }
        }
    }
}