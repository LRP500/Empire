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

        [SerializeField]
        private DealManager _dealManager = null;

        private Territory _territory = null;

        public override void Initialize(TerritoryAction action, Territory territory)
        {
            base.Initialize(action, territory);

            _territory = territory;
        }

        private void Update()
        {
            DealManager.TerritoryDealInfo info = _dealManager.GetInfo(_territory);

            if (info.currentOffer != null)
            {
                _quantityKeyValue.SetValue(info.currentOffer.Quantity.ToString());
                _sellingPriceKeyValue.SetValue(info.currentOffer.SellingPrice.ToString());

                string time = TimeUtility.Format(info.currentOffer.RemainingTime);
                _timer.text = info.currentOffer.RemainingTime <= 31 ? $"<color=red>{time}" : time;
            }
        }
    }
}
