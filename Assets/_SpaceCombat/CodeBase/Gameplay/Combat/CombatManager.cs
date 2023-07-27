using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using SpaceCombat.Gameplay.Ship;
using SpaceCombat.Infrastructure.Factory;
using SpaceCombat.Infrastructure.Input;
using SpaceCombat.Utilities;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace SpaceCombat.Gameplay.Combat
{
    public class CombatManager : MonoBehaviourPunCallbacks
    {
        public static CombatManager Instance = null;

        private IInputService _input;
        private IGameFactory _gameFactory;
        private CoinsHandler _coinsHandler;

        public void Awake()
        {
            Instance = this;
        }

        public override void OnEnable()
        {
            base.OnEnable();

            CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
        }

        public void Initialize(IInputService input, IGameFactory gameFactory, int coinsAmount)
        {
            _input = input;
            _gameFactory = gameFactory;
            _gameFactory.SetScreenSize();

            _coinsHandler = new CoinsHandler(_gameFactory);
            _coinsHandler.SeedSpace(coinsAmount);

            Hashtable props = new Hashtable
            {
                {AsteroidsGame.PLAYER_LOADED_LEVEL, true}
            };

            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            int startTimestamp;
            bool startTimeIsSet = CountdownTimer.TryGetStartTime(out startTimestamp);

            if (changedProps.ContainsKey(AsteroidsGame.PLAYER_LOADED_LEVEL))
            {
                if (CheckAllPlayerLoadedLevel())
                {
                    if (!startTimeIsSet)
                    {
                        CountdownTimer.SetStartTime();
                    }
                }
            }
        }

        private void OnCountdownTimerIsExpired()
        {
            StartCombat();
        }

        private void StartCombat()
        {
            SpaceSize screenSize = new SpaceSize(
                Camera.main.orthographicSize * Camera.main.aspect,
                Camera.main.orthographicSize
            );

            float angularStart = (360.0f / PhotonNetwork.CurrentRoom.PlayerCount) * PhotonNetwork.LocalPlayer.GetPlayerNumber();
            float x = 20.0f * Mathf.Sin(angularStart * Mathf.Deg2Rad);
            float z = 20.0f * Mathf.Cos(angularStart * Mathf.Deg2Rad);
            Vector3 position = new Vector3(x, 0.0f, z);
            Quaternion rotation = Quaternion.Euler(0.0f, angularStart, 0.0f);

            GameObject spaceShip = PhotonNetwork.Instantiate("Combat/SpaceShip", position, rotation, 0);      // avoid this call on rejoin (ship was network instantiated before)

            spaceShip.GetComponent<ShipMovement>().Initialize(_input, screenSize);
            spaceShip.GetComponent<ShipMovement>().enabled = true;

            spaceShip.GetComponent<ShipAttack>().Initialize(_input);
            spaceShip.GetComponent<ShipAttack>().enabled = true;
        }

        private bool CheckAllPlayerLoadedLevel()
        {
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                object playerLoadedLevel;

                if (p.CustomProperties.TryGetValue(AsteroidsGame.PLAYER_LOADED_LEVEL, out playerLoadedLevel))
                {
                    if ((bool)playerLoadedLevel)
                    {
                        continue;
                    }
                }

                return false;
            }

            return true;
        }
    }
}