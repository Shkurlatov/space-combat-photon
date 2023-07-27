using SpaceCombat.Gameplay.Combat;
using SpaceCombat.Infrastructure.Bootstrap;
using SpaceCombat.Infrastructure.Factory;
using SpaceCombat.Infrastructure.Configs;
using UnityEngine;

namespace SpaceCombat.Infrastructure.States
{
    public class GameLoadState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IDataProvider _dataProvider;
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
            _dataProvider = gameStaticData;
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
            InitCombatManager();

            _stateMachine.Enter<GameState>();
        }

        private void InitUIRoot()
        {
            _uiFactory.InstantiateUIRoot();
        }

        private void InitCombatManager()
        {
            GameObject combatManager = _gameFactory.InstantiateCombatManager();
            combatManager.GetComponent<CombatManager>().Initialize(_gameFactory, _dataProvider.GetCombatConfigs().CoinsAmount);
        }
    }
}