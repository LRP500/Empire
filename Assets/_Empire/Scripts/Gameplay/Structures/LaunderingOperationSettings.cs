using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Gameplay/Settings/Laundering Operation")]
    public class LaunderingOperationSettings : ScriptableObject
    {
        [SerializeField]
        [SuffixLabel("(cash/t)", Overlay = true)]
        private int _initialLaunderingRate = 200;
        public int InitialLaunderingRate => _initialLaunderingRate;
    }
}
