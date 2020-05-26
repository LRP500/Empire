using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Build Laboratory")]
    public class TerritoryActionBuildLaboratory : TerritoryAction
    {
        [SerializeField]
        private LaboratorySettings _laboratorySettings = null;

        public override void Execute(Territory territory)
        {
            territory.AddLaboratory(new Laboratory(_laboratorySettings));
        }
    }
}