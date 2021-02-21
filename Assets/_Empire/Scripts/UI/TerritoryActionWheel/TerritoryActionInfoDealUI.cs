using TMPro;
using Tools.Time;
using UnityEngine;

namespace Empire
{
    public class TerritoryActionInfoDealUI : TerritoryActionInfoUI
    {
        [SerializeField]
        private TextMeshProUGUI _title;

        [SerializeField]
        private KeyValueItemUI _quantityKeyValue;

        [SerializeField]
        private KeyValueItemUI _sellingPriceKeyValue;

        [SerializeField]
        private TextMeshProUGUI _timer;

        [SerializeField]
        private DealManager _dealManager;

        private Territory _territory;

        private DealManager.TerritoryDealInfo _dealInfo;

        public override void Initialize(TerritoryAction action, Territory territory)
        {
            base.Initialize(action, territory);

            _dealInfo = _dealManager.GetInfo(territory); 

            _territory = territory;
            _title.text = _dealInfo.activeDeal == null ? "Current Offer" : "Current Deal";
        }

        private void Update()
        {
            if (_dealInfo.currentOffer != null)
            {
                _quantityKeyValue.SetValue(_dealInfo.currentOffer.Quantity.ToString());
                _sellingPriceKeyValue.SetValue(_dealInfo.currentOffer.SellingPrice.ToString());

                string time = TimeUtility.Format(_dealInfo.currentOffer.RemainingTime);
                _timer.text = _dealInfo.currentOffer.RemainingTime <= 31 ? $"<color=red>{time}" : time;
            }
            else if (_dealInfo.activeDeal != null)
            {
                _quantityKeyValue.SetValue(_dealInfo.activeDeal.Quantity.ToString());
                _sellingPriceKeyValue.SetValue(_dealInfo.activeDeal.SellingPrice.ToString());
                _timer.gameObject.SetActive(false);
            }
        }
    }
}
