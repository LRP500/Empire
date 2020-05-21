using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Empire
{
    public class TerritoryActionItemUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _titleText = null;

        [SerializeField]
        private Button _actionButton = null;

        private TerritoryAction _action = null;

        public void Initialize(Territory territory, TerritoryAction action, UnityAction callback)
        {
            _action = action;
            _titleText.text = _action.Title;
            _actionButton.onClick.AddListener(callback);
            _actionButton.onClick.AddListener(() => { action.Execute(territory); });
        }
    }
}
