using Tools;
using UnityEngine;

namespace Empire
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera = null;

        [SerializeField]
        private float _dragSpeed = 2f;

        private Vector3 _dragOrigin;

        private void Update()
        {
            HandleDrag();
        }

        private void HandleDrag()
        {
            // Record initial drag position
            if (Input.GetMouseButtonDown(1))
            {
                _dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(1)) return;

            EventManager.Instance.Trigger(GameplayEvent.CancelSecondaryMouseSelect);
            
            float speed = _dragSpeed * Time.unscaledDeltaTime * 1000;
            Vector3 pos = _mainCamera.ScreenToViewportPoint(Input.mousePosition - _dragOrigin);
            Vector3 move = new Vector3(pos.x * speed, pos.y * speed, transform.position.z);

            // Move camera target
            transform.Translate(-move, Space.World);

            // Record new drag origin
            _dragOrigin = Input.mousePosition;
        }
    }
}
