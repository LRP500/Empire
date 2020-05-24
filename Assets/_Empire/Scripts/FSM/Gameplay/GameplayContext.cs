using Tools.FSM;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/FSM/Gameplay/Context")]
    public class GameplayContext : AContext
    {
        public PlayerManager playerManager = null;
        public ResourceManager resourceManager = null;

        public DealListVariable deals = null;
        public TerritoryListVariable territories = null;
    }
}
