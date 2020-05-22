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
        private IntVariable _target = null;

        [SerializeField]
        private Button _incrementButton = null;

        [SerializeField]
        private Button _decrementButton = null;

        [SerializeField]
        private TextMeshProUGUI _valueText = null;

        private void Awake()
        {
            _target.Subscribe(Refresh);
            _incrementButton.onClick.AddListener(_target.Increment);
            _decrementButton.onClick.AddListener(_target.Decrement);
        }

        private void Refresh()
        {
            _valueText.text = _target.Value.ToString();
        }
    }
}
