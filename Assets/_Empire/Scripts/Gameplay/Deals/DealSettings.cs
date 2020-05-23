using Tools.Variables;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Deals/Deal Settings")]
    public class DealSettings : ScriptableObject
    {
        [SerializeField]
        private IntVariable _methSellingPriceMin = null;
        public int MethSellingPriceMin => _methSellingPriceMin.Value;

        [SerializeField]
        private IntVariable _methSellingPriceMax = null;
        public int MethSellingPriceMax => _methSellingPriceMax.Value;

        [SerializeField]
        private int _methQuantityMin = 10;
        public int MethQuantityMin => _methQuantityMin;

        [SerializeField]
        private int _methQuantityMax = 200;
        public int MethQuantityMax => _methQuantityMax;
    }
}
