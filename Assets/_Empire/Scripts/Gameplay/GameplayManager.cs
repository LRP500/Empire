using UnityEngine;

namespace Empire
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private WorldMap _worldMap = null;

        private void Start()
        {
            SetupGame();
        }

        private void SetupGame()
        {
            _worldMap.Initialize();
            _worldMap.SetStartingTerritory();
        }
    }
}
