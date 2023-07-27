using UnityEngine;

namespace SpaceCombat.Infrastructure.Bootstrap
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper GameBootstrapperPrefab;

        private void Awake()
        {
            GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper == null)
            {
                Instantiate(GameBootstrapperPrefab);
            }
        }
    }
}