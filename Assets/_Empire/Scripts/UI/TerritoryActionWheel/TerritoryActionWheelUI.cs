using System.Collections.Generic;
using Tools;
using Tools.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class TerritoryActionWheelUI : PanelUI
    {
        [SerializeField]
        private CameraVariable _mainCamera;

        [SerializeField]
        private TerritoryActionUI _actionItemPrefab;

        [SerializeField]
        private Transform _actionContainer;

        [SerializeField]
        private Transform _infoContainer;

        private Territory _currentTerritory;

        private List<TerritoryActionUI> _currentActions;

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

        private void Open(Territory target)
        {
            Initialize(target);

            Open();
        }

        public override void Close()
        {
            base.Close();

            _currentTerritory = null;

            Clear();
        }

        private void Initialize(Territory territory)
        {
            _currentTerritory = territory;

            CreateActions();
            SetPosition();
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
                    TerritoryActionInfoUI infoPanel = Instantiate(action.InfoPanelPrefab, _infoContainer);
                    infoPanel.Initialize(action, _currentTerritory);
                    actionItem.SetInfoPanel(infoPanel);
                }

                _currentActions.Add(actionItem);
            }
        }

        private void SetPosition()
        {
            transform.position = _mainCamera.Value.WorldToScreenPoint(_currentTerritory.transform.position);
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
    }
}
