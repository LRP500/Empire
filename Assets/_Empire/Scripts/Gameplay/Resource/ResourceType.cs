using Sirenix.OdinInspector;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Resources/Resource Type")]
    public class ResourceType : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Basic Info")]
        private string _name = string.Empty;

        [TextArea]
        [SerializeField]
        [BoxGroup("Basic Info")]
        private string _description = string.Empty;

        [SerializeField]
        [PreviewField(75), HideLabel]
        [HorizontalGroup("Resource Data", 75)]
        private Sprite _icon;

        //[SerializeField]
        //[LabelWidth(100)]
        //[VerticalGroup("Resource Data/Stats")]
        //private int _initial = 0;

        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
    }
}
