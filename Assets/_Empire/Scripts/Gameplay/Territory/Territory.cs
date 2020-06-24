using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Empire
{
    public class Territory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        #region Serialized Fields

        [SerializeField, ReadOnly]
        private TerritoryState _state = null; 
        public TerritoryState State => _state;

        [SerializeField]
        private TerritoryListVariable _runtimeTerritories = null;

        [SerializeField]
        private List<Territory> _neighbors = null;
        public List<Territory> Neighbors => _neighbors;

        /// <summary>
        /// Allows checking if controlled without having to cast State.
        /// </summary>
        public bool Controlled { get; set; } = false;

        #endregion Serialized Fields

        private bool _dragging = false;

        #region Properties

        public SpriteRenderer Renderer { get; private set; } = null;

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

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                EventManager.Instance.Trigger(GameplayEvent.TerritoryPrimarySelect, this);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                EventManager.Instance.Trigger(GameplayEvent.TerritorySecondarySelect, this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragging = true;
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

        public void SetState(TerritoryState state)
        {
            _state = state;
        }
    }
}