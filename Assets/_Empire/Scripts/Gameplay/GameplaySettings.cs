using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Gameplay Settings")]
    public class GameplaySettings : SingletonScriptableObject<GameplaySettings>
    {
        public LaboratorySettings laboratorySettings = null;
        public LaunderingOperationSettings launderingOperationSettings = null;
    }
}
