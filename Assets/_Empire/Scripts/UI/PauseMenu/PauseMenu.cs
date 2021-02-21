using Tools;
using Tools.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Empire.UI
{
    public class PauseMenu : MenuUI<PauseMenu, PauseMenuVariable>
    {
        [SerializeField]
        private Button _abandonButton;

        [SerializeField]
        private Button _exitButton;

        [SerializeField]
        private TriggerDefeatAction _triggerDefeat;

        [SerializeField]
        private CanvasGroup _mainPanel;

        [SerializeField]
        private HelpPanel _helpPanel;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Return to main panel if not already there.
                if (IsOpen && !_mainPanel.IsVisible())
                {
                    NavigateToMain();
                    return;
                }

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
            NavigateToMain();
            base.Open();
        }

        public void NavigateToHelp()
        {
            _mainPanel.SetVisible(false);
            _helpPanel.Open();
        }

        private void NavigateToMain()
        {
            _helpPanel.Close();
            _mainPanel.SetVisible(true);
        }
    }
}