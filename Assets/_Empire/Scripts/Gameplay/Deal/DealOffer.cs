using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Empire
{
    public class DealOffer : Deal
    {
        [ReadOnly]
        [SerializeField]
        private float _remainingTime = 0;

        public float RemainingTime => _remainingTime;

        public DealOffer(DealOfferSettings settings, Territory territory)
        {
            _settings = settings;
            _territory = territory;

            Randomize();
        }

        private void Randomize()
        {
            _quantity = Random.Range(_settings.QuantityMin, _settings.QuantityMax + 1);
            _sellingPrice = Random.Range(_settings.SellingPriceMin, _settings.SellingPriceMax + 1);
            _remainingTime = Random.Range(_settings.OfferDurationMin, _settings.OfferDurationMax + 1);
        }

        public void Refresh()
        {
            _remainingTime -= Time.deltaTime;
        }

        public bool HasTimedOut()
        {
            return _remainingTime <= 1;
        }
    }
}