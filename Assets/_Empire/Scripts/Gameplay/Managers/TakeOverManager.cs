using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Empire
{
    /// <summary>
    /// Take Over Rules :
    /// (1) Resource can be gained or lost in both victory and defeat.
    /// (2) Retaliation means the attackee will attempt to take over all player-controlled neighbors.
    /// (3) Attempting to take over a territory will break active deal and make it rival.
    /// </summary>
    [CreateAssetMenu(menuName = "Empire/Managers/Take Over Manager")]
    public class TakeOverManager : ScriptableManager<TakeOverManager>
    {
        #region Nested Classes

        /// <summary>
        /// Discribes a take over attempt.
        /// </summary>
        public class TakeOverInfo
        {
            public Territory attacking;
            public Territory attacked;

            public TakeOverInfo() {}

            public TakeOverInfo(Territory attacking, Territory attacked)
            {
                this.attacking = attacking;
                this.attacked = attacked;
            }
        }

        #endregion Nested Classes

        #region Serialized Fields

        [SerializeField]
        private TakeOverSettings _takeOverSettings = null;

        #endregion Serialized Fields

        #region Scriptable Manager

        public override void Initialize()
        {
            EventManager.Instance.Subscribe(GameplayEvent.TakeOver, OnTakeOverEvent);
        }

        public override void Clear()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.TakeOver, OnTakeOverEvent);
        }

        #endregion Scriptable Manager

        private void OnTakeOverEvent(object arg)
        {
            ExecuteTakeOver(arg as TakeOverInfo);
        }

        /// <summary>
        /// Execute take over action.
        /// </summary>
        /// <param name="attacking"></param>
        /// <param name="attacked"></param>
        public void ExecuteTakeOver(TakeOverInfo info)
        {
            if (!IsValid(info)) return;

            // See rule (1)
            ProcessResourceOutcome();

            // See rule (3)
            _context.dealManager.CancelActiveDeal(info.attacked);

            TakeOverOdds odds = new TakeOverOdds(info, _takeOverSettings);
            if ((Random.value * 100) <= odds.success)
            {
                ProcessVictory(info);
            }
            else
            {
                ProcessDefeat(info);
            }
        }

        /// <summary>
        /// Process take over victory outcome results.
        /// </summary>
        /// <param name="territory"></param>
        private void ProcessVictory(TakeOverInfo info)
        {
            _context.worldMapManager.SetControlled(info.attacked);
            EventManager.Instance.Trigger(GameplayEvent.TakeOverSuccess);
        }

        /// <summary>
        /// Process take over defeat outcome results.
        /// </summary>
        /// <param name="territory"></param>
        private void ProcessDefeat(TakeOverInfo info)
        {
            ProcessRetaliations(info);
            _context.worldMapManager.SetRival(info.attacked);
            EventManager.Instance.Trigger(GameplayEvent.TakeOverFailed);
        }

        /// <summary>
        /// Process resource gains and losses on player take over attempts.
        /// </summary>
        private void ProcessResourceOutcome()
        {
            // See rule (1)
            if ((Random.value * 100) <= _takeOverSettings.ResourceGainChance)
            {
                _context.resourceManager.AddCash(_takeOverSettings.RandomCashAmount());
                _context.resourceManager.AddMeth(_takeOverSettings.RandomMethAmount());
            }
            else
            {
                _context.resourceManager.RemoveCash(_takeOverSettings.RandomCashAmount());
                _context.resourceManager.RemoveMeth(_takeOverSettings.RandomMethAmount());
            }
        }

        /// <summary>
        /// Process retaliation rules on player take over defeat.
        /// </summary>
        /// <param name="territory"></param>
        private void ProcessRetaliations(TakeOverInfo info)
        {
            // See rule (2)
            foreach (Territory neighbor in info.attacked.Neighbors)
            {
                if (!(neighbor.State is TerritoryStateControlled)) continue;
                else if (neighbor == info.attacking) continue;
                else if (RandomUtils.D100() <= _takeOverSettings.InitialRetaliationSuccessChance)
                {
                    _context.worldMapManager.SetRival(neighbor);
                }
            }
        }

        #region Utils

        public TakeOverOdds GetOdds(TakeOverInfo info)
        {
            return new TakeOverOdds(info, _takeOverSettings);
        }

        public static bool IsValid(TakeOverInfo info)
        {
            // Attacker must be non-null and controlled
            if (info.attacking == null || !info.attacking.IsControlled)
            {
                return false;
            }
            // Attacked must be non-null, uncontrolled and discovered
            else if (info.attacked == null || info.attacked.IsControlled || !info.attacked.IsDiscovered)
            {
                return false;
            }

            // Attacker and attacked must be different and neighbors
            return info.attacking != info.attacked && info.attacking.Neighbors.Contains(info.attacked);
        }

        #endregion Utils
    }
}
