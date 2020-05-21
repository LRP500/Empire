using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Empire
{
    public class Territory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("States")]

        [SerializeField]
        private TerritoryState _territoryStateUndisputed = null;

        [SerializeField]
        private TerritoryState _territoryStateControlled = null;

        [SerializeField]
        private TerritoryState _territoryStateInDeal = null;

        [SerializeField]
        private TerritoryState _territoryStateRival = null;

        [Space]

        [SerializeField]
        private List<Territory> _neighbors = null;

        [SerializeField]
        private TerritoryListVariable _runtimeTerritories = null;

        public TerritoryState State { get; private set; } = null;

        public SpriteRenderer Renderer { get; private set; } = null;

        private void Awake()
        {
            Renderer = GetComponent<SpriteRenderer>();
            _runtimeTerritories.Add(this);
        }

        private void OnDestroy()
        {
            _runtimeTerritories.Remove(this);
        }

        public void TransitionTo(TerritoryState state)
        {
            State = Instantiate(state);
            State.SetContext(this);
            Debug.Log($"[{name}] Transition to {State.GetType().Name}");
        }

        private void Update()
        {
            State?.UpdateVisualState();
        }

        public void SetControlled()
        {
            TransitionTo(_territoryStateControlled);
        }

        public void SetUndisputed()
        {
            TransitionTo(_territoryStateUndisputed);
        }

        public void SetInDeal()
        {
            TransitionTo(_territoryStateInDeal);
        }

        public void SetRival()
        {
            TransitionTo(_territoryStateRival);
        }

        #region UI Events

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            State?.Select();
            EventManager.Instance.Trigger(GameplayEvent.TerritorySelected, this);
        }

        #endregion UI Events
    }
}
