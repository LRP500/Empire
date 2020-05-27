using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Settings/Structure Settings")]
    public class StructureSettings : ScriptableObject
    {
        [SerializeField]
        private int _maxLevel = 3;
        public int MaxLevel => _maxLevel;

        [SerializeField]
        private int _startingLevel = 1;
        public int StartingLevel => _startingLevel;

        [Header("Efficiency")]
        [SerializeField]
        private int _initialRate = 0;
        public int InitialRate => _initialRate;

        [SerializeField]
        private int _rateIncrement = 0;
        public int RateIncrement => _rateIncrement;

        [SerializeField]
        private bool _isRateIncrementPercentage = false;
        public bool IsRateIncrementPercentage => _isRateIncrementPercentage;

        [Header("Price")]
        [SerializeField]
        private int _initialPrice = 0;
        public int InitialPrice => _initialPrice;

        [SerializeField]
        private int _priceIncrement = 0;
        public int PriceIncrement => _priceIncrement;

        [SerializeField]
        private bool _isPriceIncrementPercentage = true;
        public bool IsPriceIncrementPercentage => _isPriceIncrementPercentage;
    }
}
