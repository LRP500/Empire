using System.Collections.Generic;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Structure Manager")]
    public class StructureManager : ScriptableManager<StructureManager>
    {
        public class TerritoryStructureInfo
        {
            public Laboratory laboratory = null;
            public LaunderingOperation launderingOperation = null;
            public DistributionNetwork distributionNetwork = null;
        }

        public Dictionary<Territory, TerritoryStructureInfo> Structures { get; private set; } = null;

        [SerializeField]
        private StructureSettings _laboratorySettings = null;

        [SerializeField]
        private StructureSettings _distributionNetworkSettings = null;

        [SerializeField]
        private StructureSettings _launderingOperationSettings = null;

        public override void Initialize()
        {
            Structures = new Dictionary<Territory, TerritoryStructureInfo>();
        }

        public void InitializeTerritoryStructures(Territory territory)
        {
            TerritoryStructureInfo info = new TerritoryStructureInfo()
            {
                laboratory = new Laboratory(_laboratorySettings),
                launderingOperation = new LaunderingOperation(_launderingOperationSettings),
                distributionNetwork = new DistributionNetwork(_distributionNetworkSettings)
            };

            //Structures.Add(territory, info);
            Structures[territory] = info;
        }

        public void ClearStructures(Territory territory)
        {
            Structures.Remove(territory);
        }
        
        #region Upgrade

        private void UpgradeStructure(Structure structure)
        {
            if (_context.resourceManager.Spend(structure.Price))
            {
                structure.Upgrade();
            }
        }

        public void UpgradeLaboratories(Territory territory)
        {
            UpgradeStructure(GetInfo(territory).laboratory);
        }

        public void UpgradeLaunderingOperation(Territory territory)
        {
            UpgradeStructure(GetInfo(territory).launderingOperation);
        }

        public void UpgradeDistributionNetwork(Territory territory)
        {
            UpgradeStructure(GetInfo(territory).distributionNetwork);
        }

        #endregion Upgrade

        #region Efficiency

        public int GetTotalProduction()
        {
            int production = 0;

            foreach (TerritoryStructureInfo info in Structures.Values)
            {
                if (info.laboratory != null)
                {
                    production += info.laboratory.Rate;
                }
            }

            return production;
        }

        public int GetTotalDistribution()
        {
            int distribution = 0;

            foreach (TerritoryStructureInfo info in Structures.Values)
            {
                if (info.distributionNetwork != null)
                {
                    distribution += info.distributionNetwork.Rate;
                }
            }

            return distribution;
        }

        public int GetTotalLaundering()
        {
            int laundering = 0;

            foreach (TerritoryStructureInfo info in Structures.Values)
            {
                if (info.launderingOperation != null)
                {
                    laundering += info.launderingOperation.Rate;
                }
            }

            return laundering;
        }

        #endregion Efficiency

        public TerritoryStructureInfo GetInfo(Territory territory)
        {
            if (!Structures.ContainsKey(territory))
            {
                Structures.Add(territory, new TerritoryStructureInfo());
            }

            return Structures[territory];
        }
    }
}
