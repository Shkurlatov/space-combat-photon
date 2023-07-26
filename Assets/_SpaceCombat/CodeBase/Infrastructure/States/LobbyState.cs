namespace SpaceCombat.Infrastructure.States
{
    public class LobbyState : IState
    {
        private readonly IGameStateMachine _stateMachine;

        public LobbyState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}