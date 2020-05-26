using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Finance Laundering Operation")]
    public class TerritoryActionFinanceLaunderingOperation : TerritoryAction
    {
        [SerializeField]
        private LaunderingOperationSettings _launderingOperationSettings = null;

        public override void Execute(Territory territory)
        {
            territory.AddLaunderingOperation(new LaunderingOperation(_launderingOperationSettings));
        }
    }
}