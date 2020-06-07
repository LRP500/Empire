using System.Collections;
using TMPro;
using Tools.Time;
using Tools.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class ResourceItemUI : MonoBehaviour
    {
        [SerializeField]
        private Image _logo = null;

        [SerializeField]
        private TextMeshProUGUI _amount = null;

        [SerializeField]
        private TextMeshProUGUI _increment = null;

        [SerializeField]
        private CanvasGroup _incrementGroup = null;

        [SerializeField]
        private TimeController _timeController = null;

        [SerializeField]
        private Resource _resource = null;

        [SerializeField]
        private float _incrementTextFadeOutTime = 1;

        private void Awake()
        {
            _logo.sprite = _resource.Type.Icon;
            _resource.RegisterOnCurrentValueChanged(Refresh);
            _resource.RegisterOnProductionValueChanged(RefreshIncrement);
        }

        private void OnDestroy()
        {
            _resource.UnregisterOnCurrentValueChanged(Refresh);
            _resource.UnregisterOnProductionValueChanged(RefreshIncrement);
        }

        private void Refresh(int currentValue)
        {
            _amount.text = AbbreviationUtility.Format(currentValue, "0.0");
        }

        private void RefreshIncrement()
        {
            string increment = AbbreviationUtility.Format(_resource.Production, "0.0");
            _increment.text = increment.Insert(0, _resource.Production < 0 ? "-" : "+");

            //StopAllCoroutines();
            //StartCoroutine(FadeOutIncrementText());
        }

        private IEnumerator FadeOutIncrementText()
        {
            _incrementGroup.alpha = 1;

            float timer = _incrementTextFadeOutTime;

            while (timer >= 0)
            {
                timer += Time.unscaledDeltaTime;
                _incrementGroup.alpha -= 1 / _incrementTextFadeOutTime * Time.deltaTime;
                yield return null;
            }

            _incrementGroup.alpha = 0;
        }
    }
}
