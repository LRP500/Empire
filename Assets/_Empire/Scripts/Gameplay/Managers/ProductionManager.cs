using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Production Manager")]
    public class ProductionManager : ScriptableManager<ProductionManager>
    {
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

        private void ProcessLaundering()
        {
            _context.resourceManager.Launder(_context.structureManager.GetTotalLaundering());
        }

        private void ProcessDistribution()
        {
            int totalDistribution = _context.structureManager.GetTotalDistribution();
            int sold = _context.resourceManager.RemoveMeth(totalDistribution);
            _context.resourceManager.AddCash(sold * _methSellingPrice);
        }

        private void ProcessProduction()
        {
            int totalProduction = _context.structureManager.GetTotalProduction();
            _context.resourceManager.AddMeth(totalProduction);
        }

        private void ProcessDeals()
        {
            List<Deal> unfulfilledDeals = new List<Deal>();

            foreach (var info in _context.dealManager.Deals)
            {
                Deal deal = info.Value.activeDeal;

                if (deal == null) continue;

                int quantitySold = _context.resourceManager.RemoveMeth(deal.Quantity);

                if (quantitySold == deal.Quantity)
                {
                    _context.resourceManager.AddCash(quantitySold * deal.SellingPrice);
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