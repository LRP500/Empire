using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Make Deal")]
    public class TerritoryActionMakeDeal : TerritoryAction
    {
        public override void Execute(Territory territory)
        {
            _context.dealManager.AcceptDealOffer(territory);
            _context.worldMapManager.SetInDeal(territory);
        }
    }
}
