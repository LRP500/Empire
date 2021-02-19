using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Actions/Trigger Victory")]
    public class TriggerVictoryAction : ScriptableAction
    {
        [SerializeField]
        private GameplayContext _gameplayContext;

        public override void Execute()
        {
            foreach (Territory territory in _gameplayContext.worldMapManager.Territories)
            {
                _gameplayContext.worldMapManager.SetControlled(territory);
            }
        }
    }
}