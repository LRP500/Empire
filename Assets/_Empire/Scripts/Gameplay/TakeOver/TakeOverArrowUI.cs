using Tools;
using Tools.References;
using Tools.Variables;
using UnityEngine;
using UnityEngine.UI;
using TakeOverInfo = Empire.TakeOverManager.TakeOverInfo;

namespace Empire
{
    public class TakeOverArrowUI : MonoBehaviour
    {
        [SerializeField]
        private CameraVariable _camera = null;

        [SerializeField]
        private TerritoryVariable _hoveredTerritory = null;

        [SerializeField]
        private Image _renderer = null;

        [SerializeField]
        private ColorReference _validColor = null;

        [SerializeField]
        private ColorReference _invalidColor = null;

        private bool _dragging = false;

        private Vector3? _originPosition = default;

        private TakeOverInfo _currentTakeOver;

        private void Awake()
        {
            // Receive drag start event from Territory's event handlers
            EventManager.Instance.Subscribe(GameplayEvent.TakeOverDragStart, OnDragStart);
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.TakeOverDragStart, OnDragStart);
        }

        private void Update()
        {
            if (_dragging)
            {
                _currentTakeOver.attacked = _hoveredTerritory;

                HandleInput();
                RefreshLineRenderer();
            }
        }

        private void HandleInput()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                EndDrag();
            }
        }
  
        private void RefreshLineRenderer()
        {
            if (_dragging && _originPosition.HasValue)
            {
                // Vertices
                //_renderer.positionCount = 2;
                //_renderer.SetPosition(0, _originPosition.Value);
                //_renderer.SetPosition(1, GetCurrentMousePosition().GetValueOrDefault());

                // Color
                //bool isValid = TakeOverManager.IsValid(_currentTakeOver);
                //_renderer.startColor = isValid ? _validColor : _invalidColor;
                //_renderer.endColor = isValid ? _validColor : _invalidColor;
            }
        }

        private void OnDragStart(object arg)
        {
            _dragging = true;
            _originPosition = GetCurrentMousePosition();
            _currentTakeOver = _currentTakeOver ?? new TakeOverInfo();
            _currentTakeOver.attacking = arg as Territory;
            _renderer.enabled = true;
        }

        private void EndDrag()
        {
            _dragging = false;
            _renderer.enabled = false;
            _currentTakeOver.attacked = _hoveredTerritory;
            EventManager.Instance.Trigger(GameplayEvent.TakeOver, _currentTakeOver);
        }

        private Vector3? GetCurrentMousePosition()
        {
            Ray ray = _camera.Value.ScreenPointToRay(Input.mousePosition);
            var plane = new Plane(Vector3.forward, Vector3.zero);

            if (plane.Raycast(ray, out float distance))
            {
                return ray.GetPoint(distance);
            }

            return null;
        }
    }
}
