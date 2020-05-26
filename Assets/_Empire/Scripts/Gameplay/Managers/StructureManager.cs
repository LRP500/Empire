﻿using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Structure Manager")]
    public class StructureManager : SingletonScriptableObject<StructureManager>
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

        public void Initialize()
        {
            Structures = new Dictionary<Territory, TerritoryStructureInfo>();
        }

        public void SetupStructures(Territory territory)
        {
            TerritoryStructureInfo info = new TerritoryStructureInfo()
            {
                laboratory = new Laboratory(_laboratorySettings),
                launderingOperation = new LaunderingOperation(_launderingOperationSettings),
                distributionNetwork = new DistributionNetwork(_distributionNetworkSettings)
            };

            Structures.Add(territory, info);
        }

        public void ClearStructures(Territory territory)
        {
            Structures.Remove(territory);
        }

        public void UpgradeLaboratories(Territory territory)
        {
            GetInfo(territory).laboratory.Upgrade();
        }

        public void UpgradeLaunderingOperation(Territory territory)
        {
            GetInfo(territory).launderingOperation.Upgrade();
        }

        public void UpgradeDistributionNetwork(Territory territory)
        {
            GetInfo(territory).distributionNetwork.Upgrade();
        }

        #region Efficiency

        public int GetTotalProduction()
        {
            int production = 0;

            foreach (TerritoryStructureInfo info in Structures.Values)
            {
                if (info.laboratory != null)
                {
                    production += info.laboratory.Efficiency;
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
                    distribution += info.distributionNetwork.Efficiency;
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
                    laundering += info.launderingOperation.Efficiency;
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
