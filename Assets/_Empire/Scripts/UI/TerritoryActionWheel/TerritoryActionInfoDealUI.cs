using UnityEngine;

namespace Empire
{
    public class TerritoryActionInfoDealUI : TerritoryActionInfoUI
    {
        [SerializeField]
        private KeyValueItemUI _quantityKeyValue = null;

        [SerializeField]
        private KeyValueItemUI _sellingPriceKeyValue = null;

        public override void Initialize(TerritoryAction action, Territory territory)
        {
            TerritoryActionMakeDeal deal = action as TerritoryActionMakeDeal;

            _quantityKeyValue.SetValue(territory.CurrentDealOffer.Quantity.ToString());
            _sellingPriceKeyValue.SetValue(territory.CurrentDealOffer.SellingPrice.ToString());
        }
    }
}
