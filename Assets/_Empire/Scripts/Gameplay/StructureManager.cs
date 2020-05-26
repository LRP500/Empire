using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Structure Manager")]
    public class StructureManager : SingletonScriptableObject<StructureManager>
    {
        [SerializeField]
        private LaboratorySettings _laboratorySettings = null;

        [SerializeField]
        private LaunderingOperationSettings _launderingOperationSettings = null;

        public void AddLaboratory(Territory territory)
        {
            territory.AddLaboratory(new Laboratory(_laboratorySettings));
        }

        public void AddLaunderingOperation(Territory territory)
        {
            territory.AddLaunderingOperation(new LaunderingOperation(_launderingOperationSettings));
        }

        public void ClearStructures(Territory territory)
        {
            territory.DestroyAllStructures();
        }
    }
}
