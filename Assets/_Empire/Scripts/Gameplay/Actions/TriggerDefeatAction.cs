using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Actions/Trigger Defeat")]
    public class TriggerDefeatAction : ScriptableAction
    {
        [SerializeField]
        private GameplayContext _gameplayContext;

        public override void Execute()
        {
            int maxThreat = _gameplayContext.threatManager.Threat.Max;
            _gameplayContext.threatManager.IncreaseThreat(maxThreat);
        }
    }
}