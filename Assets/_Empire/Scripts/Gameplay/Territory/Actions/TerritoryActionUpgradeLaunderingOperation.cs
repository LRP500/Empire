using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Upgrade Laundering Operation")]
    public class TerritoryActionUpgradeLaunderingOperation : TerritoryAction
    {
        public override bool CanExecute(Territory territory)
        {
            Structure structure = _context.structureManager.GetInfo(territory).launderingOperation;
            return !structure.IsMaxLevel() && _context.resourceManager.CanSpend(structure.Price);
        }

        public override void Execute(Territory territory)
        {
            _context.structureManager.UpgradeLaunderingOperation(territory);
        }
    }
}