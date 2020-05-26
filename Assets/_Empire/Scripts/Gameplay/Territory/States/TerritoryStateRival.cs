using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory States/Rival")]
    public class TerritoryStateRival : TerritoryState
    {
        public override void OnEnterState()
        {
            _context.dealManager.RenewDealOffer(_territory);
        }

        public override void Refresh() { }

        public override void RefreshVisualState()
        {
            _territory.Renderer.color = Color;
        }
    }
}