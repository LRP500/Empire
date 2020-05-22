using Tools.Time;
using UnityEngine;

namespace Empire
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private TimeController _timeController = null;

        [SerializeField]
        private WorldMap _worldMap = null;

        [SerializeField]
        private ResourceManager _resourceManager = null;

        private void Awake()
        {
            _timeController.RegisterOnTick(Refresh);
        }

        private void Start()
        {
            SetupGame();
        }

        private void SetupGame()
        {
            _worldMap.Initialize();
            _worldMap.SetStartingTerritory();
            _resourceManager.Initialize();
        }

        private void Refresh()
        {
            _resourceManager.Refresh();
        }
    }
}