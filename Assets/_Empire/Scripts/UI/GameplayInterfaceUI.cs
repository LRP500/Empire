using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Empire
{
    public class GameplayInterfaceUI : PanelUI
    {
        [SerializeField]
        private List<PanelUI> _interfacePanels = null;

        private void Awake()
        {
            EventManager.Instance.Subscribe(GameplayEvent.GameOver, OnGamerOverEvent);
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.GameOver, OnGamerOverEvent);
        }

        private void OnGamerOverEvent(object arg)
        {
            Close();
        }

        public override void Close()
        {
            foreach (PanelUI panel in _interfacePanels)
            {
                panel.Close();
            }
        }

        public override void Open()
        {
            foreach (PanelUI panel in _interfacePanels)
            {
                panel.Open();
            }
        }
    }
}