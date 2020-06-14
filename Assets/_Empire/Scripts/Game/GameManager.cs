using Sirenix.OdinInspector;
using System.Collections;
using Tools;
using Tools.Navigation;
using UnityEngine;

namespace Empire
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        [BoxGroup("Scenes")]
        private SceneReference _mainMenuScene = null;

        [SerializeField]
        [BoxGroup("Scenes")]
        private SceneReference _gameplayScene = null;

        [SerializeField]
        private SceneFader _sceneFader = null;

        private void Awake()
        {
            Initialize();

#if !UNITY_EDITOR
        LoadStartingScene();
#endif
        }

        private void Initialize()
        {
            EventManager.Instance.Subscribe(SystemEvent.StartNewGame, OnStartNewGameEvent);
            EventManager.Instance.Subscribe(SystemEvent.ReturnToTitleMenu, ReturnToTitleMenu);
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(SystemEvent.StartNewGame, OnStartNewGameEvent);
            EventManager.Instance.Unsubscribe(SystemEvent.ReturnToTitleMenu, ReturnToTitleMenu);
        }

        private void OnStartNewGameEvent(object arg = null)
        {
            StartCoroutine(StartNewGame());
        }

        private IEnumerator StartNewGame()
        {
            yield return StartCoroutine(_sceneFader.FadeOut());
            yield return StartCoroutine(NavigationManager.Instance.DeepLoad(_gameplayScene, null));
            yield return StartCoroutine(_sceneFader.FadeIn());
        }

        private void ReturnToTitleMenu(object arg)
        {
            StartCoroutine(NavigationManager.Instance.FastLoad(_mainMenuScene));
        }

        private void LoadStartingScene()
        {
            StartCoroutine(NavigationManager.Instance.LoadAdditive(_mainMenuScene));
        }
    }
}