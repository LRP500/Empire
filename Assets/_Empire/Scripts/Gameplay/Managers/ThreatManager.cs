using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Threat Manager")]
    public class ThreatManager : ScriptableManager<ThreatManager>
    {
        [System.Serializable]
        public struct ThreatModifier
        {
            public bool isPercentage;
            public bool value;
        }

        [SerializeField]
        private Resource _threat = null;
        public Resource Threat => _threat;

        [SerializeField]
        private int _tickIncrement = 1;

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
            _threat.Increment(_tickIncrement);
        }
    }
}
