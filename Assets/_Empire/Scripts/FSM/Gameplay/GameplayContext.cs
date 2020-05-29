using Tools.FSM;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/FSM/Gameplay/Context")]
    public class GameplayContext : AContext
    {
        public ResourceManager resourceManager = null;
        public StructureManager structureManager = null;
        public DealManager dealManager = null;
        public ProductionManager productionManager = null;
        public WorldMapManager worldMapManager = null;

        public void Initialize()
        {
            dealManager.Initialize();
            worldMapManager.Initialize();
            resourceManager.Initialize();
            structureManager.Initialize();
            productionManager.Initialize();
        }

        public void Refresh()
        {
            dealManager.Refresh();
        }

        public void RefreshOnTick(float elapsed)
        {
            productionManager.RefreshOnTick(elapsed);
        }

        #region MAYBE!?

        public class TerritoryInfo
        {
            public DealManager.TerritoryDealInfo deals = null;
            public StructureManager.TerritoryStructureInfo structures = null;
        }

        public TerritoryInfo GetTerritoryInfo(Territory territory)
        {
            TerritoryInfo info = new TerritoryInfo
            {
                deals = dealManager.GetInfo(territory),
                structures = structureManager.GetInfo(territory)
            };

            return info;
        }

        #endregion MAYBE!?
    }
}
