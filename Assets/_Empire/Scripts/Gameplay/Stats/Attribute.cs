using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire.Stats
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Attributes/Attribute")]
    public class Attribute : Stat, IAttribute 
    {
        #region Private Fields
        
        [ReadOnly]
        [ShowInInspector]
        private float _modifierStackValue;

        private List<AttributeModifier> _modifiers = new List<AttributeModifier>();

        private bool _dirty = true;

        private event EventHandler OnValueChanged;

        #endregion Private Fields

        #region Properties
        
        public float ModifiedValue
        {
            get
            {
                if (_dirty)
                {
                    UpdateModifiers();
                }

                return BaseValue + _modifierStackValue;
            }
        }

        #endregion Properties

        #region Public Methods

        public void UpdateModifiers()
        {
            _modifierStackValue = 0;
            
            var priorityGroups = _modifiers.OrderBy(y => y.Priority).GroupBy(y => y.Priority);
            foreach (var group in priorityGroups)
            {
                float sum = 0;
                float max = float.MinValue;

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

                _modifierStackValue += group.First().Apply(BaseValue + _modifierStackValue, sum > max ? sum : max);
            }

            _dirty = false;

            OnValueChanged?.Invoke(this, null);
        }

        public void AddModifier(AttributeModifier modifier)
        {
            _modifiers.Add(modifier);
            _dirty = true;
        }

        public void RemoveModifier(AttributeModifier modifier)
        {
            _modifiers.Remove(modifier);
            _dirty = true;
        }

        public void ClearModifiers()
        {
            _modifiers ??= new List<AttributeModifier>();
            _modifiers.Clear();
            _modifierStackValue = 0;
            _dirty = false;
        }

        #endregion Public Methods

        #region Editor

        public void Log()
        {
            Debug.Log($"[Name: {Name}] [Base: {BaseValue}] [Modified: {ModifiedValue}]");
        }

        #endregion Editor
    }
}
