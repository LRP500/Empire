using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Empire
{
    public class Territory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        #region Serialized Fields

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
        public List<Territory> Neighbors => _neighbors;

        #endregion Serialized Fields

        #region Properties

        public SpriteRenderer Renderer { get; private set; } = null;

        public DealOffer CurrentDealOffer { get; private set; } = null;
        public Deal CurrentDeal { get; private set; } = null;

        public List<LaunderingOperation> LaunderingOperations { get; private set; } = null;
        public List<Laboratory> Laboratories { get; private set; } = null;

        #endregion Properties

        #region MonoBehaviour

        private void Awake()
        {
            Renderer = GetComponent<SpriteRenderer>();
            _runtimeTerritories.Add(this);
        }

        private void OnDestroy()
        {
            _runtimeTerritories.Remove(this);
        }

        private void Update()
        {
            State?.Refresh();
            State?.RefreshVisualState();
        }

        #endregion MonoBehaviour

        #region Structures

        public void AddLaboratory(Laboratory laboratory)
        {
            Laboratories = Laboratories ?? new List<Laboratory>();
            Laboratories.Add(laboratory);
        }

        public void AddLaunderingOperation(LaunderingOperation operation)
        {
            LaunderingOperations = LaunderingOperations ?? new List<LaunderingOperation>();
            LaunderingOperations.Add(operation);
        }

        public void DestroyAllStructures()
        {
            Laboratories.Clear();
            LaunderingOperations.Clear();
        }

        #endregion Structures

        #region Deal

        public void AcceptDealOffer(DealListVariable deals)
        {
            CurrentDeal = CurrentDealOffer;
            CurrentDealOffer = null;

            deals.Add(CurrentDeal);

            SetInDeal();
        }

        public void CancelCurrentDeal(DealListVariable deals)
        {
            if (CurrentDeal != null)
            {
                deals.Remove(CurrentDeal);
                CurrentDeal = null;
            }
        }

        public void SetDealOffer(DealOffer offer)
        {
            CurrentDealOffer = offer;
        }

        #endregion Deal

        #region State

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

        public void TransitionTo(TerritoryState state)
        {
            _state = Instantiate(state);
            State.SetContext(this);
            State.OnEnterState();
            Debug.Log($"[{name}] Transition to {State.GetType().Name}");
        }

        #endregion State

        #region UI Events

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                EventManager.Instance.Trigger(GameplayEvent.TerritoryPrimarySelect, this);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                EventManager.Instance.Trigger(GameplayEvent.TerritorySecondarySelect, this);
            }
        }

        #endregion UI Events
    }
}
