using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class TerritoryTakeOverOddDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text = null;

        [SerializeField]
        private Image _icon = null;

        public void Initialize(TakeOverOdds odds)
        {
            Color color = odds.failure >= odds.success ? Color.red : Color.green;
            _text.text = odds.ToString();
            _text.color = color;

            if (_icon)
            {
                _icon.color = color;
            }
        }
    }
}
