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
        private ResourceProduction _methProduction;

        [SerializeField]
        private ResourceProduction _cashProduction;

        [SerializeField]
        private ResourceProduction _bankProduction;

        [Space]
        [SerializeField]
        [SuffixLabel("($k/lbs)", Overlay = true)]
        private int _initialMethSellingPrice = 25;

        [SerializeField]
        private IntVariable _methSellingPrice;

        private int _methProductionCache;
        private int _cashProductionCache;
        private int _bankProductionCache;

        public int MethProduction => _methProduction;
        public int CashProduction => _cashProduction;
        public int BankProduction => _bankProduction;

        public override void Initialize()
        {
            _methSellingPrice.SetValue(_initialMethSellingPrice);
        }

        public override void RefreshOnTick(float elapsed)
        {
            // Store resource amount before production is processed
            _methProductionCache = _context.resourceManager.Meth;
            _cashProductionCache = _context.resourceManager.Cash;
            _bankProductionCache = _context.resourceManager.Bank;

            // Distribution is processed first
            ProcessProduction();

            // Deals are to be honored before own distribution
            ProcessDeals();

            // Distribution on controlled territories
            ProcessDistribution();

            // Laundering money comes last
            ProcessLaundering();

            // Save resource amount increment to variables
            _methProduction.SetValue(_context.resourceManager.Meth - _methProductionCache);
            _cashProduction.SetValue(_context.resourceManager.Cash - _cashProductionCache);
            _bankProduction.SetValue(_context.resourceManager.Bank - _bankProductionCache);
        }

        private void ProcessProduction()
        {
            int production = _context.structureManager.GetTotalProduction();
            _context.resourceManager.AddMeth(production);
        }

        private void ProcessDistribution()
        {
            int totalDistribution = _context.structureManager.GetTotalDistribution();
            int sold = _context.resourceManager.RemoveMeth(totalDistribution);
            _context.resourceManager.AddCash(sold * _methSellingPrice);
        }

        private void ProcessDeals()
        {
            List<Deal> unfulfilledDeals = new List<Deal>();

            foreach (var info in _context.dealManager.Deals)
            {
                Deal deal = info.Value.activeDeal;

                if (deal == null) continue;

                if (_methProduction.Value + _context.resourceManager.Meth >= deal.Quantity)
                {
                    _context.resourceManager.RemoveMeth(deal.Quantity);
                    _context.resourceManager.AddCash(deal.Quantity * deal.SellingPrice);
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

        private void ProcessLaundering()
        {
            int laundering = _context.structureManager.GetTotalLaundering();
            _context.resourceManager.Launder(laundering);
        }
    }
}