using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    public abstract class PanelUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _group = null;

        [Button]
        private void Display()
        {
            _group.alpha = 1;
            _group.blocksRaycasts = true;
            _group.interactable = true;
        }

        [Button]
        private void Hide()
        {
            _group.alpha = 0;
            _group.blocksRaycasts = false;
            _group.interactable = false;
        }

        public virtual void Open()
        {
            Display();
        }

        public virtual void Close()
        {
            Hide();
        }
    }
}
