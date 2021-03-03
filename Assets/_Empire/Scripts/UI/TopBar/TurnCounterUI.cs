using TMPro;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    public class TurnCounterUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _turnText;

        [SerializeField]
        private IntVariable _turnCount;

        private void Awake()
        {
            _turnCount.Subscribe(Refresh);
        }

        private void OnDestroy()
        {
            _turnCount.Unsubscribe(Refresh);
        }

        private void Refresh()
        {
            _turnText.text = $"Day {_turnCount.Value}";
        }
    }
}
