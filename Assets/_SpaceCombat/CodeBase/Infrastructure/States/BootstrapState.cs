using SpaceCombat.Infrastructure.Bootstrap;
using SpaceCombat.Infrastructure.Factory;
using SpaceCombat.Infrastructure.GameConfigs;
using SpaceCombat.Infrastructure.GameResources;
using SpaceCombat.Infrastructure.Input;
using SpaceCombat.Infrastructure.Services;
using System;

namespace SpaceCombat.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string LOADING = "Loading";

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
            _sceneLoader.Load(LOADING, onLoaded: EnterLoadLevel);

        public void Exit()
        {

        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LobbyLoadState, string>("Lobby");
        }

        private void RegisterServices()
        {
            RegisterAssetProvider();
            RegisterDataProvider();
            RegisterGameFactory();
            RegisterUIFactory();
            RegisterInputService();
        }

        private void RegisterAssetProvider()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
        }

        private void RegisterDataProvider()
        {
            IDataProvider dataProvider = new DataProvider();

            _services.RegisterSingle(dataProvider);
        }

        private void RegisterGameFactory()
        {
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                            _services.Single<IAssetProvider>(),
                            _services.Single<IDataProvider>()));
        }

        private void RegisterUIFactory()
        {
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IDataProvider>()));
        }

        private void RegisterInputService()
        {
            _services.RegisterSingle<IInputService>(new MobileInputService());
        }
    }
}