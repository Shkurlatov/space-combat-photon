using SpaceCombat.Infrastructure.Bootstrap;
using SpaceCombat.Infrastructure.Factory;
using SpaceCombat.Infrastructure.Services;
using SpaceCombat.Infrastructure.GameConfigs;
using System;
using System.Collections.Generic;

namespace SpaceCombat.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, ServiceContainer services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LobbyLoadState)] = new LobbyLoadState(this, sceneLoader, services.Single<IDataProvider>(), services.Single<IUIFactory>()),
                [typeof(LobbyState)] = new LobbyState(this),
                [typeof(GameLoadState)] = new GameLoadState(this, sceneLoader, services.Single<IDataProvider>(), services.Single<IGameFactory>(), services.Single<IUIFactory>()),
                [typeof(GameState)] = new GameState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}