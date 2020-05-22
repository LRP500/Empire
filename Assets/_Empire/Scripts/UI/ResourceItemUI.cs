using TMPro;
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
            _amount.text = _resource.Current.ToString();
        }

        private void Update()
        {
            Refresh();   
        }

        private void Refresh()
        {
            _amount.text = _resource.Current.ToString();
        }
    }
}
