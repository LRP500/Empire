using TMPro;
using UnityEngine;
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

        private Territory _territory = null;

        private System.Action OnActionExecuted = null;

        public TerritoryAction Action { get; private set; } = null;
        public TerritoryActionInfoUI InfoPanel { get; private set; } = null;

        public void Initialize(Territory territory, TerritoryAction action)
        {
            Action = action;
            _territory = territory;
            _titleText.text = Action.Title;

            _actionButton.onClick.AddListener(() =>
            {
                action.Execute(territory);
                OnActionExecuted?.Invoke();
            });
        }

        private void Update()
        {
            _actionButton.interactable = Action.CanExecute(_territory);
        }

        public void SetInfoPanel(TerritoryActionInfoUI infoPanel)
        {
            InfoPanel = infoPanel;
        }

        public void RegisterOnActionExecuted(System.Action callback)
        {
            OnActionExecuted += callback;
        }

        #region UI Events

        public void OnPointerExit(PointerEventData eventData)
        {
            InfoPanel?.Close();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            InfoPanel?.Open();
        }

        #endregion UI Events
    }
}
