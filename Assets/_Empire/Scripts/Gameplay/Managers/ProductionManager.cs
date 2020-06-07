using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Production Manager")]
    public class ProductionManager : ScriptableManager<ProductionManager>
    {
        [Space]
        [SerializeField]
        private IntVariable _methProduction = null;
        public int MethProduction => _methProduction;

        [SerializeField]
        private IntVariable _cashProduction = null;
        public int CashProduction => _cashProduction;

        [SerializeField]
        private IntVariable _bankProduction = null;
        public int BankProduction => _bankProduction;

        [Space]
        [SerializeField]
        [SuffixLabel("($k/lbs)", Overlay = true)]
        private int _initialMethSellingPrice = 25;

        [SerializeField]
        private IntVariable _methSellingPrice = null;

        public override void Initialize()
        {
            _methSellingPrice.SetValue(_initialMethSellingPrice);
        }

        public override void RefreshOnTick(float elapsed)
        {
            // Distribution is processed first
            ProcessProduction();

            // Deals are to be honored before own distribution
            ProcessDeals();

            // Distribution on controlled territories
            ProcessDistribution();

            // Laundering money comes last
            ProcessLaundering();
        }

        private void ProcessProduction()
        {
            _methProduction.SetValue(_context.structureManager.GetTotalProduction());
        }

        private void ProcessDistribution()
        {
            int totalDistribution = _context.structureManager.GetTotalDistribution();
            int sold = _context.resourceManager.RemoveMeth(totalDistribution);
            _cashProduction.SetValue(sold * _methSellingPrice);
        }

        private void ProcessLaundering()
        {
            _bankProduction.SetValue(_context.structureManager.GetTotalLaundering());
        }

        private void ProcessDeals()
        {
            List<Deal> unfulfilledDeals = new List<Deal>();

            foreach (var info in _context.dealManager.Deals)
            {
                Deal deal = info.Value.activeDeal;

                if (deal == null) continue;

                if (_methProduction.Value >= deal.Quantity)
                {
                    _methProduction.Substract(deal.Quantity);
                    _cashProduction.Add(deal.Quantity * deal.SellingPrice);
                }
                else
                {
                    unfulfilledDeals.Add(deal);
                }
            }

            foreach (Deal unfulfilled in unfulfilledDeals)
            {
                _context.dealManager.CancelActiveDeal(unfulfilled.Territory);
                _context.worldMapManager.SetRival(unfulfilled.Territory);
            }
        }
    }
}