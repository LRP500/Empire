using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Take Over Manager")]
    public class TakeOverManager : ScriptableManager<TakeOverManager>
    {
        public class TakeOverInfo
        {
            public Territory attacking;
            public Territory attacked;
        }

        [SerializeField]
        private TakeOverSettings _takeOverSettings = null;

        public override void Initialize()
        {
            EventManager.Instance.Subscribe(GameplayEvent.TakeOver, OnTakeOverEvent);
        }

        public override void Clear()
        {
            EventManager.Instance.Unsubscribe(GameplayEvent.TakeOver, OnTakeOverEvent);
        }

        private void OnTakeOverEvent(object arg)
        {
            TakeOverInfo info = arg as TakeOverInfo;
            ExecuteTakeOver(info.attacking, info.attacked);
        }

        public void ExecuteTakeOver(Territory attacking, Territory attacked)
        {
            if (!IsValid(attacking, attacked)) return;

            // Taking over a territory will break active deal
            _context.dealManager.CancelActiveDeal(attacked);

            TakeOverOdds odds = new TakeOverOdds(attacked);

            if ((Random.value * 100) <= odds.success)
            {
                HandleVictory(attacked);
            }
            else
            {
                HandleDefeat(attacked);
            }
        }

        private void HandleVictory(Territory territory)
        {
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

            _context.worldMapManager.SetControlled(territory);

            EventManager.Instance.Trigger(GameplayEvent.TakeOverSuccess);
        }

        private void HandleDefeat(Territory territory)
        {
            _context.resourceManager.RemoveCash(_takeOverSettings.RandomCashAmount());
            _context.resourceManager.RemoveMeth(_takeOverSettings.RandomMethAmount());
            _context.worldMapManager.SetRival(territory);

            EventManager.Instance.Trigger(GameplayEvent.TakeOverFailed);
        }

        public static bool IsValid(Territory attacker, Territory attacked)
        {
            // Attacker must be non-null and controlled
            if (attacker == null || !attacker.IsControlled)
            {
                return false;
            }
            // Attacked must be non-null, uncontrolled and discovered
            else if (attacked == null || attacked.IsControlled || !attacked.IsDiscovered)
            {
                return false;
            }

            // Attacker and attacked must be different and neighbors
            return attacker != attacked && attacker.Neighbors.Contains(attacked);
        }
    }
}
