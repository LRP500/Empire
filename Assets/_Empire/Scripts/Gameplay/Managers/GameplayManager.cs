using Tools.Time;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private TimeController _timeController = null;

        [SerializeField]
        private GameplayContext _context = null;

        [SerializeField]
        private IntVariable _turnCount = null;

        private void Awake()
        {
            _turnCount.SetValue(0);
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
            _turnCount.Increment();
            _context.RefreshOnTick(elapsed);
        }
    }
}