using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Upgrade Distribution Network")]
    public class TerritoryActionUpgradeDistributionNetwork : TerritoryAction
    {
        public override bool CanExecute(Territory territory)
        {
            Structure structure = _context.structureManager.GetInfo(territory).distributionNetwork;
            return !structure.IsMaxLevel() && structure.Price <= _context.resourceManager.Bank;
        }

        public override void Execute(Territory territory)
        {
            int price = _context.structureManager.UpgradeDistributionNetwork(territory);
            _context.resourceManager.Bank.Decrement(price);
        }

    }
}
