using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory States/Undisputed")]
    public class TerritoryStateUndisputed : TerritoryState
    {
        public override void UpdateVisualState()
        {
            _territory.Renderer.color = Color;
        }

        public override void Select()
        {
            _territory.SetControlled();
        }
    }
}
