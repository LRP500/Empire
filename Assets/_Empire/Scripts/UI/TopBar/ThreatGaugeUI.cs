using Tools.UI;
using UnityEngine;

namespace Empire
{
    public class ThreatGaugeUI : ResourceGauge
    {
        [Space]
        [SerializeField]
        private ThreatManager _threatManager = null;

        private void OnDestroy()
        {
            _threatManager.Threat.UnregisterOnCurrentValueChanged(Refresh);
        }

        protected override void Initialize()
        {
            SetMax(_threatManager.Threat.Max);
            SetCurrent(_threatManager.Threat.Initial, false);
            _threatManager.Threat.RegisterOnCurrentValueChanged(Refresh);
        }

        private void Refresh(int value)
        {
            SetCurrent(value, true);
        }

        protected override void RefreshText()
        {
            ValueText.gameObject.SetActive(IsCritical());
        }

        protected override bool IsCritical()
        {
            float ratio = 1f / (MaximumValue / CurrentValue);
            return ratio >= (_criticalTreshold / 100);
        }
    }
}