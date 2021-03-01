using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire.Stats
{
    public class Stat : ScriptableObject, IStat
    {
        #region Serialized Fields

        [SerializeField]
        private string _name;

#if UNITY_EDITOR
        [Multiline]
        [SerializeField]
        private string _description;
#endif

        [SerializeField]
        private float _baseValue;

        [SerializeField]
        private bool _clamped;

        [SerializeField]
        [ShowIf(nameof(_clamped))]
        private float _minValue;

        [SerializeField]
        [ShowIf(nameof(_clamped))]
        private float _maxValue;

        #endregion Serialized Fields

        #region Properties

        public string Name => _name;
        
        public float BaseValue => _baseValue;
        
        public float MinValue => _minValue;
        
        public float MaxValue => _maxValue;

        public bool Clamped { get; set; }

        #endregion Properties

        #region Public Methods

        public void SetBaseValue(float value)
        {
            _baseValue = Clamped ? Mathf.Clamp(value, MinValue, MaxValue) : value;
        }

        #endregion Public Methods
    }
}
