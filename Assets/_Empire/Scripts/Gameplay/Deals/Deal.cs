using System;
using Random = UnityEngine.Random;

namespace Empire
{
    [Serializable]
    public class Deal
    {
        private DealSettings _settings = null;

        private int _negociationAttempts = 0;

        public int MethQuantity { get; private set; } = 0;
        public int MethSellingPrice { get; private set; } = 0;

        public Deal(DealSettings settings)
        {
            _settings = settings;
            MethQuantity = Random.Range(_settings.MethQuantityMin, _settings.MethQuantityMax + 1);
            MethSellingPrice = Random.Range(_settings.MethSellingPriceMin, _settings.MethSellingPriceMax + 1);
        }
    }
}