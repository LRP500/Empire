using Sirenix.OdinInspector;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class GameOverUI : PanelUI
    {
        [SerializeField]
        private GameObject _victoryText;

        [SerializeField]
        private GameObject _defeatText;

        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private Button _exitButton;

        private void Awake()
        {
            Close();

            EventManager.Instance.Subscribe(GameplayEvent.PlayerVictory, OnPlayerVictory);
            EventManager.Instance.Subscribe(GameplayEvent.PlayerDefeat, OnPlayerDefeat);

            _exitButton.onClick.AddListener(OnExitButtonClick);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.PlayerVictory, OnPlayerVictory);
            EventManager.Instance.Unsubscribe(GameplayEvent.PlayerDefeat, OnPlayerDefeat);
        }

        private void OnPlayerVictory(object arg)
        {
            SetVictory();
            Open();
        }

        private void OnPlayerDefeat(object arg)
        {
            SetDefeat();
            Open();
        }

        [Button]
        public void SetVictory()
        {
            _defeatText.SetActive(false);
            _victoryText.SetActive(true);
        }

        [Button]
        public void SetDefeat()
        {
            _victoryText.SetActive(false);
            _defeatText.SetActive(true);
        }

        private static void OnRestartButtonClick()
        {
            EventManager.Instance.Trigger(SystemEvent.StartNewGame);
        }

        private static void OnExitButtonClick()
        {
            EventManager.Instance.Trigger(SystemEvent.ReturnToTitleMenu);
        }
    }
}
