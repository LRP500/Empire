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
        private bool _hasLowerLimit;

        [SerializeField]
        private bool _hasUpperLimit;

        [SerializeField]
        [ShowIf(nameof(_hasLowerLimit))]
        private float _minValue;

        [SerializeField]
        [ShowIf(nameof(_hasUpperLimit))]
        private float _maxValue;

        #endregion Serialized Fields

        #region Properties

        public string Name => _name;
        
        public float BaseValue => _baseValue;
        
        public float MinValue => _hasLowerLimit ? _minValue : float.MinValue;

        public float MaxValue => _hasUpperLimit ? _maxValue : float.MaxValue;

        #endregion Properties

        #region Public Methods

        public void SetBaseValue(float value)
        {
            _baseValue = Mathf.Clamp(value, MinValue, MaxValue);
        }

        #endregion Public Methods
    }
}
