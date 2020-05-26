using Tools.Variables;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Settings/Deal Offer")]
    public class DealOfferSettings : ScriptableObject
    {
        [SerializeField]
        private IntVariable _sellingPriceMin = null;
        public int SellingPriceMin => _sellingPriceMin.Value;

        [SerializeField]
        private IntVariable _sellingPriceMax = null;
        public int SellingPriceMax => _sellingPriceMax.Value;

        [SerializeField]
        private int _quantityMin = 10;
        public int QuantityMin => _quantityMin;

        [SerializeField]
        private int _quantityMax = 200;
        public int QuantityMax => _quantityMax;

        [SerializeField]
        private int _offerDurationMin = 30;
        public int OfferDurationMin => _offerDurationMin;

        [SerializeField]
        private int _offerDurationMax = 500;
        public int OfferDurationMax => _offerDurationMax;
    }
}