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

        public override void Refresh() { }

        public override void OnEnterState()
        {
            _territory.IsDiscovered = false;
        }

        public override void OnExitState()
        {
            _territory.IsDiscovered = true;
        }

        public override string ToString()
        {
            return "<color=white>Undiscovered</>";
        }
    }
}
