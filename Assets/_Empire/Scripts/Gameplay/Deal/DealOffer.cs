using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Empire
{
    public class DealOffer : Deal
    {
        [ReadOnly]
        [SerializeField]
        private float _remainingTime;

        public float RemainingTime => _remainingTime;

        public DealOffer(Territory territory, DealOfferSettings settings)
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

        public void Refresh(float elapsed)
        {
            _remainingTime -= elapsed;
        }

        public bool HasTimedOut()
        {
            return _remainingTime <= 1;
        }
    }
}