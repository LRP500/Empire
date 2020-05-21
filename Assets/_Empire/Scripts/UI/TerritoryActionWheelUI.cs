using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Empire
{
    public class TerritoryActionWheelUI : PanelUI
    {
        [SerializeField]
        private GameObject _actionContainer = null;

        [SerializeField]
        private TerritoryActionItemUI _actionItemPrefab = null;

        [SerializeField]
        private Camera _mainCamera = null;

        private Territory _currentTarget = null;

        private List<TerritoryActionItemUI> _currentActions = null;

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
            if (territory != _currentTarget)
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
                TerritoryActionItemUI instance = Instantiate(_actionItemPrefab, _actionContainer.transform);
                instance.Initialize(_currentTarget, action, Close);
                _currentActions.Add(instance);
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
            _currentActions = _currentActions ?? new List<TerritoryActionItemUI>();

            foreach (var item in _currentActions)
            {
                Destroy(item.gameObject);
            }

            _currentActions.Clear();
        }
    }
}
