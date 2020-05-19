using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Player Info")]
    public class PlayerInfo : ScriptableObject
    {
        private PlayerResourceInfo _resources = null;
    }
}
