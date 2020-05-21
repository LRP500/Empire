using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Resources/Resource")]
    public class Resource : ScriptableObject
    {
        public ResourceType type;
        public int initial;
        public int current;
    }
}