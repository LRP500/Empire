using TMPro;
using Tools.Utilities;
using UnityEngine;
using static Empire.StructureManager;

namespace Empire
{
    public class TerritoryActionInfoStructureUI : TerritoryActionInfoUI
    {
        public enum Type
        {
            Laboratory,
            Laundering,
            Distribution
        }

        [SerializeField]
        private Type _type = default;

        [SerializeField]
        private TextMeshProUGUI _level = null;

        [SerializeField]
        private KeyValueItemUI _rate = null;

        [SerializeField]
        private KeyValueItemUI _price = null;

        [SerializeField]
        private StructureManager _structureManager = null;

        [SerializeField]
        private ResourceManager _resourceManager = null;

        private Structure _structure = null;

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
                _level.text = string.Format("{0}/{1}", _structure.Level, _structure.MaxLevel);

                _rate.SetValue(
                    string.Format("{0} > <color=green>{1}",
                    AbbreviationUtility.Format(_structure.Rate),
                    AbbreviationUtility.Format(_structure.GetNextLevelRate())));

                _price.gameObject.SetActive(true);

                if (!_resourceManager.CanSpend(_structure.Price))
                {
                    _price.SetValue($"<color=red>{AbbreviationUtility.Format(_structure.Price)}");
                }
                else if (_resourceManager.Bank < _structure.Price)
                {
                    _price.SetValue($"<color=orange>RISKY {AbbreviationUtility.Format(_structure.Price)}");
                }
                else
                {
                    _price.SetValue(AbbreviationUtility.Format(_structure.Price));
                }
            }
        }

        public override void Open()
        {
            Refresh();

            base.Open();
        }
    }
}
