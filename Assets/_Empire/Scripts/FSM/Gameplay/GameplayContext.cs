using Tools.FSM;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/FSM/Gameplay/Context")]
    public class GameplayContext : AContext
    {
        public ResourceManager resourceManager = null;
        public StructureManager structureManager = null;

        public TerritoryListVariable territories = null;
        public DealListVariable deals = null;
    }
}
