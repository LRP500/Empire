using Tools;
using Tools.Time;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/FSM/Gameplay/Context")]
    public class GameplayContext : SingletonScriptableObject<GameplayContext>
    {
        public DealManager dealManager;
        public ThreatManager threatManager;
        public TakeOverManager takeOverManager;
        public ResourceManager resourceManager;
        public WorldMapManager worldMapManager;
        public StructureManager structureManager;
        public ProductionManager productionManager;
        public TimeControllerVariable timeController;

        public IntVariable turnCount;

        public float SpeedMultiplier => timeController.Value.CurrentSpeedMultiplier;

        public void Initialize()
        {
            dealManager.Initialize();
            threatManager.Initialize();
            takeOverManager.Initialize();
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
            takeOverManager.Clear();
        }

        #region MAYBE!?

        public class TerritoryInfo
        {
            public DealManager.TerritoryDealInfo deals;
            public StructureManager.TerritoryStructureInfo structures;
        }

        public TerritoryInfo GetTerritoryInfo(Territory territory)
        {
            var info = new TerritoryInfo
            {
                deals = dealManager.GetInfo(territory),
                structures = structureManager.GetInfo(territory)
            };

            return info;
        }

        #endregion MAYBE!?
    }
}
