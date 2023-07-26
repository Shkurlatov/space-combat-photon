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

            _loadingCurtain.FillProgress(0);

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
            {
                yield return new WaitForSeconds(0.1f);

                _loadingCurtain.FillProgress(Mathf.Clamp01(waitNextScene.progress / 0.9f));

                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}