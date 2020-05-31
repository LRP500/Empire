using UnityEngine;

namespace Empire
{
    public abstract class ThreatModifier : ScriptableObject
    {
        [SerializeField]
        private int _incrementModifier = 0;
        public int IncrementModifier => _incrementModifier;

        public abstract bool Evaluate(GameplayContext context);
    }
}