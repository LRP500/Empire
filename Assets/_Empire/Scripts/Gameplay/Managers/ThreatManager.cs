using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Threat Manager")]
    public class ThreatManager : ScriptableManager<ThreatManager>
    {
        [SerializeField]
        private Resource _threat = null;
        public Resource Threat => _threat;

        public System.Action<float> OnThreatChanged = null;

        public override void Initialize()
        {
            _threat.Initialize();
        }

        public override void Refresh()
        {
        }

        public override void RefreshOnTick(float elapsed)
        {
        }
    }
}
