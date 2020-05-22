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
        [Header("Resources")]

        [SerializeField]
        private Resource _bank = null;

        [SerializeField]
        private Resource _cash = null;

        [SerializeField]
        private Resource _meth = null;

        [Header("Production")]

        [SerializeField]
        [SuffixLabel("(lbs/controlled)", Overlay = true)]
        private int _initialMethProduction = 50;

        [Header("Laundering")]

        [SerializeField]
        [SuffixLabel("(cash/controlled)", Overlay = true)]
        private int _initialLaunderingRate = 500;

        [Header("Distribution")]

        [SerializeField]
        [SuffixLabel("(lbs/controlled)", Overlay = true)]
        private int _initialDistributionRate = 50;

        [SerializeField]
        [SuffixLabel("($k/lbs)", Overlay = true)]
        private int _initialMethSellingPrice = 25;

        [SerializeField]
        private IntVariable _methSellingPrice = null;

        [Header("Context")]

        [SerializeField]
        public TerritoryListVariable _territories = null;

        private List<Territory> _controlledTerritories = null;

        public void Initialize()
        {
            _bank.Initialize();
            _cash.Initialize();
            _meth.Initialize();

            _methSellingPrice.SetValue(_initialMethSellingPrice);
        }

        public bool Refresh()
        {
            bool profitable = false;

            _controlledTerritories = _territories.Items.Where(x => x.State is TerritoryStateControlled).ToList();

            ProcessMethProduction();
            //ProcessMoneyLaundering();
            ProcessDeals();

            return profitable;
        }

        private void ProcessMethProduction()
        {
            _meth.Increment(_initialMethProduction * _controlledTerritories.Count);
        }

        private void ProcessMoneyLaundering()
        {
            _bank.Increment(_cash.Decrement(_initialMethProduction * _controlledTerritories.Count));
        }

        private void ProcessDistribution()
        {

        }

        private void ProcessDeals()
        {
        }
    }
}
