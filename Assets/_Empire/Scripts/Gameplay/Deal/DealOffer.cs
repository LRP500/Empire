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

        public bool TurnBased { get; private set; } = false;

        public DealOffer(Territory territory, DealOfferSettings settings, bool turnBased)
        {
            _settings = settings;
            _territory = territory;
            TurnBased = turnBased;

            Randomize();
        }

        private void Randomize()
        {
            _quantity = Random.Range(_settings.QuantityMin, _settings.QuantityMax + 1);
            _sellingPrice = Random.Range(_settings.SellingPriceMin, _settings.SellingPriceMax + 1);
            _remainingTime = Random.Range(_settings.OfferDurationMin, _settings.OfferDurationMax + 1);

            if (TurnBased)
            {
                _remainingTime = Mathf.FloorToInt(_remainingTime / 10);
            }
        }

        public void Refresh()
        {
            _remainingTime -= TurnBased ? 1 : Time.deltaTime;
        }

        public bool HasTimedOut()
        {
            return _remainingTime <= (TurnBased ? 0 : 1);
        }
    }
}