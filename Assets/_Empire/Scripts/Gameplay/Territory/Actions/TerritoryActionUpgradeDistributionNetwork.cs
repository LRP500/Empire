using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Upgrade Distribution Network")]
    public class TerritoryActionUpgradeDistributionNetwork : TerritoryAction
    {
        public override bool CanExecute(Territory territory)
        {
            return !_context.structureManager.GetInfo(territory).launderingOperation.IsMaxLevel();
        }

        public override void Execute(Territory territory)
        {
            _context.structureManager.UpgradeDistributionNetwork(territory);
        }
    }
}
