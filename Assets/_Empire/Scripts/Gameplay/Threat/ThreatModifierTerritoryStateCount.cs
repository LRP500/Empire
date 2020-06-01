using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Threat Modifiers/Territory State Count")]
    public class ThreatModifierTerritoryStateCount : ThreatModifier
    {
        public enum Type
        {
            Controlled
        }

        [SerializeField]
        private Type _type = default;

        [SerializeField]
        private NumericalEvaluation _evaluation = NumericalEvaluation.EqualTo;

        [SerializeField]
        private int _compareTo = 0;

        public override bool Evaluate(GameplayContext context)
        {
            int count = 0;

            switch (_type)
            {
                case Type.Controlled:
                    count = context.worldMapManager.ControlledTerritories.Count;
                    break;
                default: break;
            }

            var evaluator = new NumericalEvaluator<int>();
            return evaluator.Evaluate(count, _evaluation, _compareTo);
        }
    }
}
