using UnityEngine;
using UnityEngine.EventSystems;

namespace Empire
{
    public class MapGeoState : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private SpriteRenderer _renderer = null;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        #region UI Events

        public void OnPointerEnter(PointerEventData eventData)
        {
            _renderer.color = Color.green;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _renderer.color = Color.white;
        }

        #endregion UI Events
    }
}
