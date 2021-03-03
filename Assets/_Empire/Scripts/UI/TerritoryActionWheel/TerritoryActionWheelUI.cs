using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class TerritoryActionWheelUI : PanelUI
    {
        #region Serialized Fields

        [SerializeField]
        private CameraVariable _mainCamera;

        [SerializeField]
        private TerritoryActionUI _actionItemPrefab;

        [SerializeField]
        private RectTransform _actionContainer;

        [SerializeField]
        private RectTransform _leftInfoContainer;

        [SerializeField]
        private RectTransform _rightInfoContainer;

        #endregion Serialized Fields

        #region Private Fields

        private readonly Vector2 _margin = default;

        private Territory _currentTerritory;
        
        private List<TerritoryActionUI> _currentActions;
        
        #endregion Private Fields

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();
            EventManager.Instance.Subscribe(GameplayEvent.TerritoryPrimarySelect, OnSelectionCanceled);
            EventManager.Instance.Subscribe(GameplayEvent.TerritorySecondarySelect, OnTerritorySelected);
            EventManager.Instance.Subscribe(GameplayEvent.CancelSecondaryMouseSelect, OnSelectionCanceled);
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.TerritorySecondarySelect, OnTerritorySelected);
            EventManager.Instance.Unsubscribe(GameplayEvent.CancelSecondaryMouseSelect, OnSelectionCanceled);
        }

        private void Update()
        {
            if (_currentTerritory)
            {
                SetPosition();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
        }
        
        #endregion MonoBehaviour

        #region Private Methods

        private void Open(Territory target)
        {
            Initialize(target);

            Open();
        }

        private void Initialize(Territory territory)
        {
            _currentTerritory = territory;

            SetPosition();
            CreateActions();
        }

        private void CreateActions()
        {
            Clear();

            foreach (TerritoryAction action in _currentTerritory.State.Actions)
            {
                TerritoryActionUI actionItem = Instantiate(_actionItemPrefab, _actionContainer);
                actionItem.Initialize(_currentTerritory, action);
                actionItem.RegisterOnActionExecuted(Close);

                Canvas.ForceUpdateCanvases();
                actionItem.GetComponent<ContentSizeFitter>().enabled = false;
                actionItem.GetComponent<ContentSizeFitter>().enabled = true;

                if (action.InfoPanelPrefab)
                {
                    Transform container = GetInfoContainer();
                    TerritoryActionInfoUI infoPanel = Instantiate(action.InfoPanelPrefab, container);
                    infoPanel.Initialize(action, _currentTerritory);
                    actionItem.SetInfoPanel(infoPanel);
                }

                _currentActions.Add(actionItem);
            }
        }

        private Vector3 GetPosition()
        {
            return _mainCamera.Value.WorldToScreenPoint(_currentTerritory.transform.position);
        }

        private void SetPosition()
        {
            Vector3 position = GetPosition();
            Vector2 containerSize = _actionContainer.sizeDelta;
            float min = containerSize.x / 2 + _margin.x; 
            float max = Screen.width - containerSize.x / 2 - _margin.x;
            position.x = Mathf.Clamp(position.x, min, max);
            transform.position = position;
        }

        private void OnTerritorySelected(object arg)
        {
            var target = arg as Territory;

            if (target && target.State.Actions.Count > 0)
            {
                Open(target);
            }
        }

        private void OnSelectionCanceled(object arg)
        {
            Close();
        }

        private void Clear()
        {
            _currentActions ??= new List<TerritoryActionUI>();

            foreach (TerritoryActionUI item in _currentActions)
            {
                if (item.InfoPanel)
                {
                    Destroy(item.InfoPanel.gameObject);
                }

                Destroy(item.gameObject);
            }

            _currentActions.Clear();
        }

        private Transform GetInfoContainer()
        {
            return IsOverflowing(_rightInfoContainer) ? _leftInfoContainer : _rightInfoContainer;
        }

        private bool IsOverflowing(RectTransform rect)
        {
            var corners = new Vector3[4];
            rect.GetWorldCorners(corners);
            var screenRect = new Rect(0, 0, Screen.width - _margin.x / 2, Screen.height - _margin.y / 2);
            return corners.Count(corner => !screenRect.Contains(corner)) > 0;
        }

        #endregion Private Methods

        #region Public Methods

        public override void Close()
        {
            base.Close();

            _currentTerritory = null;

            Clear();
        }

        #endregion Public Methods
    }
}