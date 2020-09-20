using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Take Over")]
    public class TerritoryActionTakeOver : TerritoryAction
    {
        [SerializeField]
        private TakeOverSettings _takeOverSettings = null;

        public override void Execute(Territory territory)
        {
            // Taking over a territory will break active deal.
            _context.dealManager.CancelActiveDeal(territory);

            TakeOverOdds odds = new TakeOverOdds(territory);

            if ((Random.value * 100) <= odds.success)
            {
                HandleVictory(territory);
            }
            else
            {
                HandleDefeat(territory);
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

        public override bool CanExecute(Territory territory)
        {
            return true;
        }
    }
}