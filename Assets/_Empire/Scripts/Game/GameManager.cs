using Sirenix.OdinInspector;
using System.Collections;
using Tools;
using Tools.Navigation;
using Tools.Time;
using UnityEngine;

namespace Empire
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        [BoxGroup("Scenes")]
        private SceneReference _mainMenuScene;

        [SerializeField]
        [BoxGroup("Scenes")]
        private SceneReference _gameplayScene;

        [SerializeField]
        private TimeController _timeController;

        [SerializeField]
        private SceneFader _sceneFader;

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
            EventManager.Instance.Subscribe(SystemEvent.ReturnToTitleMenu, OnReturnToTitleEvent);
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(SystemEvent.StartNewGame, OnStartNewGameEvent);
            EventManager.Instance.Unsubscribe(SystemEvent.ReturnToTitleMenu, OnReturnToTitleEvent);
        }

        private void OnStartNewGameEvent(object arg = null)
        {
            StartCoroutine(StartNewGame());
        }

        private void OnReturnToTitleEvent(object arg)
        {
            StartCoroutine(ReturnToTitle());
        }

        private IEnumerator StartNewGame()
        {
            _timeController.Pause();
            yield return StartCoroutine(_sceneFader.FadeOut());
            yield return StartCoroutine(NavigationManager.Instance.DeepLoad(_gameplayScene, null));
            yield return StartCoroutine(_sceneFader.FadeIn());
            _timeController.Resume();
        }

        private IEnumerator ReturnToTitle()
        {
            yield return StartCoroutine(_sceneFader.FadeOut());
            yield return StartCoroutine(NavigationManager.Instance.FastLoad(_mainMenuScene));
            _timeController.Resume();
            yield return StartCoroutine(_sceneFader.FadeIn());
        }

        private void LoadStartingScene()
        {
            StartCoroutine(NavigationManager.Instance.LoadAdditive(_mainMenuScene));
        }
    }
}