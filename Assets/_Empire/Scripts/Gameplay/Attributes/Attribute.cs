using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire.Attributes
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Attributes/Attribute")]
    public class Attribute : ScriptableObject
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
        private float _minValue;

        [SerializeField]
        private float _maxValue;

        #endregion Serialized Fields

        #region Private Fields
        
        [ReadOnly]
        [ShowInInspector]
        private float _modifierStackValue;

        private List<AttributeModifier> _modifiers;

        private bool _dirty = true;

        private event EventHandler OnValueChanged;

        #endregion Private Fields

        public string Name => _name;
        
        public float BaseValue => _baseValue;

        public float ModifiedValue
        {
            get
            {
                if (_dirty)
                {
                    UpdateModifiers();
                }

                return _baseValue + _modifierStackValue;
            }
        }

        private List<AttributeModifier> Modifiers => _modifiers ??= new List<AttributeModifier>();

        public void SetBaseValue(float value)
        {
            _baseValue = Mathf.Clamp(value, _minValue, _maxValue);
        }

        private void UpdateModifiers()
        {
            _modifierStackValue = 0;
            
            var groups = Modifiers.OrderBy(x => x.Priority).GroupBy(y => y.Priority);
            foreach (var group in groups)
            {
                float sum = 0;
                float max = 0;

                foreach (AttributeModifier modifier in group)
                {
                    if (!modifier.Stacks)
                    {
                        max = Mathf.Max(max, modifier.Value);
                    }
                    else
                    {
                        sum += modifier.Value;
                    }
                }

                _modifierStackValue += group.First().Apply(_baseValue + _modifierStackValue, Mathf.Max(sum, max));
            }

            _dirty = false;

            OnValueChanged?.Invoke(this, null);
        }

        public void AddModifier(AttributeModifier modifier)
        {
            Modifiers.Add(modifier);
            _dirty = true;
        }

        public void RemoveModifier(AttributeModifier modifier)
        {
            Modifiers.Remove(modifier);
            _dirty = true;
        }

        public void CleanModifiers()
        {
            Modifiers.Clear();
        }

        #region Editor

        public void Log()
        {
            Debug.Log($"[Name: {Name}] " +
                      $"[Base: {BaseValue}] " +
                      $"[Modified: {ModifiedValue}]");
        }

        #endregion Editor
    }
}
