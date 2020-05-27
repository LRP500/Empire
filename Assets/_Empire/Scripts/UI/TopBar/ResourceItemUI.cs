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
        private Resource _resource = null;

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
            _amount.text = AbbreviationUtility.Format(_resource.Current, "0.00");
        }
    }
}
