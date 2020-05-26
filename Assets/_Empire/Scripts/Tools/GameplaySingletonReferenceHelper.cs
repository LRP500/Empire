using UnityEngine;

#pragma warning disable 0414

namespace Empire
{
    public class GameplaySingletonReferenceHelper : MonoBehaviour
    {
        public ResourceManager resourceManager = null;
        public StructureManager structureManager = null;
        public DealManager dealManager = null;
        public ProductionManager productionManager = null;
    }
}