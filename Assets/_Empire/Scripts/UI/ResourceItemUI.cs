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
            _logo.sprite = _resource.type.Icon;
            _amount.text = _resource.current.ToString();
        }
    }
}
