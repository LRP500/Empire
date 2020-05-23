using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory States/Rival")]
    public class TerritoryStateRival : TerritoryState
    {
        [SerializeField]
        private DealOfferSettings _dealSettings = null;

        public override void OnEnterState()
        {
            _territory.SetDealOffer(new DealOffer(_dealSettings, _territory));
        }

        public override void Refresh()
        {
        }

        public override void RefreshVisualState()
        {
            _territory.Renderer.color = Color;
        }

        public override void Select()
        {
        }
    }
}