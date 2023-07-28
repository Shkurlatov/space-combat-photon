using UnityEngine.SceneManagement;

namespace SpaceCombat.Infrastructure.States
{
    public class LobbyState : IState
    {
        private const string GAME = "Game";

        private readonly IGameStateMachine _stateMachine;

        public LobbyState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        public void Exit()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene current, Scene next)
        {
            if (next.name == GAME)
            {
                _stateMachine.Enter<GameState>();
            }
        }
    }
}