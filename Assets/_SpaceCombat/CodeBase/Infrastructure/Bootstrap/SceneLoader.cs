using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceCombat.Infrastructure.Bootstrap
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;

        private void Awake() =>
            DontDestroyOnLoad(this);

        public void ShowCurtain() =>
            _loadingCurtain.Show();

        public void Load(string name, Action onLoaded = null) =>
            StartCoroutine(LoadScene(name, onLoaded));

        public void HideCurtain() =>
            _loadingCurtain.Hide();

        private IEnumerator LoadScene(string nextScene, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}