using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Take Over")]
    public class TerritoryActionTakeOver : TerritoryAction
    {
        public override void Execute(Territory territory)
        {
            base.Execute(territory);

            territory.SetControlled();
        }
    }
}