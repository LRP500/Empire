using System.Collections.Generic;
using UnityEngine;

namespace Empire
{
    public class AssociateFactory : ScriptableObject
    {
        [SerializeField]
        private AssociateInfoListVariable _uniqueAssociates;

        [SerializeField]
        private PerkFactory _perkFactory;

        public Associate CreateRandom()
        {
            var positivePerks = new List<Perk>
            {
                _perkFactory.CreatePositiveCommon()
            };

            var negativePerks = new List<Perk>
            {
                _perkFactory.CreateNegativeCommon()
            };

            return new Associate("Anonymous", positivePerks, negativePerks);
        }

        public Associate CreateUnique()
        {
            AssociateInfo info = _uniqueAssociates.Random();

            var positivePerks = new List<Perk>();
            var negativePerks = new List<Perk>();

            foreach (PerkInfo perkInfo in info.PositivePerks)
            {
                positivePerks.Add(new Perk(perkInfo));
            }

            foreach (PerkInfo perkInfo in info.NegativePerks)
            {
                negativePerks.Add(new Perk(perkInfo));
            }

            return new Associate(info.Name, positivePerks, negativePerks);
        }
    }
}
