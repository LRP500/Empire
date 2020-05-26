using UnityEngine;

#pragma warning disable 0414

namespace Empire
{
    public class GameplaySingletonReferenceHelper : MonoBehaviour
    {
        public ResourceManager resourceManager = null;
        public PlayerManager playerManager = null;
        public GameplaySettings gameplaySettings = null;
    }
}