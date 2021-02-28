using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory States/Undisputed")]
    public class TerritoryStateUndisputed : TerritoryState
    {
        public override void RefreshVisualState()
        {
            _territory.Renderer.color = Color;
        }

        public override void Refresh() { }

        public override string ToString()
        {
            return "<color=white>Undisputed</>";
        }
    }
}
