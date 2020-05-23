using TMPro;
using UnityEngine;

namespace Empire
{
    public class KeyValueItemUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _key = null;

        [SerializeField]
        private TextMeshProUGUI _value = null;

        public void SetKey(string key)
        {
            _key.text = key;
        }

        public void SetValue(string value)
        {
            _value.text = value;
        }

        public void SetKeyValue(string key, string value)
        {
            SetKey(key);
            SetValue(value);
        }
    }
}
