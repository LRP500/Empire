using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Build Laboratory")]
    public class TerritoryActionBuildLaboratory : TerritoryAction
    {
        public override bool CanExecute(Territory territory)
        {
            Structure structure = _context.structureManager.GetInfo(territory).laboratory;
            return !structure.IsMaxLevel() && _context.resourceManager.CanSpend(structure.Price);
        }

        public override void Execute(Territory territory)
        {
            base.Execute(territory);

            _context.structureManager.UpgradeLaboratories(territory);
        }
    }
}