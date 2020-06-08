using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Empire
{
    public class GameOverUI : PanelUI
    {
        [SerializeField]
        private GameObject _victoryText = null;

        [SerializeField]
        private GameObject _defeatText = null;

        private void Awake()
        {
            Close();

            EventManager.Instance.Subscribe(GameplayEvent.PlayerVictory, OnPlayerVictory);
            EventManager.Instance.Subscribe(GameplayEvent.PlayerDefeat, OnPlayerDefeat);
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
    }
}
