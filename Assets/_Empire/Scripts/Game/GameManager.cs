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
            Debug.Log("[GameManager] Before Fade Out");
            yield return StartCoroutine(_sceneFader.FadeOut());
            Debug.Log("[GameManager] After Fade Out / Before Load");
            yield return StartCoroutine(NavigationManager.Instance.DeepLoad(_gameplayScene, null));
            Debug.Log("[GameManager] After Load / Before Fade In");
            yield return StartCoroutine(_sceneFader.FadeIn());
            Debug.Log("[GameManager] After Fade In");
        }

        private IEnumerator ReturnToTitle()
        {
            yield return StartCoroutine(_sceneFader.FadeOut());
            yield return StartCoroutine(NavigationManager.Instance.FastLoad(_mainMenuScene));
            yield return StartCoroutine(_sceneFader.FadeIn());
        }

        private void LoadStartingScene()
        {
            StartCoroutine(NavigationManager.Instance.LoadAdditive(_mainMenuScene));
        }
    }
}