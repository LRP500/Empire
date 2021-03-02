using Tools;
using Tools.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Empire
{
    public class ThreatGaugeUI : ResourceGauge, IPointerEnterHandler, IPointerExitHandler
    {
        [Space]
        [SerializeField]
        private ThreatManager _threatManager;

        [SerializeField]
        private Animation _feedbackAnimation;

        [Space]
        [SerializeField]
        private ResourceVariable _hoveredResource;

        private void Awake()
        {
            EventManager.Instance.Subscribe(GameplayEvent.CashSpent, TriggerVisualFeedback);
            EventManager.Instance.Subscribe(GameplayEvent.TakeOverFailed, TriggerVisualFeedback);
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.CashSpent, TriggerVisualFeedback);
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

        private void TriggerVisualFeedback(object arg)
        {
            _feedbackAnimation.Play();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hoveredResource.SetValue(_threatManager.Threat);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hoveredResource.Clear();
        }
    }
}