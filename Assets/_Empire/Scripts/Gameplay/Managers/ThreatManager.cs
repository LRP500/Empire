using System.Collections.Generic;
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
        private List<ThreatModifier> _threatModifiers = null;

        private System.Action<float> OnThreatChanged = null;

        public override void Initialize()
        {
            _threat.Initialize();
        }

        public override void Refresh()
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

            Debug.Log(increment);

            _threat.Increment(increment);
        }
    }
}
