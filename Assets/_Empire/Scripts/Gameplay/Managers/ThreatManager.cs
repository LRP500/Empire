using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Threat Manager")]
    public class ThreatManager : ScriptableManager<ThreatManager>
    {
        [SerializeField]
        private Resource _threat;
        public Resource Threat => _threat;

        [SerializeField]
        private int _initialIncrement = 1;

        [SerializeField]
        private int _cashSpentSliceIncrement = 10000;

        [SerializeField]
        [MinMaxSlider(0, 100, ShowFields = true)]
        private Vector2Int _failedTakeOverIncrement;

        [SerializeField]
        private List<ThreatModifier> _threatModifiers;

        public bool MaxThreatReached => _threat.Current >= _threat.Max;

        #region Life Cycle

        public override void Initialize()
        {
            _threat.Initialize();

            EventManager.Instance.Subscribe(GameplayEvent.CashSpent, ProcessCashSpendings);
            EventManager.Instance.Subscribe(GameplayEvent.TakeOverFailed, ProcessFailedTakeOver);
        }

        public override void Clear()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.CashSpent, ProcessCashSpendings);
            EventManager.Instance.Unsubscribe(GameplayEvent.TakeOverFailed, ProcessFailedTakeOver);
        }

        public override void Refresh(float elapsed)
        {
        }

        public override void RefreshOnTick(float elapsed)
        {
            int increment = _initialIncrement;

            foreach (ThreatModifier mod in _threatModifiers)
            {
                if (mod.Evaluate(_context))
                {
                    increment += mod.IncrementModifier;
                }
            }

            IncreaseThreat(increment);
        }

        #endregion Life Cycle

        public void IncreaseThreat(int amount)
        {
            _threat.Increment(amount);
        }

        private void ProcessCashSpendings(object arg)
        {
            int cashSpent = (int)arg;

            // The result of an int division will always floored (decimals are truncated basically)
            int threatGenerated = cashSpent / _cashSpentSliceIncrement;

            IncreaseThreat(threatGenerated);
        }

        private void ProcessFailedTakeOver(object arg)
        {
            IncreaseThreat(Random.Range(_failedTakeOverIncrement.x, _failedTakeOverIncrement.y + 1));
        }
    }
}
