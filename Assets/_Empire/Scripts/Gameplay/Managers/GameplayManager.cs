using Tools.Time;
using UnityEngine;

namespace Empire
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private TimeController _timeController = null;

        [SerializeField]
        private GameplayContext _context = null;

        private void Awake()
        {
            _timeController.RegisterOnTick(RefreshOnTick);
        }

        private void Start()
        {
            StartNewGame();
        }

        private void Update()
        {
            _context.Refresh();
        }

        private void StartNewGame()
        {
            _context.Initialize();
            _context.worldMapManager.SetStartingTerritory();
        }

        private void RefreshOnTick(float elapsed)
        {
            _context.RefreshOnTick(elapsed);
        }
    }
}