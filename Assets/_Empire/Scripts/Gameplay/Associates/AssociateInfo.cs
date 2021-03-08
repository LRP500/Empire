using System.Collections.Generic;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Associates/Associate")]
    public class AssociateInfo : ScriptableObject
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private List<PerkInfo> _positivePerks;

        [SerializeField]
        private List<PerkInfo> _negativePerks;

        public string Name => _name;
        public List<PerkInfo> PositivePerks => _positivePerks;
        public List<PerkInfo> NegativePerks => _negativePerks;
    }
}
