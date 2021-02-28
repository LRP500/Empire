using System.Collections;
using Empire.Attributes;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Attribute = Empire.Attributes.Attribute;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Perk")]
    public class PerkInfo : ScriptableObject
    {
        [SerializeField]
        private Attribute _attribute;

#if UNITY_EDITOR

        [SerializeReference]
        [ValueDropdown(nameof(GetModifierTypes))]
        private System.Type _modifierType;

#endif

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
