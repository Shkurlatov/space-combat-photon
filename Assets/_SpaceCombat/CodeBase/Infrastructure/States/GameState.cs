using SpaceCombat.Gameplay.Combat;
using SpaceCombat.Gameplay.UI;
using SpaceCombat.Infrastructure.Configs;
using SpaceCombat.Infrastructure.Factory;
using UnityEngine;

namespace SpaceCombat.Infrastructure.States
{
    public class GameState : IState
    {
        private const string COMBAT_MANAGER = "CombatManager";

        private readonly IGameStateMachine _stateMachine;
        private readonly IDataProvider _dataProvider;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public GameState(
            IGameStateMachine stateMachine,
            IDataProvider dataProvider,
            IGameFactory gameFactory,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _dataProvider = dataProvider;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            InitUIRoot();
            InitCombatManager();
        }

        public void Exit()
        {

        }

        private void InitUIRoot()
        {
            GameObject uiRoot = _uiFactory.InstantiateUIRoot();
            uiRoot.GetComponent<UIRoot>().Initialize(_uiFactory);
        }

        private void InitCombatManager()
        {
            GameObject combatManager = GameObject.FindGameObjectWithTag(COMBAT_MANAGER);
            combatManager.GetComponent<CombatManager>().Initialize(_gameFactory, _dataProvider.GetCombatConfigs().CoinsAmount);
        }
    }
}