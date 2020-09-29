using UnityEngine;
using static Empire.TakeOverManager;

namespace Empire
{
    public class TakeOverOdds
    {
        public int success = 50;
        public int failure = 50;

        public TakeOverOdds(int success)
        {
            Initialize(success);
        }

        public TakeOverOdds(TakeOverInfo info, TakeOverSettings settings)
        {
            // Initial success chance + attacked territory state modifier
            int successChance = settings.InitialTakeOverSuccessChance;
            successChance += info.attacked.State.TakeOverSuccessChanceModifier;

            foreach (Territory neighbor in info.attacked.Neighbors)
            {
                successChance += neighbor.State.TakeOverNeighborModifier;
            }

            Initialize(successChance);
        }

        private void Initialize(int success)
        {
            this.success = Mathf.Clamp(success, 0, 100);
            failure = 100 - this.success;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", success, failure);
        }
    }
}
