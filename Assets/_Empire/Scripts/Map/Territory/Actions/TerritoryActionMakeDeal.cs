﻿using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory Actions/Make Deal")]
    public class TerritoryActionMakeDeal : TerritoryAction
    {
        public override void Execute(Territory territory)
        {
            base.Execute(territory);

            territory.SetInDeal();
        }
    }
}