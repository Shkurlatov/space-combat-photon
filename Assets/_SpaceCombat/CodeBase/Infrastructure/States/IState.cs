namespace SpaceCombat.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}