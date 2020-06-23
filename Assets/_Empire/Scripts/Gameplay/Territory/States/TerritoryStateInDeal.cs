﻿using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory States/In Deal")]
    public class TerritoryStateInDeal : TerritoryState
    {
        public override void RefreshVisualState()
        {
            _territory.Renderer.color = Color;
        }

        public override void Refresh() { }
        public override void OnEnterState() { }

        public override string ToString()
        {
            return "<color=orange>In Deal</>";
        }
    }
}