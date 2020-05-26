using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Resource Manager")]
    public class ResourceManager : SingletonScriptableObject<ResourceManager>
    {
        #region Serialized Fields

        [Header("Resources")]

        [SerializeField]
        private Resource _bank = null;
        public Resource Bank => _bank;

        [SerializeField]
        private Resource _cash = null;
        public Resource Cash => _cash;

        [SerializeField]
        private Resource _meth = null;
        public Resource Meth => _meth;

        [Header("Production")]

        //[SerializeField]
        //[SuffixLabel("(lbs/controlled)", Overlay = true)]
        //private int _initialMethProduction = 20;

        [Header("Laundering")]

        //[SerializeField]
        //[SuffixLabel("(cash/controlled)", Overlay = true)]
        //private int _initialLaunderingRate = 500;

        [Header("Distribution")]

        [SerializeField]
        [SuffixLabel("(lbs/controlled)", Overlay = true)]
        private int _initialDistributionRate = 10;

        [SerializeField]
        [SuffixLabel("($k/lbs)", Overlay = true)]
        private int _initialMethSellingPrice = 25;

        [SerializeField]
        private IntVariable _methSellingPrice = null;

        [Header("Context")]

        [SerializeField]
        private TerritoryListVariable _territories = null;

        [SerializeField]
        private DealListVariable _deals = null;

        #endregion Serialized Fields

        #region Private Fields

        private List<Territory> _controlledTerritories = null;

        #endregion Private Fields

        public void Initialize()
        {
            _deals.Clear();

            _bank.Initialize();
            _cash.Initialize();
            _meth.Initialize();

            _methSellingPrice.SetValue(_initialMethSellingPrice);
        }

        public bool Refresh()
        {
            bool profitable = false;

            _controlledTerritories = _territories.Items.Where(x => x.State is TerritoryStateControlled).ToList();

            ProcessStructures(); // Structures are processed first
            ProcessDeals(); // Deals are processed before own distribution
            ProcessDistribution();

            return profitable;
        }

        private void ProcessStructures()
        {
            foreach (Territory state in _controlledTerritories)
            {
                _meth.Increment(state.GetProductionRate());
                _bank.Increment(_cash.Decrement(state.GetLaunderingRate()));
            }
        }

        // HAS TO GO BEFORE LAUNDERING
        private void ProcessDistribution()
        {
            int sold = _meth.Decrement(_initialDistributionRate * _controlledTerritories.Count);
            _cash.Increment(sold * _methSellingPrice);
        }

        private void ProcessDeals()
        {
            List<Deal> unfulfilledDeals = new List<Deal>();

            foreach (Deal deal in _deals.Items)
            {
                int quantitySold = _meth.Decrement(deal.Quantity);

                if (quantitySold == deal.Quantity)
                {
                    _cash.Increment(quantitySold * deal.SellingPrice);
                }
                else
                {
                    unfulfilledDeals.Add(deal);
                }
            }

            foreach (Deal unfulfilled in unfulfilledDeals)
            {
                unfulfilled.Territory.CancelCurrentDeal(_deals);
                unfulfilled.Territory.SetRival();
            }
        }
    }
}
