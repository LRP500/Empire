using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Finance Laundering Operation")]
    public class TerritoryActionFinanceLaunderingOperation : TerritoryAction
    {
        public override bool CanExecute(Territory territory)
        {
            return !_context.structureManager.GetInfo(territory).launderingOperation.IsMaxLevel();
        }

        public override void Execute(Territory territory)
        {
            _context.structureManager.UpgradeLaunderingOperation(territory);
        }
    }
}