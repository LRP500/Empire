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
            _territory.Controlled = false;
            _context.structureManager.ClearStructures(_territory);
        }

        public override void OnEnterState()
        {
            base.OnEnterState();

            _territory.Controlled = true;

            foreach (Territory neighbor in _territory.Neighbors)
            {
                if (neighbor.State is TerritoryStateUnreachable)
                {
                    _context.worldMapManager.SetRival(neighbor);
                }
            }

            _context.structureManager.InitializeTerritoryStructures(_territory);
        }

        public override string ToString()
        {
            return "<color=green>Controlled</>";
        }
    }
}