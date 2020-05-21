using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory States/Unreachable")]
    public class TerritoryStateUnreachable : TerritoryState
    {
        public override void UpdateVisualState()
        {
            _territory.Renderer.color = Color;
        }

        public override void Select()
        {
        }
    }
}
