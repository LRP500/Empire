using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Take Over")]
    public class TerritoryActionTakeOver : TerritoryAction
    {
        public override void Execute(Territory territory)
        {
        }

        public override bool CanExecute(Territory territory)
        {
            return false;
        }
    }
}