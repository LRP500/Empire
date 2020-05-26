using UnityEngine;

namespace Empire
{
    public abstract class Structure
    {
        public int Level { get; private set; } = 0;
        public int Efficiency { get; protected set; } = 0;

        public int MaxLevel => _settings.MaxLevel;

        private StructureSettings _settings = null;

        public Structure(StructureSettings settings)
        {
            _settings = settings;
            Level = Mathf.Clamp(_settings.StartingLevel, 0, _settings.MaxLevel);
            Efficiency = _settings.InitialEfficiency;
        }

        public void Upgrade()
        {
            if (!IsMaxLevel())
            {
                Level += 1;

                if (_settings.PercentageIncrement)
                {
                    Efficiency += Efficiency * (_settings.EfficiencyLevelIncrement / 100);
                }
                else
                {
                    Efficiency += _settings.EfficiencyLevelIncrement;
                }
            }
        }

        public bool IsMaxLevel()
        {
            return Level == _settings.MaxLevel;
        }
    }
}
