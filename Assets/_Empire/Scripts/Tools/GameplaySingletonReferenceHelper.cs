using UnityEngine;

namespace Empire
{
    public class GameplaySingletonReferenceHelper : MonoBehaviour
    {
        [SerializeField]
        private ResourceManager _resourceManager = null;

        [SerializeField]
        private PlayerManager _playerManager = null;
    }
}
