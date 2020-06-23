using UnityEngine;

namespace Empire
{
    public class WorldMap : MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera = null;

        [SerializeField]
        private TerritoryVariable _hoveredTerritory = null;

        [SerializeField]
        private LayerMask _layerMask = default;

        private void Update()
        {
            GetHoveredTerritory();
        }

        private void GetHoveredTerritory()
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z);
            Vector2 origin = _mainCamera.ScreenToWorldPoint(mousePosition);

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
