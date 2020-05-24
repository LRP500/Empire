using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class TerritoryFocusUI : PanelUI
    {
        [SerializeField]
        private Image _territoryRenderer = null;

        private Territory _territory = null;

        private void Awake()
        {
            EventManager.Instance.Subscribe(GameplayEvent.TerritoryPrimarySelect, FocusTerritory);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }
        }

        private void FocusTerritory(object arg)
        {
            _territory = arg as Territory;

            Initialize();
            Open();
        }

        private void Initialize()
        {
            _territoryRenderer.sprite = _territory.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
