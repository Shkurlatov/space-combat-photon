using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceCombat.Infrastructure.Bootstrap
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private Image _progressImage;

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void FillProgress(float progress)
        {
            _progressImage.fillAmount = progress;
        }

        public void Hide() =>
            StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= 0.09f;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}
