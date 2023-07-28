using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using SpaceCombat.Infrastructure.Factory;
using SpaceCombat.Utilities;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace SpaceCombat.Gameplay.Combat
{
    public class CombatManager : MonoBehaviourPunCallbacks
    {
        public static CombatManager Instance = null;

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

        public void Initialize(IGameFactory gameFactory, int coinsAmount)
        {
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
            _gameFactory.InstantiateSpaceShip(PhotonNetwork.CurrentRoom.PlayerCount, PhotonNetwork.LocalPlayer.GetPlayerNumber());
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