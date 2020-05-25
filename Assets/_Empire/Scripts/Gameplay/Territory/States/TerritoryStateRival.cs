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
            RenewDealOffer();
        }

        public override void Refresh()
        {
            _territory.CurrentDealOffer?.Refresh();

            if (_territory.CurrentDealOffer.HasTimedOut())
            {
                RenewDealOffer();
            }
        }

        public override void RefreshVisualState()
        {
            _territory.Renderer.color = Color;
        }

        public void RenewDealOffer()
        {
            _territory.SetDealOffer(new DealOffer(_dealSettings, _territory));
        }
    }
}