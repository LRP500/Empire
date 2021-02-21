using TMPro;
using Tools.Utilities;
using UnityEngine;
using static Empire.StructureManager;

namespace Empire
{
    public class TerritoryActionInfoStructureUI : TerritoryActionInfoUI
    {
        private enum Type
        {
            Laboratory,
            Laundering,
            Distribution
        }

        [SerializeField]
        private Type _type;

        [SerializeField]
        private TextMeshProUGUI _level;

        [SerializeField]
        private KeyValueItemUI _rate;

        [SerializeField]
        private KeyValueItemUI _price;

        [SerializeField]
        private StructureManager _structureManager;

        [SerializeField]
        private ResourceManager _resourceManager;

        private Structure _structure;

        private string _abbreviatedPrice = string.Empty;

        public override void Initialize(TerritoryAction action, Territory territory)
        {
            base.Initialize(action, territory);

            TerritoryStructureInfo info = _structureManager.GetInfo(territory);

            switch (_type)
            {
                case Type.Laboratory: _structure = info.laboratory; break;
                case Type.Laundering: _structure = info.launderingOperation; break;
                case Type.Distribution: _structure = info.distributionNetwork; break;
                default: break;
            }
        }

        private void Update()
        {
            RefreshPriceText();
        }

        private void RefreshPriceText()
        {
            if (!_structure.IsMaxLevel() && !_abbreviatedPrice.Equals(string.Empty))
            {
                if (!_resourceManager.CanSpend(_structure.Price))
                {
                    _price.SetValue($"<color=red>{_abbreviatedPrice}");
                }
                else if (_resourceManager.Bank < _structure.Price)
                {
                    _price.SetValue($"<color=orange>RISKY {_abbreviatedPrice}");
                }
                else
                {
                    _price.SetValue(_abbreviatedPrice);
                }
            }
        }

        private void Refresh()
        {
            if (_structure.IsMaxLevel())
            {
                _level.text = "<color=red>MAX";
                _price.gameObject.SetActive(false);
                _rate.SetValue(AbbreviationUtility.Format(_structure.Rate));
            }
            else
            {
                _level.text = $"{_structure.Level}/{_structure.MaxLevel}";
                
                _rate.SetValue($"{AbbreviationUtility.Format(_structure.Rate)} > <color=green>" +
                               $"{AbbreviationUtility.Format(_structure.GetNextLevelRate())}");

                _price.gameObject.SetActive(true);

                _abbreviatedPrice = AbbreviationUtility.Format(_structure.Price);
            }
        }

        public override void Open()
        {
            Refresh();

            base.Open();
        }
    }
}
