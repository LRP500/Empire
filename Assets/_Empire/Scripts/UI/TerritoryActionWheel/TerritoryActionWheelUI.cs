using System.Collections.Generic;
using Tools;
using UnityEngine;

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

        private Territory _currentTarget = null;

        private List<TerritoryActionUI> _currentActions = null;

        private void Awake()
        {
            EventManager.Instance.Subscribe(GameplayEvent.TerritorySelected, OnTerritorySelected);
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.TerritorySelected, OnTerritorySelected);
        }

        private void Update()
        {
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
            //if (territory != _currentTarget)
            {
                _currentTarget = territory;

                SetActions();
                SetPosition();
            }
        }

        private void SetActions()
        {
            Clear();

            foreach (TerritoryAction action in _currentTarget.State.Actions)
            {
                TerritoryActionInfoUI infoPanel = Instantiate(action.InfoPanelPrefab, _infoContainer);
                infoPanel.Close();

                TerritoryActionUI actionItem = Instantiate(_actionItemPrefab, _actionContainer);
                actionItem.Initialize(_currentTarget, action, infoPanel);
                actionItem.RegisterOnActionExecuted(Close);

                _currentActions.Add(actionItem);
            }
        }

        private void SetPosition()
        {
            transform.position = _mainCamera.WorldToScreenPoint(_currentTarget.transform.position);
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

            foreach (var item in _currentActions)
            {
                Destroy(item.gameObject);
            }

            _currentActions.Clear();
        }
    }
}
