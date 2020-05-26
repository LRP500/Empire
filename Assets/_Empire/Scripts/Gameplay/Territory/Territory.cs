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

        [SerializeField, ReadOnly]
        private TerritoryState _state = null; 
        public TerritoryState State => _state;

        [SerializeField]
        private TerritoryListVariable _runtimeTerritories = null;

        [SerializeField]
        private List<Territory> _neighbors = null;
        public List<Territory> Neighbors => _neighbors;

        #endregion Serialized Fields

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

        public void SetState(TerritoryState state)
        {
            _state = state;
        }
    }
}
