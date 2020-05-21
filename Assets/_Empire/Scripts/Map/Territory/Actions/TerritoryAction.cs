using UnityEngine;

namespace Empire
{
    public abstract class TerritoryAction : ScriptableObject
    {
        [SerializeField]
        private string _title = string.Empty;
        public string Title => _title;

        public virtual void Execute(Territory target)
        {
            Debug.Log($"[{target.name}] Execute {GetType().Name}");
        }
    }
}
