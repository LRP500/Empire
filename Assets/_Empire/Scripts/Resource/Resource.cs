using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Resources/Resource")]
    public class Resource : ScriptableObject
    {
        [SerializeField]
        private ResourceType _type = null;

        [SerializeField]
        private int _current = 0;

        public ResourceType Type => _type;
        public int Current => _current;

        public int SetCurrent(int value)
        {
            int previousAmount = _current;
            _current = Mathf.Clamp(value, 0, value);
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

        public void Initialize()
        {
            SetCurrent(_type.Initial);
        }
    }
}