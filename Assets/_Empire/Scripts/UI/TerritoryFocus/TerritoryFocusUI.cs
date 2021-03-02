using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class TerritoryFocusUI : PanelUI
    {
        [SerializeField]
        private Image _territoryRenderer;

        private Territory _territory;

        //protected override void Awake()
        //{
        //    //base.Awake();
        //    //EventManager.Instance.Subscribe(GameplayEvent.TerritoryPrimarySelect, FocusTerritory);
        //}

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        Close();
        //    }
        //}

        //private void FocusTerritory(object arg)
        //{
        //    _territory = arg as Territory;

        //    Initialize();
        //    Open();
        //}

        //private void Initialize()
        //{
        //    _territoryRenderer.sprite = _territory.GetComponent<SpriteRenderer>().sprite;
        //}
    }
}
