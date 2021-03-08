using System.Collections;
using Empire.Stats;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Attribute = Empire.Stats.Attribute;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Perks/Perk")]
    public class PerkInfo : ScriptableObject
    {
        [SerializeField]
        private Attribute _attribute;

        [SerializeReference]
#if UNITY_EDITOR
        [ValueDropdown(nameof(GetModifierTypes))]
#endif
        private System.Type _modifierType;

        [SerializeField]
        private float _minValue;

        [SerializeField]
        private float _maxValue;

        public Attribute Attribute => _attribute;
        public System.Type ModifierType => _modifierType;
        public float MinValue => _minValue;
        public float MaxValue => _maxValue;

#if UNITY_EDITOR

        private static IEnumerable GetModifierTypes()
        {
            return TypeCache.GetTypesDerivedFrom<AttributeModifier>();
        }

#endif
    }
}
