using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Cancel Deal")]
    public class TerritoryActionCancelDeal : TerritoryAction
    {
        public override bool CanExecute(Territory territory)
        {
            return true;
        }

        public override void Execute(Territory territory)
        {
            _context.dealManager.CancelActiveDeal(territory);
            _context.worldMapManager.SetRival(territory);
        }
    }
}
