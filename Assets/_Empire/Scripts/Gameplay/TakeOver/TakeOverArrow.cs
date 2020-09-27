using Tools;
using Tools.References;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    public class TakeOverArrow : MonoBehaviour
    {
        [SerializeField]
        private CameraVariable _camera = null;

        [SerializeField]
        private TerritoryVariable _hoveredTerritory = null;

        [SerializeField]
        private LineRenderer _lineRenderer = null;

        [SerializeField]
        private ColorReference _validColor = null;

        [SerializeField]
        private ColorReference _invalidColor = null;

        private bool _dragging = false;

        private Vector3? _originPosition = default;
        private Territory _originTerritory = null;

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
                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPosition(0, _originPosition.Value);
                _lineRenderer.SetPosition(1, GetCurrentMousePosition().GetValueOrDefault());

                // Color
                bool isValid = TakeOverManager.IsValid(_originTerritory, _hoveredTerritory.Value);
                _lineRenderer.startColor = isValid ? _validColor : _invalidColor;
                _lineRenderer.endColor = isValid ? _validColor : _invalidColor;
            }
        }

        private void OnDragStart(object arg)
        {
            _dragging = true;
            _originTerritory = arg as Territory;
            _originPosition = GetCurrentMousePosition();
            _lineRenderer.enabled = true;
        }

        private void EndDrag()
        {
            _dragging = false;
            _lineRenderer.enabled = false;

            EventManager.Instance.Trigger(GameplayEvent.TakeOver, new TakeOverManager.TakeOverInfo
            {
                attacking = _originTerritory,
                attacked = _hoveredTerritory.Value
            });
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
