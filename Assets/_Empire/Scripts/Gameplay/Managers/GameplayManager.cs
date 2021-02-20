using Tools;
using Tools.Time;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private TimeController _timeController;

        [SerializeField]
        private GameplayContext _context;

        [SerializeField]
        private CameraRigController _cameraRig;

        [SerializeField]
        private IntVariable _turnCount;

        private void Awake()
        {
            _context.Clear();
        }

        private void Start()
        {
            StartNewGame();
        }

        private void Update()
        {
            if (!CheckGameOverConditions())
            {
                _context.Refresh();
            }
        }

        private bool CheckGameOverConditions()
        {
            // Check victories before defeats
            if (_context.worldMapManager.AllTerritoriesControlled())
            {
                PlayerVictory();
                return true;
            }
            else if (_context.threatManager.MaxThreatReached)
            {
                PlayerDefeat();
                return true;
            }

            return false;
        }

        private void StartNewGame()
        {
            // Context
            _context.Initialize();
            _context.worldMapManager.SetStartingTerritory();

            // Turn
            _turnCount.SetValue(0);

            // Time tick
            _timeController.Resume();
            TimeController.RegisterOnTick(RefreshOnTick);
        }

        private void RefreshOnTick(float elapsed)
        {
            _turnCount.Increment();
            _context.RefreshOnTick(elapsed);
        }

        private void GameOver()
        {
            _cameraRig.SetLocked(true);
            _timeController.Pause();
            _timeController.SetSpeedMultiplier(0);
            EventManager.Instance.Trigger(GameplayEvent.GameOver);
        }

        private void PlayerDefeat()
        {
            GameOver();
            EventManager.Instance.Trigger(GameplayEvent.PlayerDefeat);
        }

        private void PlayerVictory()
        {
            GameOver();
            EventManager.Instance.Trigger(GameplayEvent.PlayerVictory);
        }
    }
}