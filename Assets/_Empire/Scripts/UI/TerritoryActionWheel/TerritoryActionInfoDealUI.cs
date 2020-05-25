using TMPro;
using Tools.Time;
using UnityEngine;

namespace Empire
{
    public class TerritoryActionInfoDealUI : TerritoryActionInfoUI
    {
        [SerializeField]
        private KeyValueItemUI _quantityKeyValue = null;

        [SerializeField]
        private KeyValueItemUI _sellingPriceKeyValue = null;

        [SerializeField]
        private TextMeshProUGUI _timer = null;

        private Territory _territory = null;

        public override void Initialize(TerritoryAction action, Territory territory)
        {
            base.Initialize(action, territory);

            _territory = territory;
        }

        private void Update()
        {
            if (_territory.CurrentDealOffer != null)
            {
                _quantityKeyValue.SetValue(_territory.CurrentDealOffer.Quantity.ToString());
                _sellingPriceKeyValue.SetValue(_territory.CurrentDealOffer.SellingPrice.ToString());
                _timer.text = TimeUtility.Format(_territory.CurrentDealOffer.RemainingTime);
            }
        }
    }
}
