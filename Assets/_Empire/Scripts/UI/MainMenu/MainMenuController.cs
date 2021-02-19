using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Empire
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private Button _newGameButton;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _newGameButton.onClick.AddListener(() =>
            {
                EventManager.Instance.Trigger(SystemEvent.StartNewGame);
            });
        }
    }
}