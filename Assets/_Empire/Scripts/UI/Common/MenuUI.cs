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
}
