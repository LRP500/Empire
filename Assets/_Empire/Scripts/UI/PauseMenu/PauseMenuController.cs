using Tools;
using Tools.Time;
using UnityEngine;
using UnityEngine.UI;

namespace Empire.UI
{
    public class PauseMenuController : MenuUI
    {
        [SerializeField]
        private TimeControllerVariable _timeController;

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

        public override void Open()
        {
            _timeController.Value.Freeze();
            base.Open();
        }

        public override void Close()
        {
            base.Close();
            _timeController.Value.Unfreeze();
        }
    }
}