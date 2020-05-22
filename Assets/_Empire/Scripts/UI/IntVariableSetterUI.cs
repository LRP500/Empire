using TMPro;
using Tools.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class IntVariableSetterUI : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _min = null;

        [SerializeField]
        private IntVariable _max = null;

        [SerializeField]
        private IntVariable _current = null;

        [SerializeField]
        private Button _incrementButton = null;

        [SerializeField]
        private Button _decrementButton = null;

        [SerializeField]
        private TextMeshProUGUI _valueText = null;

        private void Awake()
        {
            _current.Subscribe(Refresh);
            _incrementButton.onClick.AddListener(OnClickIncrement);
            _decrementButton.onClick.AddListener(OnClickDecrement);
        }

        private void Refresh()
        {
            _valueText.text = _current.Value.ToString();
        }

        private void OnClickIncrement()
        {
            if (_current < _max)
            {
                _current.Increment();
            }
        }

        private void OnClickDecrement()
        {
            if (_current > _min)
            {
                _current.Decrement();
            }
        }
    }
}
