using System.Collections.Generic;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Deal Manager")]
    public class DealManager : ScriptableManager<DealManager>
    {
        public class TerritoryDealInfo
        {
            public DealOffer currentOffer = null;
            public Deal activeDeal = null;
        }

        [SerializeField]
        private DealOfferSettings _dealSettings = null;

        public Dictionary<Territory, TerritoryDealInfo> Deals { get; private set; } = null;

        public override void Initialize()
        {
            Deals = new Dictionary<Territory, TerritoryDealInfo>();
        }

        public override void Refresh()
        {
            foreach (KeyValuePair<Territory, TerritoryDealInfo> info in Deals)
            {
                if (info.Value.currentOffer != null)
                {
                    info.Value.currentOffer.Refresh();

                    if (info.Value.currentOffer.HasTimedOut())
                    {
                        RenewDealOffer(info.Key);
                    }
                }
            }
        }

        public void RenewDealOffer(Territory territory)
        {
            GetInfo(territory).currentOffer = new DealOffer(territory, _dealSettings);
        }

        public void AcceptDealOffer(Territory territory)
        {
            TerritoryDealInfo info = GetInfo(territory);
            info.activeDeal = info.currentOffer;
            info.currentOffer = null;
        }

        public void CancelActiveDeal(Territory territory)
        {
            GetInfo(territory).activeDeal = null;
        }

        public void SetDealOffer(Territory territory, DealOffer offer)
        {
            GetInfo(territory).currentOffer = offer;
        }

        public TerritoryDealInfo GetInfo(Territory territory)
        {
            if (!Deals.ContainsKey(territory))
            {
                Deals.Add(territory, new TerritoryDealInfo());
            }

            return Deals[territory];
        }
    }
}
