using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Resource Manager")]
    public class Resource : ScriptableObject
    {
        [SerializeField]
        private ResourceType _type;

        [SerializeField]
        private int _initial;

        [SerializeField]
        private int _current;

        [SerializeField]
        private bool _clamped;

        [SerializeField]
        [ShowIf(nameof(_clamped))]
        private int _min;

        [SerializeField]
        [ShowIf(nameof(_clamped))]
        private int _max;

        [SerializeField]
        private ResourceProduction _production;

        public ResourceType Type => _type;
        public int Initial => _initial;
        public int Current => _current;
        public int Max => _max;
        public int Production => _production.Value;

        private System.Action<int> OnCurrentValueChanged;

        public void Initialize()
        {
            SetCurrent(_initial);
        }

        private int SetCurrent(int value)
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

        #region Callbacks

        public void RegisterOnCurrentValueChanged(System.Action<int> callback)
        {
            OnCurrentValueChanged += callback;
        }

        public void UnregisterOnCurrentValueChanged(System.Action<int> callback)
        {
            OnCurrentValueChanged -= callback;
        }

        #endregion Callbacks

        public static implicit operator int(Resource lhs)
        {
            return lhs.Current;
        }
    }
}