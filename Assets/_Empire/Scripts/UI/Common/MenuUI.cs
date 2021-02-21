using Tools.Events;
using UnityEngine;

namespace Empire
{
    public class MenuUI : PanelUI
    {
        [SerializeField]
        private GameEvent _openEvent;

        [SerializeField]
        private GameEvent _closeEvent;

        public override void Open()
        {
            _openEvent.Raise();
            base.Open();
        }

        public override void Close()
        {
            base.Close();
            _closeEvent.Raise();
        }
    }

    public abstract class MenuUI<T1, T2> : MenuUI where T1 : MenuUI where T2 : MenuVariable<T1>
    {
        [SerializeField]
        private T2 _runtimeReference;

        protected override void Awake()
        {
            base.Awake();
            Register();
        }

        private void Register()
        {
            if (_runtimeReference)
            {
                _runtimeReference.SetValue(this as T1);
            }
        }
    }
}
