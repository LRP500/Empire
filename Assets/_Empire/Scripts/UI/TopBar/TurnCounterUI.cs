using TMPro;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    public class TurnCounterUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _turnText = null;

        [SerializeField]
        private IntVariable _turnCount = null;

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
            _turnText.text = $"Turn {_turnCount.ToString()}";
        }
    }
}
