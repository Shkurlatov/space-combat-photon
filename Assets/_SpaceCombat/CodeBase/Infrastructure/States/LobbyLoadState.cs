using SpaceCombat.Infrastructure.Bootstrap;
using SpaceCombat.Infrastructure.Factory;
using SpaceCombat.Infrastructure.GameConfigs;
using SpaceCombat.Lobby;
using UnityEngine;

namespace SpaceCombat.Infrastructure.States
{
    public class LobbyLoadState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IDataProvider _dataProvider;
        private readonly IUIFactory _uiFactory;

        public LobbyLoadState(
            IGameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IDataProvider gameStaticData,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _dataProvider = gameStaticData;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => 
            _sceneLoader.HideCurtain();

        private void OnLoaded()
        {
            InitLobbyMainPalel();

            _stateMachine.Enter<LobbyState>();
        }

        private void InitLobbyMainPalel()
        {
            LobbyMainPanel lobbyMainPanel = _uiFactory.CreateMainLobbyPanel().GetComponent<LobbyMainPanel>();
            lobbyMainPanel.Initialize(_stateMachine);
        }
    }
}