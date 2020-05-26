using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Build Laboratory")]
    public class TerritoryActionBuildLaboratory : TerritoryAction
    {
        public override bool CanExecute(Territory territory)
        {
            return !_context.structureManager.GetInfo(territory).laboratory.IsMaxLevel();
        }

        public override void Execute(Territory territory)
        {
            _context.structureManager.UpgradeLaboratories(territory);
        }
    }
}