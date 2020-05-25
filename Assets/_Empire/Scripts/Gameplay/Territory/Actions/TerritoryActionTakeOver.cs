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

        [Space]

        [SerializeField]
        private GameplayContext _context = null;

        [SerializeField]
        private TakeOverSettings _takeOverSettings = null;

        public override void Execute(Territory territory)
        {
            territory.CancelCurrentDeal(_context.deals);

            if ((Random.value * 100) <= _takeOverSettings.SuccessChance)
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

            territory.SetControlled();
        }

        private void HandleDefeat(Territory territory)
        {
            _context.resourceManager.Cash.Decrement(_takeOverSettings.RandomCashAmount());
            _context.resourceManager.Meth.Decrement(_takeOverSettings.RandomMethAmount());
            territory.SetRival();
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
    }
}