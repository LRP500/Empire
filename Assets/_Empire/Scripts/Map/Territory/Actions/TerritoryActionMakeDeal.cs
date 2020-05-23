using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Make Deal")]
    public class TerritoryActionMakeDeal : TerritoryAction
    {
        [SerializeField]
        private DealSettings _defaultDealSettings = null;

        [SerializeField]
        private DealListVariable _dealsInProgress = null;

        public override void Execute(Territory territory)
        {
            base.Execute(territory);

            Deal deal = new Deal(_defaultDealSettings);
            _dealsInProgress.Add(deal);

            territory.SetInDeal();
        }
    }
}
