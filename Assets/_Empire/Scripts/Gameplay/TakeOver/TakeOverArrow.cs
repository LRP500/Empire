using Tools;
using Tools.References;
using Tools.Variables;
using UnityEngine;
using static Empire.TakeOverManager;

namespace Empire
{
    public class TakeOverArrow : MonoBehaviour
    {
        [SerializeField]
        private CameraVariable _camera;

        [SerializeField]
        private TerritoryVariable _hoveredTerritory;

        [SerializeField]
        private LineRenderer _lineRenderer;

        [SerializeField]
        private ColorReference _validColor;

        [SerializeField]
        private ColorReference _invalidColor;

        private bool _dragging;

        private Vector3? _originPosition;

        private TakeOverInfo _currentTakeOver;

        private Gradient _gradient;

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
                Vector3 target = GetCurrentMousePosition().GetValueOrDefault();
                CreateSegments(_originPosition.Value, target, 8);
                RefreshColor();
            }
        }

        private void RefreshColor()
        {
            _gradient = _gradient ?? new Gradient();

            // Get line renderer color
            Color color = IsValid(_currentTakeOver) ? _validColor : _invalidColor;
            
            // Set gradient keys
            GradientAlphaKey[] alphaKeys = _lineRenderer.colorGradient.alphaKeys;
            GradientColorKey[] colorKeys =
            {
                new GradientColorKey(color, 0.0f),
                new GradientColorKey(color, 1.0f)
            };

            // Set LineRenderer gradient
            _gradient.SetKeys(colorKeys, alphaKeys);
            //_lineRenderer.colorGradient = _gradient;
        }

        /// <summary>
        /// Create multiple segments so gradient alpha can work properly.
        /// </summary>
        /// <param name="origin">Origin position.</param>
        /// <param name="target">Target position.</param>
        /// <param name="segmentCount">Segment count.</param>
        private void CreateSegments(Vector3 origin, Vector3 target, int segmentCount)
        {
            Vector3 direction = (target - origin).normalized;
            float distance = Vector3.Distance(target, origin);
            Vector3 position = origin;

            _lineRenderer.positionCount = segmentCount;
            for (int i = 0; i < segmentCount; i++)
            {
                _lineRenderer.SetPosition(i, position);
                position += direction * (distance / (segmentCount - 1));
            }
        }

        private void OnDragStart(object arg)
        {
            _dragging = true;
            _originPosition = GetCurrentMousePosition();
            _currentTakeOver ??= new TakeOverInfo();
            _currentTakeOver.attacking = arg as Territory;
            _lineRenderer.enabled = true;
        }

        private void EndDrag()
        {
            _dragging = false;
            _lineRenderer.enabled = false;
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
