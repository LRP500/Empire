using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Upgrade Distribution Network")]
    public class TerritoryActionUpgradeDistributionNetwork : TerritoryAction
    {
        public override bool CanExecute(Territory territory)
        {
            Structure structure = _context.structureManager.GetInfo(territory).distributionNetwork;
            return !structure.IsMaxLevel() && _context.resourceManager.CanSpend(structure.Price);
        }

        public override void Execute(Territory territory)
        {
            _context.structureManager.UpgradeDistributionNetwork(territory);
        }
    }
}
