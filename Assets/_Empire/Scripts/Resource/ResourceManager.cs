using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/Resource Manager")]
    public class ResourceManager : SingletonScriptableObject<ResourceManager>
    {
        [SerializeField]
        private List<Resource> _resources = null;
    }
}
