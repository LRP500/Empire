using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Make Deal")]
    public class TerritoryActionMakeDeal : TerritoryAction
    {
        [Space]

        [SerializeField]
        private DealSettings _defaultDealSettings = null;

        [SerializeField]
        private DealListVariable _dealsInProgress = null;

        public Deal CurrentDealOffer { get; private set; } = null;

        public override void Execute(Territory territory)
        {
            base.Execute(territory);

            CurrentDealOffer = new Deal(_defaultDealSettings);
            _dealsInProgress.Add(CurrentDealOffer);

            territory.SetInDeal();
        }
    }
}
