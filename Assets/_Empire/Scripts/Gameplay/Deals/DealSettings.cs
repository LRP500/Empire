using Tools.Variables;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Deals/Deal Offer Settings")]
    public class DealSettings : ScriptableObject
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
    }
}
