﻿using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Resource Manager")]
    public class ResourceManager : ScriptableManager<ResourceManager>
    {
        [SerializeField]
        private Resource _bank = null;
        public int Bank => _bank.Current;

        [SerializeField]
        private Resource _cash = null;
        public int Cash => _cash.Current;

        [SerializeField]
        private Resource _meth = null;
        public int Meth => _meth.Current;

        public override void Initialize()
        {
            _meth.Initialize();
            _cash.Initialize();
            _bank.Initialize();
        }

        public override void RefreshOnTick(float elapsed)
        {
            //_meth.Increment(_context.productionManager.MethProduction);
            //_cash.Increment(_context.productionManager.CashProduction);
            //_bank.Increment(_context.productionManager.BankProduction);
        }

        public bool Spend(int amount)
        {
            if (CanSpend(amount))
            {
                // Spend what is available in the bank, then cash
                int spentAmoutBank = _bank.Decrement(amount);
                int spentAmountCash = _cash.Decrement(amount - spentAmoutBank);

                // Notifty threat system of cash spendings
                if (spentAmountCash > 0)
                {
                    EventManager.Instance.Trigger(GameplayEvent.CashSpent, spentAmountCash);
                }

                return true;
            }

            return false;
        }

        public bool CanSpend(int amount)
        {
            return amount <= _bank + _cash;
        }

        /// <summary>
        /// Convert cash into bank.
        /// </summary>
        /// <param name="amount"></param>
        public void Launder(int amount)
        {
            _bank.Increment(_cash.Decrement(amount));
        }

        public void AddCash(int amount)
        {
            _cash.Increment(amount);
        }

        public int RemoveCash(int amount)
        {
            return _cash.Decrement(amount);
        }

        public int RemoveMeth(int amount)
        {
            return _meth.Decrement(amount);
        }

        public void AddMeth(int amount)
        {
            _meth.Increment(amount);
        }

        public void AddBank(int amount)
        {
            _bank.Increment(amount);
        }

        public int RemoveBank(int amount)
        {
            return _bank.Decrement(amount);
        }
    }
}
