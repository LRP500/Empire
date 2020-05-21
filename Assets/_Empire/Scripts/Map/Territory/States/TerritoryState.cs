using System.Collections.Generic;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    public abstract class TerritoryState : ScriptableObject
    {
        [SerializeField]
        private List<TerritoryAction> _actions = null;
        public List<TerritoryAction> Actions => _actions;

        [SerializeField]
        private ColorVariable _color = null;
        protected Color Color => _color.Value;

        protected Territory _territory = null;

        public void SetContext(Territory territory)
        {
            _territory = territory;
        }

        public abstract void UpdateVisualState();
        public abstract void Select();
    }
}
