using SpaceCombat.Infrastructure.Services;
using SpaceCombat.Infrastructure.States;

namespace SpaceCombat.Infrastructure.Bootstrap
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(SceneLoader sceneLoader)
        {
            sceneLoader.ShowCurtain();
            StateMachine = new GameStateMachine(sceneLoader, new ServiceContainer());
        }
    }
}