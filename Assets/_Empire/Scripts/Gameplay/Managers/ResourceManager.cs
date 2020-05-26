using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Resource Manager")]
    public class ResourceManager : SingletonScriptableObject<ResourceManager>
    {
        [SerializeField]
        private Resource _bank = null;
        public Resource Bank => _bank;

        [SerializeField]
        private Resource _cash = null;
        public Resource Cash => _cash;

        [SerializeField]
        private Resource _meth = null;
        public Resource Meth => _meth;

        public void Initialize()
        {
            _bank.Initialize();
            _cash.Initialize();
            _meth.Initialize();
        }
    }
}
