using UnityEngine;

namespace Empire
{
    public class TerritoryActionInfoDealUI : TerritoryActionInfoUI
    {
        [SerializeField]
        private KeyValueItemUI _quantityKeyValue = null;

        [SerializeField]
        private KeyValueItemUI _sellingPriceKeyValue = null;

        public override void Initialize(TerritoryAction action)
        {
            TerritoryActionMakeDeal deal = action as TerritoryActionMakeDeal;

            _quantityKeyValue.SetValue(deal.CurrentDealOffer.Quantity.ToString());
            _sellingPriceKeyValue.SetValue(deal.CurrentDealOffer.SellingPrice.ToString());
        }
    }
}
