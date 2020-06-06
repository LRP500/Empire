using TMPro;
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
        private Resource _resource = null;

        [SerializeField]
        private float _incrementTextFadeOutTime = 1;

        private void Awake()
        {
            _logo.sprite = _resource.Type.Icon;
        }

        private void Update()
        {
            Refresh();   
        }

        private void Refresh()
        {
            _amount.text = AbbreviationUtility.Format(_resource.Current, "0.0");

            string increment = AbbreviationUtility.Format(_resource.Production, "0.0");
            _increment.text = increment.Insert(0, _resource.Production < 0 ? "-" : "+");
        }

        //private IEnumerator FadeOutIncrementText()
        //{
        //    SetIncrementTextAlpha(1);

        //    float timer = _incrementTextFadeOutTime;

        //    while (timer >= 0)
        //    {
        //        timer += Time.unscaledDeltaTime;
        //        yield return null;
        //    }

        //    SetIncrementTextAlpha(0);
        //}

        //private void SetIncrementTextAlpha(int alpha)
        //{
        //    _increment.color = new Color(_increment.color.r, _increment.color.g, _increment.color.b, alpha);
        //}
    }
}
