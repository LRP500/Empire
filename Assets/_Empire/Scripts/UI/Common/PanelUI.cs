using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    public abstract class PanelUI : MonoBehaviour
    {
        [SerializeField]
        protected CanvasGroup _group = null;

        public bool IsOpen { get; protected set; } = false;

        [Button]
        protected virtual void Display()
        {
            _group.alpha = 1;
            _group.blocksRaycasts = true;
            _group.interactable = true;
        }

        [Button]
        protected virtual void Hide()
        {
            _group.alpha = 0;
            _group.blocksRaycasts = false;
            _group.interactable = false;
        }

        public virtual void Open()
        {
            IsOpen = true;
            Display();
        }

        public virtual void Close()
        {
            IsOpen = false;
            Hide();
        }
    }
}
