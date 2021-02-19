using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Empire.UI
{
    public class PauseMenuController : PanelUI
    {
        [SerializeField]
        private Button _abandonButton;

        [SerializeField]
        private Button _exitButton;

        [SerializeField]
        private TriggerDefeatAction _triggerDefeat;

        private void Awake()
        {
            Hide();
            Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Toggle();
            }
        }

        private void Initialize()
        {
            _abandonButton.onClick.AddListener(() =>
            {
                _triggerDefeat.Execute();
                Close();
            });

            _exitButton.onClick.AddListener(() =>
            {
                EventManager.Instance.Trigger(SystemEvent.ReturnToTitleMenu);
            });
        }
    }
}