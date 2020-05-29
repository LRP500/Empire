using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Resource Manager")]
    public class ResourceManager : ScriptableManager<ResourceManager>
    {
        [SerializeField]
        private Resource _bank = null;
        public Resource Bank => _bank;

        [SerializeField]
        private Resource _cash = null;
        public Resource Cash => _cash;

        [SerializeField]
        private Resource _meth = null;
        public Resource Meth => _meth;

        public override void Initialize()
        {
            _bank.Initialize();
            _cash.Initialize();
            _meth.Initialize();
        }

        public bool Spend(int amount)
        {
            if (CanSpend(amount))
            {
                // Spend what is available in the bank, then cash
                Cash.Decrement(amount - Bank.Decrement(amount));
                return true;
            }

            return false;
        }

        public bool CanSpend(int amount)
        {
            return amount <= Bank + Cash;
        }
    }
}
