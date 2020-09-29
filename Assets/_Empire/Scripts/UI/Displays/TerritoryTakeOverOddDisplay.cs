using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Empire.TakeOverManager;

namespace Empire
{
    public class TerritoryTakeOverOddDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text = null;

        [SerializeField]
        private Image _icon = null;

        [SerializeField]
        private TakeOverManager _takeOverManager = null;

        public void Initialize(TakeOverInfo info)
        {
            TakeOverOdds odds = _takeOverManager.GetOdds(info);

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
