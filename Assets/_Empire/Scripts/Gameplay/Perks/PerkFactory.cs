using UnityEngine;

namespace Empire
{
    public class PerkFactory : ScriptableObject
    {
        [SerializeField]
        private PerkInfoListVariable _commonPositivePerks;

        [SerializeField]
        private PerkInfoListVariable _commonNegativePerks;

        public Perk CreatePositiveCommon()
        {
            return new Perk(_commonPositivePerks.Random());
        }

        public Perk CreateNegativeCommon()
        {
            return new Perk(_commonNegativePerks.Random());
        }
    }
}