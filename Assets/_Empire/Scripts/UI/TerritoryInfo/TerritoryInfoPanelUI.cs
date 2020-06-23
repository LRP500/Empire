using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using static Empire.StructureManager;

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

        [SerializeField]
        private GameObject _upgradeContainer = null;

        private void Awake()
        {
            Close();

            _hoveredTerritory.Subscribe(Refresh);
        }

        private void OnDestroy()
        {
            _hoveredTerritory.Unsubscribe(Refresh);
        }

        private void Refresh()
        {
            if (!_hoveredTerritory?.Value || _hoveredTerritory.Value.State is TerritoryStateUnreachable)
            {
                Close();
                return;
            }

            _territoryName.text = _hoveredTerritory.Value.gameObject.name;

            _territoryState.text = _hoveredTerritory.Value.State.Name;
            _territoryState.color = _hoveredTerritory.Value.State.Color;

            if (_hoveredTerritory.Value.State is TerritoryStateControlled)
            {
                TerritoryStructureInfo info = _structureManager.GetInfo(_hoveredTerritory.Value);

                _laboratoryLevel.text = $"{info.laboratory.Level}/{info.laboratory.MaxLevel}";
                _distributionLevel.text = $"{info.distributionNetwork.Level}/{info.distributionNetwork.MaxLevel}";
                _launderingLevel.text = $"{info.launderingOperation.Level}/{info.launderingOperation.MaxLevel}";

                _laboratoryLevel.color = info.laboratory.IsMaxLevel() ? Color.green : Color.white;
                _distributionLevel.color = info.distributionNetwork.IsMaxLevel() ? Color.green : Color.white;
                _launderingLevel.color = info.launderingOperation.IsMaxLevel() ? Color.green : Color.white;

                _upgradeContainer.SetActive(true);
            }
            else
            {
                _upgradeContainer.SetActive(false);
            }

            Open();
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
