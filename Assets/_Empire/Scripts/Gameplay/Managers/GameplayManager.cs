using Sirenix.OdinInspector;
using Tools;
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
        private CameraRigController _cameraRig = null;

        [SerializeField]
        private IntVariable _turnCount = null;

        [SerializeField]
        [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        [DisableIf("@UnityEngine.Application.isPlaying")]
        private BoolVariable _turnBased = null;

        #region MonoBehaviour

        private void Awake()
        {
            _context.Clear();
        }

        private void OnDestroy()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.PlayerAction, OnPlayerAction);
        }

        private void Start()
        {
            StartNewGame();
        }

        private void Update()
        {
            if (!_turnBased)
            {
                Refresh();
            }
        }

        #endregion MonoBehaviour

        private void Refresh()
        {
            if (!CheckGameOverConditions())
            {
                _context.Refresh();
            }
        }

        /// <summary>
        /// Player action callback when playing turn based.
        /// </summary>
        /// <param name="arg"></param>
        private void OnPlayerAction(object arg)
        {
            Refresh();
            RefreshOnTick(0);
        }

        private void StartNewGame()
        {
            // Context
            _context.Initialize();
            _context.worldMapManager.SetStartingTerritory();

            // Turn
            _turnCount.SetValue(0);

            // Refresh
            if (_turnBased)
            {
                EventManager.Instance.Subscribe(GameplayEvent.PlayerAction, OnPlayerAction);
            }
            else
            {
                _timeController.RegisterOnTick(RefreshOnTick);
            }
        }

        private void RefreshOnTick(float elapsed)
        {
            _turnCount.Increment();
            _context.RefreshOnTick(elapsed);
        }

        #region Game Over

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

        #endregion Game Over
    }
}