using System.Collections;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/FSM/Gameplay/Turn")]
    public class GameplayTurn : GameplayState
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
