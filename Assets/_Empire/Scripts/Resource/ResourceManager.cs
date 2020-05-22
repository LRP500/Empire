using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Tools;
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

        [SerializeField]
        [SuffixLabel("(cash/controlled)", Overlay = true)]
        private int _initialLaunderingRate = 500;

        [SerializeField]
        [SuffixLabel("($/lbs)", Overlay = true)]
        private int _methSellingPriceMin = 18;

        [SerializeField]
        [SuffixLabel("($/lbs)", Overlay = true)]
        private int _methSellingPriceMax = 50;

        [Header("Context")]

        [SerializeField]
        public TerritoryListVariable _territories = null;

        private List<Territory> _controlledTerritories = null;

        public void Initialize()
        {
            _bank.Initialize();
            _cash.Initialize();
            _meth.Initialize();
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

        private void ProcessDeals()
        {
        }
    }
}
