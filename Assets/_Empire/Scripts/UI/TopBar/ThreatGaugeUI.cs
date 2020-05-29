using Tools.UI;
using UnityEngine;

namespace Empire
{
    public class ThreatGaugeUI : ResourceGauge
    {
        [Space]
        [SerializeField]
        private ThreatManager _threatManager = null;

        private void Awake()
        {
            SetMax(_threatManager.Threat.Max);

            _threatManager.Threat.RegisterOnCurrentValueChanged(Refresh);
        }

        private void OnDestroy()
        {
            _threatManager.Threat.UnregisterOnCurrentValueChanged(Refresh);
        }

        private void Refresh(int value)
        {
            SetCurrent(value, true);
        }

        protected override void RefreshText()
        {
            ValueText.gameObject.SetActive(IsCritical());
        }
    }
}