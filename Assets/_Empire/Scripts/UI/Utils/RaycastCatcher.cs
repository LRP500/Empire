using Tools.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Empire
{
    public class RaycastCatcher : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        [SerializeField]
        private GameEvent _clickedOnNothingEvent;

        private void RaiseEvent()
        {
            _clickedOnNothingEvent.Raise();
        }

        #region Event Handlers

        public void OnPointerDown(PointerEventData eventData)
        {
            RaiseEvent();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            RaiseEvent();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            RaiseEvent();
        }

        #endregion Event Handlers
    }
}
