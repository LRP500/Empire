using System.Collections;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/FSM/Gameplay/Setup")]
    public class GameplaySetup : GameplayState
    {
        protected override bool CheckEndConditions()
        {
            return false;
        }

        protected override IEnumerator RunExtend()
        {
            yield return null;
        }
    }
}
