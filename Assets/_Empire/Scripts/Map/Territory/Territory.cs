using Sirenix.OdinInspector;
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
        private TerritoryState _territoryStateUnreachable = null;

        [SerializeField]
        private TerritoryState _territoryStateUndisputed = null;

        [SerializeField]
        private TerritoryState _territoryStateControlled = null;

        [SerializeField]
        private TerritoryState _territoryStateInDeal = null;

        [SerializeField]
        private TerritoryState _territoryStateRival = null;

        [SerializeField, ReadOnly]
        private TerritoryState _state = null; 
        public TerritoryState State => _state;

        [Space]

        [Header("Context")]

        [SerializeField]
        private TerritoryListVariable _runtimeTerritories = null;

        [SerializeField]
        private List<Territory> _neighbors = null;

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
            _state = Instantiate(state);
            State.SetContext(this);
            Debug.Log($"[{name}] Transition to {State.GetType().Name}");
        }

        private void Update()
        {
            State?.Refresh();
            State?.RefreshVisualState();
        }

        public void SetControlled()
        {
            TransitionTo(_territoryStateControlled);

            foreach (Territory neighbor in _neighbors)
            {
                if (neighbor.State is TerritoryStateUnreachable)
                {
                    neighbor.SetRival();
                }
            }
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

        public void SetUnreachable()
        {
            TransitionTo(_territoryStateUnreachable);
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
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                EventManager.Instance.Trigger(GameplayEvent.TerritorySelected, this);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                State?.Select();
            }
        }

        #endregion UI Events
    }
}
