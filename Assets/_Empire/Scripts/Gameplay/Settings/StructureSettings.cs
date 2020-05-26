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

        [SerializeField]
        private int _initialEfficiency = 0;
        public int InitialEfficiency => _initialEfficiency;

        [SerializeField]
        private int _efficiencyLevelIncrement = 0;
        public int EfficiencyLevelIncrement => _efficiencyLevelIncrement;

        [SerializeField]
        private bool _percentageIncrement = false;
        public bool PercentageIncrement => _percentageIncrement;
    }
}
