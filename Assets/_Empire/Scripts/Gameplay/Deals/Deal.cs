using System;
using Random = UnityEngine.Random;

namespace Empire
{
    [Serializable]
    public class Deal
    {
        private DealSettings _settings = null;

        private int _negociationAttempts = 0;
        private float _remainingTime = 0;
        private bool _accepted = false;

        public int Quantity { get; private set; } = 0;
        public int SellingPrice { get; private set; } = 0;

        public Deal(DealSettings settings)
        {
            _settings = settings;

            Randomize();
        }

        private void Randomize()
        {
            Quantity = Random.Range(_settings.QuantityMin, _settings.QuantityMax + 1);
            SellingPrice = Random.Range(_settings.SellingPriceMin, _settings.SellingPriceMax + 1);
        }
    }
}