using UnityEngine;

namespace Empire
{
    public abstract class Structure
    {
        private StructureSettings _settings = null;

        public int Level { get; private set; } = 0;
        public int Rate { get; protected set; } = 0;
        public int Price { get; protected set; } = 0;

        public int MaxLevel => _settings.MaxLevel;

        public Structure(StructureSettings settings)
        {
            _settings = settings;

            Initialize();
        }

        private void Initialize()
        {
            Price = _settings.InitialPrice;

            int startingLevel = Mathf.Min(_settings.StartingLevel, _settings.MaxLevel);
            for (int i = 0; i < startingLevel; i++)
            {
                Upgrade();
            }
        }

        public void Upgrade()
        {
            if (!IsMaxLevel())
            {
                Rate = GetNextLevelRate();
                Price = GetNextLevelPrice();
                Level += 1;
            }
        }

        public void SetLevel(int level)
        {
            while (!IsMaxLevel() && Level != level)
            {
                Upgrade();
            }
        }

        public bool IsMaxLevel()
        {
            return Level == _settings.MaxLevel;
        }

        private int GetNextLevelPrice()
        {
            if (_settings.IsPriceIncrementPercentage)
            {
                return Price * (_settings.PriceIncrement * (Level + 1)) / 100;
            }
            else
            {
                return Price + _settings.PriceIncrement;
            }
        }

        public int GetNextLevelRate()
        {
            // Rate is null at level 0 
            if (Level == 0)
            {
                return _settings.InitialRate;
            }

            if (_settings.IsRateIncrementPercentage)
            {
                return Rate * (_settings.RateIncrement * (Level + 1)) / 100;
            }
            else
            {
                return Rate + _settings.RateIncrement;
            }
        }
    }
}
