using Tools.FSM;
using Tools.Time;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/FSM/Gameplay/Context")]
    public class GameplayContext : AContext
    {
        public DealManager dealManager = null;
        public ThreatManager threatManager = null;
        public ResourceManager resourceManager = null;
        public WorldMapManager worldMapManager = null;
        public StructureManager structureManager = null;
        public ProductionManager productionManager = null;
        public TimeControllerVariable timeController = null;
        public IntVariable turnCount = null;

        public float SpeedMultiplier => timeController.Value.CurrentSpeedMultiplier;

        public void Initialize()
        {
            dealManager.Initialize();
            threatManager.Initialize();
            worldMapManager.Initialize();
            resourceManager.Initialize();
            structureManager.Initialize();
            productionManager.Initialize();
        }

        public void Refresh()
        {
            float elapsed = Time.deltaTime * SpeedMultiplier; 
            threatManager.Refresh(elapsed);
            dealManager.Refresh(elapsed);
        }

        public void RefreshOnTick(float elapsed)
        {
            productionManager.RefreshOnTick(elapsed);
            resourceManager.RefreshOnTick(elapsed);
            threatManager.RefreshOnTick(elapsed);
        }

        public void Clear()
        {
            threatManager.Clear();
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
