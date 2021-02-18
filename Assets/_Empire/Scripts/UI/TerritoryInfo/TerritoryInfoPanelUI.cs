using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using static Empire.StructureManager;
using static Empire.TakeOverManager;

namespace Empire
{
    public class TerritoryInfoPanelUI : PanelUI
    {
        [SerializeField]
        private TerritoryVariable _hoveredTerritory;

        [SerializeField]
        private StructureManager _structureManager;

        [Space]
        [SerializeField]
        private TextMeshProUGUI _territoryName;

        [SerializeField]
        private TextMeshProUGUI _territoryState;


        [SerializeField]
        private TextMeshProUGUI _laboratoryLevel;

        [SerializeField]
        private TextMeshProUGUI _distributionLevel;

        [SerializeField]
        private TextMeshProUGUI _launderingLevel;

        [Space]

        [SerializeField]
        private GameObject _upgradeContainer;

        [SerializeField]
        private TerritoryTakeOverOddDisplay _takeOverOdds;

        [SerializeField]
        private TerritoryDealDisplay _currentDeal;

        private Territory _territory;

        private void Awake()
        {
            Close();

            _hoveredTerritory.Subscribe(Refresh);
        }

        private void OnDestroy()
        {
            _hoveredTerritory.Unsubscribe(Refresh);
        }

        private void Update()
        {
            if (IsOpen)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            if (!_hoveredTerritory ||
                !_hoveredTerritory.Value ||
                _hoveredTerritory.Value.State is TerritoryStateUnreachable)
            {
                Close();
                return;
            }

            ResetToDefault();
            RefreshTerritoryInfo();

            // Controlled
            if (_hoveredTerritory.Value.State is TerritoryStateControlled)
            {
                RefreshUpgrades();
            }
            // Rival
            else if (_hoveredTerritory.Value.State is TerritoryStateRival)
            {
                RefreshTakeOverOdds();
                RefreshDeal();
            }
            // In Deal    
            else if (_hoveredTerritory.Value.State is TerritoryStateInDeal)
            {
                RefreshTakeOverOdds();
                RefreshDeal();
            }

            Open();
        }

        private void RefreshTerritoryInfo()
        {
            _territory = _hoveredTerritory.Value;
            _territoryName.text = _territory.gameObject.name;
            _territoryState.text = _territory.State.Name;
            _territoryState.color = _territory.State.Color;
        }

        private void RefreshUpgrades()
        {
            TerritoryStructureInfo info = _structureManager.GetInfo(_territory);

            _laboratoryLevel.text = $"{info.laboratory.Level}/{info.laboratory.MaxLevel}";
            _laboratoryLevel.color = info.laboratory.IsMaxLevel() ? Color.green : Color.white;
            
            _distributionLevel.text = $"{info.distributionNetwork.Level}/{info.distributionNetwork.MaxLevel}";
            _distributionLevel.color = info.distributionNetwork.IsMaxLevel() ? Color.green : Color.white;
            
            _launderingLevel.text = $"{info.launderingOperation.Level}/{info.launderingOperation.MaxLevel}";
            _launderingLevel.color = info.launderingOperation.IsMaxLevel() ? Color.green : Color.white;

            _upgradeContainer.SetActive(true);
        }

        private void RefreshTakeOverOdds()
        {
            _takeOverOdds.gameObject.SetActive(true);
            _takeOverOdds.Initialize(new TakeOverInfo(_territory, _hoveredTerritory.Value));
        }

        private void RefreshDeal()
        {
            _currentDeal.gameObject.SetActive(true);
            _currentDeal.Initialize(GameplayContext.Instance.dealManager.GetInfo(_territory));
        }

        private void ResetToDefault()
        {
            _upgradeContainer.SetActive(false);
            _currentDeal.gameObject.SetActive(false);
            _takeOverOdds.gameObject.SetActive(false);
        }

        #region Overrides

        [Button]
        protected override void Display()
        {
            _group.alpha = 1;
            _group.blocksRaycasts = false;
            _group.interactable = false;
        }

        #endregion Overrides
    }
}
