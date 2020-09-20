using Tools;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    public class TakeOverArrow : MonoBehaviour
    {
        [SerializeField]
        private CameraVariable _camera = null;

        [SerializeField]
        private LineRenderer _lineRenderer = null;

        [SerializeField]
        private TerritoryVariable _hoveredTerritory = null;

        [SerializeField]
        private TerritoryActionTakeOver _takeOverAction = null;

        private bool _dragging = false;

        private Vector3? _origin = default;

        private void Awake()
        {
            EventManager.Instance.Subscribe(GameplayEvent.TakeOverDragStart, OnDragStart);
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.TakeOverDragStart, OnDragStart);
        }

        private void Update()
        {
            HandleInput();
            RefreshLineRenderer();
        }

        private void HandleInput()
        {
            if (_dragging && Input.GetKeyUp(KeyCode.Mouse0))
            {
                EndDrag();
            }
        }

        private void RefreshLineRenderer()
        {
            if (_dragging && _origin.HasValue)
            {
                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPosition(0, _origin.Value);
                _lineRenderer.SetPosition(1, GetCurrentMousePosition().GetValueOrDefault());
            }
        }

        private void OnDragStart(object arg)
        {
            _dragging = true;
            _origin = GetCurrentMousePosition();
            _lineRenderer.enabled = true;
        }

        private void EndDrag()
        {
            _dragging = false;
            _lineRenderer.enabled = false;
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
