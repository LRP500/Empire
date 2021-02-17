using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Empire
{
    public class Territory : MonoBehaviour,
        IBeginDragHandler, IEndDragHandler, IDragHandler,
        IPointerEnterHandler, IPointerExitHandler,
        IPointerClickHandler
    {
        #region Serialized Fields

        [SerializeField, ReadOnly]
        private TerritoryState _state; 

        [SerializeField]
        private TerritoryListVariable _runtimeTerritories;

        [SerializeField]
        private List<Territory> _neighbors;
        
        #endregion Serialized Fields

        #region Private Fields

        private bool _dragging;

        #endregion Private Fields

        #region Properties

        public TerritoryState State => _state;
        public List<Territory> Neighbors => _neighbors;

        public SpriteRenderer Renderer { get; private set; }

        /// <summary>
        /// Allows checking if controlled without having to cast State.
        /// </summary>
        public bool IsControlled { get; set; }

        /// <summary>
        /// Allows checking if discovered without having to cast State.
        /// </summary>
        public bool IsDiscovered { get; set; }

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

        public void SetState(TerritoryState state)
        {
            if (_state)
            {
                _state.OnExitState();
            }

            _state = state;
            _state.SetTerritory(this);
            _state.OnEnterState();
        }

        #region UI Events

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_dragging)
            {
                return;
            }

            //if (Input.GetKeyUp(KeyCode.Mouse0))
            //{
            //    EventManager.Instance.Trigger(GameplayEvent.TerritoryPrimarySelect, this);
            //}
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                EventManager.Instance.Trigger(GameplayEvent.TerritorySecondarySelect, this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragging = true;

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (IsControlled)
                {
                    // Initiate take over
                    EventManager.Instance.Trigger(GameplayEvent.TakeOverDragStart, this);
                }
            }

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragging = false;
        }

        /// <summary>
        /// OnDrag has to be implemented to allow OnEndDrag and OnBeginDrag to work.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
        }

        #endregion UI Events
    }
}