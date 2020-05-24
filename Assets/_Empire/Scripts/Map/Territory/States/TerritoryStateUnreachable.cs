using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory States/Unreachable")]
    public class TerritoryStateUnreachable : TerritoryState
    {
        public override void RefreshVisualState()
        {
            _territory.Renderer.color = Color;
        }

        public override void OnEnterState() { }
        public override void Refresh() { }
    }
}
