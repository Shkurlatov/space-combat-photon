using SpaceCombat.Infrastructure.Bootstrap;
using SpaceCombat.Infrastructure.Factory;
using SpaceCombat.Infrastructure.GameConfigs;
using System.Threading.Tasks;

namespace SpaceCombat.Infrastructure.States
{
    public class GameLoadState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IDataProvider _gameStaticData;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public GameLoadState(
            IGameStateMachine stateMachine,
            SceneLoader sceneLoader,
            IDataProvider gameStaticData,
            IGameFactory gameFactory,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameStaticData = gameStaticData;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.ShowCurtain();
            _gameFactory.Cleanup();
            _sceneLoader.Await(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _sceneLoader.HideCurtain();
        }

        private void OnLoaded()
        {
            InitUIRoot();
            InitGameWorld();

            _stateMachine.Enter<GameState>();
        }

        private void InitUIRoot()
        {

        }

        private void InitGameWorld()
        {

        }
    }
}