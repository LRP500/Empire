using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Map/Territory States/Controlled")]
    public class TerritoryStateControlled : TerritoryState
    {
        public override void RefreshVisualState()
        {
            _territory.Renderer.color = Color;
        }

        public override void Refresh() { }

        public override void OnExitState()
        {
            _context.structureManager.ClearStructures(_territory);
        }

        public override void OnEnterState()
        {
            base.OnEnterState();

            foreach (Territory neighbor in _territory.Neighbors)
            {
                if (neighbor.State is TerritoryStateUnreachable)
                {
                    neighbor.SetRival();
                }
            }

            _context.structureManager.AddLaboratory(_territory);
            _context.structureManager.AddLaunderingOperation(_territory);
        }
    }
}