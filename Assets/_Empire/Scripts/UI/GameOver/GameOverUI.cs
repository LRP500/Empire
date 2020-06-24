using System;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class GameOverUI : PanelUI
    {
        [SerializeField]
        private GameObject _victoryText = null;

        [SerializeField]
        private GameObject _defeatText = null;

        [SerializeField]
        private Button _restartButton = null;

        [SerializeField]
        private Button _exitButton = null;

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

        private void OnRestartButtonClick()
        {
            EventManager.Instance.Trigger(SystemEvent.StartNewGame);
        }

        private void OnExitButtonClick()
        {
            EventManager.Instance.Trigger(SystemEvent.ReturnToTitleMenu);
        }
    }
}
