using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class TerritoryActionWheelUI : PanelUI
    {
        [SerializeField]
        private Camera _mainCamera = null;

        [SerializeField]
        private TerritoryActionUI _actionItemPrefab = null;

        [SerializeField]
        private Transform _actionContainer = null;

        [SerializeField]
        private Transform _infoContainer = null;

        private Territory _currentTerritory = null;

        private List<TerritoryActionUI> _currentActions = null;

        private void Awake()
        {
            EventManager.Instance.Subscribe(GameplayEvent.TerritorySecondarySelect, OnTerritorySelected);
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.TerritorySecondarySelect, OnTerritorySelected);
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

        public void Open(Territory target)
        {
            Initialize(target);

            Open();
        }

        private void Initialize(Territory territory)
        {
            _currentTerritory = territory;

            SetActions();
            SetPosition();
        }

        private void SetActions()
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
            transform.position = _mainCamera.WorldToScreenPoint(_currentTerritory.transform.position);
        }

        private void OnTerritorySelected(object arg)
        {
            Territory target = arg as Territory;

            if (target.State.Actions.Count > 0)
            {
                Open(arg as Territory);
            }
        }

        private void Clear()
        {
            _currentActions = _currentActions ?? new List<TerritoryActionUI>();

            foreach (TerritoryActionUI item in _currentActions)
            {
                Destroy(item.InfoPanel?.gameObject);
                Destroy(item.gameObject);
            }

            _currentActions.Clear();
        }

        public override void Close()
        {
            base.Close();

            _currentTerritory = null;

            Clear();
        }
    }
}
