using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Make Deal")]
    public class TerritoryActionMakeDeal : TerritoryAction
    {
        [Space]

        [SerializeField]
        private GameplayContext _context = null;

        public override void Execute(Territory territory)
        {
            territory.AcceptDealOffer(_context.deals);
        }
    }
}
