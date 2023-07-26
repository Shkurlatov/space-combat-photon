using SpaceCombat.Infrastructure.GameResources;
using SpaceCombat.Infrastructure.Bootstrap;
using SpaceCombat.Infrastructure.Factory;
using SpaceCombat.Infrastructure.Services;
using SpaceCombat.Infrastructure.GameConfigs;

namespace SpaceCombat.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Bootstrap = "Loading";

        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceContainer _services;

        public BootstrapState(IGameStateMachine stateMachine, SceneLoader sceneLoader, ServiceContainer services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.Load(Bootstrap, onLoaded: EnterLoadLevel);

        public void Exit()
        {

        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<ConnectionState>();

        private void RegisterServices()
        {
            RegisterAssetProvider();
            RegisterGameStaticData();
            RegisterGameFactory();
        }

        private void RegisterAssetProvider()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
        }

        private void RegisterGameStaticData()
        {
            IDataProvider gameStaticData = new DataProvider();

            _services.RegisterSingle(gameStaticData);
        }

        private void RegisterGameFactory()
        {
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                            _services.Single<IAssetProvider>(),
                            _services.Single<IDataProvider>()));
        }
    }
}