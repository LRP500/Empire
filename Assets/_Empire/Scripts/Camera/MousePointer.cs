using UnityEngine;

namespace Empire
{
    public class MousePointer : MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera = null;

        [SerializeField]
        private LayerMask _hitLayers = default;

        [SerializeField]
        private float _sensitivity = 1f;

        private void Update()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _hitLayers))
            {
                transform.position = Vector3.Lerp(transform.position, hit.point, _sensitivity * Time.deltaTime);
            }
        }
    }
}
