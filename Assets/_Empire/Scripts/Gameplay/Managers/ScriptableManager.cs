using Tools;
using UnityEngine;

namespace Empire
{
    public abstract class ScriptableManager<T> : SingletonScriptableObject<T> where T : ScriptableObject
    {
        [SerializeField]
        protected GameplayContext _context = null;

        public abstract void Initialize();

        public virtual void RefreshOnTick(float elapsed) { }
        public virtual void Refresh(float elapsed = 0f) { }
        public virtual void Clear() { }
    }
}
