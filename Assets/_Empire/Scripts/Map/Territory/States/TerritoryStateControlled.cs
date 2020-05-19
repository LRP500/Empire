using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory States/Controlled")]
    public class TerritoryStateControlled : TerritoryState
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
