using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Take Over")]
    public class TerritoryActionTakeOver : TerritoryAction
    {
        public class Odds
        {
            public int success = 50;
            public int failure = 50;

            public Odds(int success)
            {
                this.success = Mathf.Clamp(success, 0, 100);
                failure = 100 - this.success;
            }

            public override string ToString()
            {
                string format = (success >= failure) ? "<color=green>{0}/{1}" : "<color=red>{0}/{1}";
                return string.Format(format, success, failure);
            }
        }

        [SerializeField]
        private TakeOverSettings _takeOverSettings = null;

        public override void Execute(Territory territory)
        {
            // Taking over a territory will break active deal.
            _context.dealManager.CancelActiveDeal(territory);

            Odds odds = CalculateOdds(territory);

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
                _context.resourceManager.Cash.Increment(_takeOverSettings.RandomCashAmount());
                _context.resourceManager.Meth.Increment(_takeOverSettings.RandomMethAmount());
            }
            else
            {
                _context.resourceManager.Cash.Decrement(_takeOverSettings.RandomCashAmount());
                _context.resourceManager.Meth.Decrement(_takeOverSettings.RandomMethAmount());
            }

            _context.worldMapManager.SetControlled(territory);
        }

        private void HandleDefeat(Territory territory)
        {
            _context.resourceManager.Cash.Decrement(_takeOverSettings.RandomCashAmount());
            _context.resourceManager.Meth.Decrement(_takeOverSettings.RandomMethAmount());
            _context.worldMapManager.SetRival(territory);
        }

        public Odds CalculateOdds(Territory target)
        {
            int successChance = target.State.TakeOverSuccessChance;

            foreach (Territory neighbor in target.Neighbors)
            {
                successChance += neighbor.State.TakeOverNeighborModifier;
            }

            Odds odds = new Odds(successChance);

            return odds;
        }

        public override bool CanExecute(Territory territory)
        {
            return true;
        }
    }
}