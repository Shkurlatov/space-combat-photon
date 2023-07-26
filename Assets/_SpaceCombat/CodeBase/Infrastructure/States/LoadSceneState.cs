using SpaceCombat.Infrastructure.Bootstrap;
using SpaceCombat.Infrastructure.Factory;
using SpaceCombat.Infrastructure.GameConfigs;
using System.Threading.Tasks;

namespace SpaceCombat.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IDataProvider _gameStaticData;
        private readonly IGameFactory _gameFactory;

        public LoadSceneState(
            IGameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IDataProvider gameStaticData,
            IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameStaticData = gameStaticData;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.ShowCurtain();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => 
            _sceneLoader.HideCurtain();

        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLobbyState>();
        }

        private async Task InitUIRoot()
        {
            await Task.Delay(100);
        }

        private async Task InitGameWorld()
        {
            await Task.Delay(100);
        }

        private void InformProgressReaders()
        {

        }
    }
}