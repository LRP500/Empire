using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Resources/Resource Type")]
    public class ResourceType : ScriptableObject
    {
        [SerializeField]
        private string _name = string.Empty;

        [SerializeField]
        private Sprite _icon = null;

        public string Name => _name;
        public Sprite Icon => _icon;
    }
}
