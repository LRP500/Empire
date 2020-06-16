using TMPro;
using Tools.Time;
using UnityEngine;

namespace Empire
{
    public class TerritoryActionInfoDealUI : TerritoryActionInfoUI
    {
        [SerializeField]
        private TextMeshProUGUI _title = null;

        [SerializeField]
        private KeyValueItemUI _quantityKeyValue = null;

        [SerializeField]
        private KeyValueItemUI _sellingPriceKeyValue = null;

        [SerializeField]
        private TextMeshProUGUI _timer = null;

        [SerializeField]
        private DealManager _dealManager = null;

        private Territory _territory = null;

        private DealManager.TerritoryDealInfo _dealInfo = null;

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
                // Attributes
                _quantityKeyValue.SetValue(_dealInfo.currentOffer.Quantity.ToString());
                _sellingPriceKeyValue.SetValue(_dealInfo.currentOffer.SellingPrice.ToString());

                // Time
                float remainingTime = _dealInfo.currentOffer.RemainingTime;
                if (_dealManager.IsTurnBased)
                {
                    string time = remainingTime.ToString();
                    _timer.text = remainingTime <= 3 ? $"<color=red>{time} Turns" : $"{time} Turns";
                }
                else
                {
                    string time = TimeUtility.Format(remainingTime);
                    _timer.text = _dealInfo.currentOffer.RemainingTime <= 31 ? $"<color=red>{time}" : time;
                }
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
