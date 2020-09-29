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
        private TerritoryVariable _hoveredTerritory = null;

        [SerializeField]
        private StructureManager _structureManager = null;

        [Space]
        [SerializeField]
        private TextMeshProUGUI _territoryName = null;

        [SerializeField]
        private TextMeshProUGUI _territoryState = null;


        [SerializeField]
        private TextMeshProUGUI _laboratoryLevel = null;

        [SerializeField]
        private TextMeshProUGUI _distributionLevel = null;

        [SerializeField]
        private TextMeshProUGUI _launderingLevel = null;

        [Space]

        [SerializeField]
        private GameObject _upgradeContainer = null;

        [SerializeField]
        private TerritoryTakeOverOddDisplay _takeOverOdds = null;

        [SerializeField]
        private TerritoryDealDisplay _currentDeal = null;

        private Territory _territory = null;

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
            if (!_hoveredTerritory?.Value || _hoveredTerritory.Value.State is TerritoryStateUnreachable)
            {
                Close();
                return;
            }

            ResetToDefault();

            _territory = _hoveredTerritory.Value;
            _territoryName.text = _territory.gameObject.name;
            _territoryState.text = _territory.State.Name;
            _territoryState.color = _territory.State.Color;

            // Controlled
            if (_hoveredTerritory.Value.State is TerritoryStateControlled)
            {
                TerritoryStructureInfo info = _structureManager.GetInfo(_territory);

                _laboratoryLevel.text = $"{info.laboratory.Level}/{info.laboratory.MaxLevel}";
                _distributionLevel.text = $"{info.distributionNetwork.Level}/{info.distributionNetwork.MaxLevel}";
                _launderingLevel.text = $"{info.launderingOperation.Level}/{info.launderingOperation.MaxLevel}";

                _laboratoryLevel.color = info.laboratory.IsMaxLevel() ? Color.green : Color.white;
                _distributionLevel.color = info.distributionNetwork.IsMaxLevel() ? Color.green : Color.white;
                _launderingLevel.color = info.launderingOperation.IsMaxLevel() ? Color.green : Color.white;

                _upgradeContainer.SetActive(true);
            }
            // Rival
            else if (_hoveredTerritory.Value.State is TerritoryStateRival)
            {
                // Odds display
                _takeOverOdds.gameObject.SetActive(true);
                _takeOverOdds.Initialize(new TakeOverInfo(_territory, _hoveredTerritory.Value));

                // Deal offer display
                _currentDeal.gameObject.SetActive(true);
                _currentDeal.Initialize(GameplayContext.Instance.dealManager.GetInfo(_territory));
            }
            // In Deal    
            else if (_hoveredTerritory.Value.State is TerritoryStateInDeal)
            {
                _currentDeal.gameObject.SetActive(true);
                _currentDeal.Initialize(GameplayContext.Instance.dealManager.GetInfo(_territory));
            }

            Open();
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
