using SpaceCombat.Infrastructure.States;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour
    {
        public SceneLoader SceneLoaderPrefab;

        private Game _game;

        private void Awake()
        {
            _game = new Game(Instantiate(SceneLoaderPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}