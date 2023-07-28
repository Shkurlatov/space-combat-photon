using SpaceCombat.Infrastructure.Bootstrap;
using SpaceCombat.Infrastructure.Configs;
using SpaceCombat.Infrastructure.Factory;
using SpaceCombat.Lobby;

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
            IDataProvider dataProvider,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _dataProvider = dataProvider;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _sceneLoader.HideCurtain();
        }

        private void OnLoaded()
        {
            InitLobbyManager();

            _stateMachine.Enter<LobbyState>();
        }

        private void InitLobbyManager()
        {
            _uiFactory.InstantiateLobbyManager().GetComponent<LobbyManager>();
        }
    }
}