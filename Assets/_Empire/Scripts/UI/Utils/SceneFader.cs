using System.Collections;
using Tools.UI;
using UnityEngine;

namespace Empire
{
    public class SceneFader : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup = null;

        [SerializeField]
        private float _fadeDuration = 1f;

        public IEnumerator FadeOut()
        {
            yield return StartCoroutine(FadeEffect.FadeCanvas(_canvasGroup, 0, 1, _fadeDuration, true));
        }

        public IEnumerator FadeIn()
        {
            yield return StartCoroutine(FadeEffect.FadeCanvas(_canvasGroup, 1, 0, _fadeDuration, true));
        }
    }
}
