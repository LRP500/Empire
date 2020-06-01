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
        private Resource _threat = null;
        public Resource Threat => _threat;

        [SerializeField]
        private int _initialIncrement = 1;

        [SerializeField]
        private int _cashSliceIncrement = 100000;

        [SerializeField]
        private List<ThreatModifier> _threatModifiers = null;

        private System.Action<float> OnThreatChanged = null;

        public override void Initialize()
        {
            _threat.Initialize();

            EventManager.Instance.Subscribe(GameplayEvent.CashSpent, ProcessCashSpendings);
        }

        public override void Clear()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.CashSpent, ProcessCashSpendings);
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

            IncrementThreat(increment);
        }

        public void IncrementThreat(int amount)
        {
            _threat.Increment(amount);

            OnThreatChanged?.Invoke(_threat);
        }

        private void ProcessCashSpendings(object arg)
        {
            int cashSpent = (int)arg;

            // The result of an int division will always floored (decimals are truncated basically)
            int threatGenerated = cashSpent / _cashSliceIncrement;

            Debug.Log("Threat generated " + threatGenerated);

            IncrementThreat(threatGenerated);
        }

        #region Callbacks

        public void RegisterOnThreatChanged(System.Action<float> callback)
        {
            OnThreatChanged += callback;
        }

        public void UnregisterOnThreatChanged(System.Action<float> callback)
        {
            OnThreatChanged -= callback;
        }

        #endregion Callbacks
    }
}
