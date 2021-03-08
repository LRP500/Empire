using Tools.Variables;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Resources/Resource Production")]
    public class ResourceProduction : IntVariable
    {
        [SerializeField]
        private string _name;

        [Multiline]
        [SerializeField]
        private string _description;

        public string Name => _name;
        public string Description => _description;
    }
}
