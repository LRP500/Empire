using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory States/Rival")]
    public class TerritoryStateRival : TerritoryState
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