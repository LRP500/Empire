using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Build Laboratory")]
    public class TerritoryActionBuildLaboratory : TerritoryAction
    {
        public override bool CanExecute(Territory territory)
        {
            Structure structure = _context.structureManager.GetInfo(territory).laboratory;
            return !structure.IsMaxLevel() && structure.Price <= _context.resourceManager.Bank;
        }

        public override void Execute(Territory territory)
        {
            int price = _context.structureManager.UpgradeLaboratories(territory);
            _context.resourceManager.Bank.Decrement(price);
        }
    }
}