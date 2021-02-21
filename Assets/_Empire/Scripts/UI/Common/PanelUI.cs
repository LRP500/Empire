using Sirenix.OdinInspector;
using Tools.Extensions;
using UnityEngine;

namespace Empire
{
    public abstract class PanelUI : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField]
        protected CanvasGroup _group;

        [SerializeField]
        private bool _showOnAwake = true;

        #endregion Serialized Fields

        #region Properties

        public bool IsOpen { get; private set; }

        #endregion Properties

        #region MonoBehaviour

        protected virtual void Awake() { }

        private void Start()
        {
            if (_showOnAwake)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        #endregion MonoBehaviour

        #region Private Methods
        
        [Button]
        protected virtual void Display()
        {
            _group.SetVisible(true);
        }

        [Button]
        protected virtual void Hide()
        {
            _group.SetVisible(false);
        }

        #endregion Private Methods

        #region Public Methods

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

        public virtual void Toggle()
        {
            if (IsOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        #endregion Public Methods
    }
}