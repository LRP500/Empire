using Sirenix.OdinInspector;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Resource Manager")]
    public class Resource : ScriptableObject
    {
        [SerializeField]
        private ResourceType _type = null;
        public ResourceType Type => _type;

        [SerializeField]
        private int _initial = 0;

        [SerializeField]
        private int _current = 0;
        public int Current => _current;

        [SerializeField]
        private bool _clamped = false;

        [SerializeField]
        [ShowIf(nameof(_clamped))]
        private int _min = 0;

        [SerializeField]
        [ShowIf(nameof(_clamped))]
        private int _max = 0;
        public int Max => _max;

        [SerializeField]
        private IntVariable _production = null;
        public int Production => _production.Value;

        private System.Action<int> OnCurrentValueChanged = null;

        public void Initialize()
        {
            SetCurrent(_initial);
        }

        public int SetCurrent(int value)
        {
            int previousAmount = _current;
            _current = _clamped ? Mathf.Clamp(value, _min, _max) : Mathf.Clamp(value, 0, value);
            OnCurrentValueChanged?.Invoke(_current);
            return _current - previousAmount;
        }

        public int Increment(int value)
        {
            return SetCurrent(_current + value);
        }

        public int Decrement(int value)
        {
            return SetCurrent(_current - value) * -1;
        }

        public void RegisterOnCurrentValueChanged(System.Action<int> callback)
        {
            OnCurrentValueChanged += callback;
        }

        public void UnregisterOnCurrentValueChanged(System.Action<int> callback)
        {
            OnCurrentValueChanged -= callback;
        }

        public static implicit operator int(Resource lhs)
        {
            return lhs.Current;
        }
    }
}