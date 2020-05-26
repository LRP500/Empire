using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Settings/Laboratory")]
    public class LaboratorySettings : ScriptableObject
    {
        [SerializeField]
        [SuffixLabel("(lbs/t)", Overlay = true)]
        private int _initialProductionRate = 20;
        public int InitialProductionRate => _initialProductionRate;
    }
}
