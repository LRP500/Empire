using Tools.Variables;
using UnityEngine;

namespace Empire
{
    public class WorldMap : MonoBehaviour
    {
        [SerializeField]
        private CameraVariable _mainCamera;

        [SerializeField]
        private TerritoryVariable _hoveredTerritory;

        [SerializeField]
        private LayerMask _layerMask;

        private void Update()
        {
            GetHoveredTerritory();
        }

        private void GetHoveredTerritory()
        {
            Transform cameraTransform = _mainCamera.Value.transform;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cameraTransform.position.z);
            Vector2 origin = _mainCamera.Value.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f, _layerMask);

            if (hit)
            {
                _hoveredTerritory.SetValue(hit.collider.gameObject.GetComponent<Territory>());
            }
            else
            {
                _hoveredTerritory.Clear();
            }
        }
    }
}