using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Threat Manager")]
    public class ThreatManager : ScriptableManager<ThreatManager>
    {
        [System.Serializable]
        public class ThreatModifier
        {
            public int value = 0;
            public bool active = false;
            public bool onTick = true;

            [HideIf(nameof(onTick))]
            public string gameEvent = string.Empty;

            public int Calculate() => value;
        }

        [SerializeField]
        private Resource _threat = null;
        public Resource Threat => _threat;

        [SerializeField]
        private int _initialIncrement = 1;

        [SerializeField]
        private List<ThreatModifier> _modifiers = null;

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

            foreach (ThreatModifier mod in _modifiers)
            {
                if (mod.active && mod.onTick)
                {
                    increment += mod.Calculate();
                }
            }

            _threat.Increment(increment);
        }
    }
}
