using UnityEngine;

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

        public TakeOverOdds(Territory attacked)
        {
            int successChance = attacked.State.TakeOverSuccessChance;

            foreach (Territory neighbor in attacked.Neighbors)
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
