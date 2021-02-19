using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    public class GameplayCheats : MonoBehaviour
    {
        [SerializeField]
        private TriggerDefeatAction _triggerDefeat;

        [SerializeField]
        private TriggerVictoryAction _triggerVictory;

        [Button]
        private void TriggerPlayerDefeat()
        {
            _triggerDefeat.Execute();
        }

        [Button]
        private void TriggerPlayerVictory()
        {
            _triggerVictory.Execute();
        }
    }
}