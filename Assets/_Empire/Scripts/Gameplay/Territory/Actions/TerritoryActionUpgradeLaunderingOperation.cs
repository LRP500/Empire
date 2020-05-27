using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Upgrade Laundering Operation")]
    public class TerritoryActionUpgradeLaunderingOperation : TerritoryAction
    {
        public override bool CanExecute(Territory territory)
        {
            Structure structure = _context.structureManager.GetInfo(territory).launderingOperation;
            return !structure.IsMaxLevel() && structure.Price <= _context.resourceManager.Bank;
        }

        public override void Execute(Territory territory)
        {
            int price = _context.structureManager.UpgradeLaunderingOperation(territory);
            _context.resourceManager.Bank.Decrement(price);
        }
    }
}