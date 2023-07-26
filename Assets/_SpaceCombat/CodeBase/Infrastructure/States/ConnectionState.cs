namespace SpaceCombat.Infrastructure.States
{
    public class ConnectionState : IState
    {
        private readonly IGameStateMachine _stateMachine;

        public ConnectionState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _stateMachine.Enter<LoadSceneState, string>("Lobby");
        }

        public void Exit()
        {

        }

        private void LoadProgressOrInitNew()
        {

        }
    }
}