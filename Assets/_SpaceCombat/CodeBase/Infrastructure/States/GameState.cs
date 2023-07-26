using UnityEngine;

namespace SpaceCombat.Infrastructure.States
{
    public class GameState : IState
    {
        public GameState(IGameStateMachine stateMachine)
        {

        }

        public void Enter()
        {
            Debug.Log("I'm in");
        }

        public void Exit()
        {

        }
    }
}