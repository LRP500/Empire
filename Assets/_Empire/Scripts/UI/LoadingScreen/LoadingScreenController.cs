using Tools.Navigation;
using UnityEngine;

namespace Empire
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField]
        private SceneReference _gameplayScene = null;

        private void Awake()
        {
            StartCoroutine(NavigationManager.Instance.FastLoad(_gameplayScene));
        }
    }
}
