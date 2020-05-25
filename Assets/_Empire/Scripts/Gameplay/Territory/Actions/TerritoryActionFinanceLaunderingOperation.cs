using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Finance Laundering Operation")]
    public class TerritoryActionFinanceLaunderingOperation : TerritoryAction
    {
        public override void Execute(Territory territory)
        {
            territory.AddLaunderingOperation(new LaunderingOperation());
        }
    }
}
