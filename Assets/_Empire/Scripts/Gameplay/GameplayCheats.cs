using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    public class GameplayCheats : MonoBehaviour
    {
        [SerializeField]
        private GameplayContext _gameplayContext = null;

        [Button]
        private void TriggerPlayerDefeat()
        {
            int maxThreat = _gameplayContext.threatManager.Threat.Max;
            _gameplayContext.threatManager.IncreaseThreat(maxThreat);
        }

        [Button]
        private void TriggerPlayerVictory()
        {
            foreach (Territory territory in _gameplayContext.worldMapManager.Territories)
            {
                _gameplayContext.worldMapManager.SetControlled(territory);
            }
        }
    }
}
