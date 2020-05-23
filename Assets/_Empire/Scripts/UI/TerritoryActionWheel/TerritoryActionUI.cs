using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Empire
{
    public class TerritoryActionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private TextMeshProUGUI _titleText = null;

        [SerializeField]
        private Button _actionButton = null;

        private TerritoryAction _action = null;

        private TerritoryActionInfoUI _infoPanel = null;

        private System.Action OnActionExecuted = null;

        public void Initialize(Territory territory, TerritoryAction action, TerritoryActionInfoUI infoPanel)
        {
            _action = action;
            _titleText.text = _action.Title;

            _infoPanel = infoPanel;
            _infoPanel.Initialize(_action);

            _actionButton.onClick.AddListener(() =>
            {
                action.Execute(territory);
                OnActionExecuted?.Invoke();
            });
        }

        public void RegisterOnActionExecuted(System.Action callback)
        {
            OnActionExecuted += callback;
        }

        #region UI Events

        public void OnPointerExit(PointerEventData eventData)
        {
            _infoPanel.Close();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _infoPanel.Open();
        }

        #endregion UI Events
    }
}
