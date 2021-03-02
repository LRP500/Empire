using TMPro;
using Tools.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Empire
{
    public class ResourceItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image _logo;

        [SerializeField]
        private TextMeshProUGUI _amount;

        [SerializeField]
        private TextMeshProUGUI _increment;

        [SerializeField]
        private Resource _resource;

        [SerializeField]
        private ResourceVariable _hoveredResource;

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
            string increment = AbbreviationUtility.Format(Mathf.Abs(_resource.Production), "0.0");
            _increment.text = increment.Insert(0, _resource.Production < 0 ? "-" : "+");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hoveredResource.SetValue(_resource);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hoveredResource.Clear();
        }
    }
}
