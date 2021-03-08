using TMPro;
using Tools.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Empire
{
    public class ResourceProductionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private TextMeshProUGUI _displayText;

        [SerializeField]
        private ResourceProduction _production;

        [SerializeField]
        private ResourceProductionVariable _hoveredProduction;

        private void Awake()
        {
            _production.Subscribe(Refresh);
        }

        private void OnDestroy()
        {
            _production.Unsubscribe(Refresh);
        }

        private void Refresh()
        {
            string increment = AbbreviationUtility.Format(Mathf.Abs(_production), "0.0");
            _displayText.text = increment.Insert(0, _production < 0 ? "-" : "+");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hoveredProduction.SetValue(_production);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hoveredProduction.Clear();
        }
    }
}
