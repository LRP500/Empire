using System.Collections.Generic;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    public abstract class TerritoryState : ScriptableObject
    {
        [SerializeField]
        private ColorVariable _color = null;
        protected Color Color => _color.Value;

        [SerializeField]
        private int _takeOverSuccessChance = 50;
        public int TakeOverSuccessChance => _takeOverSuccessChance;

        [SerializeField]
        private int _takeOverNeighborModifier = 0;
        public int TakeOverNeighborModifier => _takeOverNeighborModifier;

        [SerializeField]
        protected GameplayContext _context = null;

        [SerializeField]
        private List<TerritoryAction> _actions = null;
        public List<TerritoryAction> Actions => _actions;

        protected Territory _territory = null;

        public void SetTerritory(Territory territory)
        {
            _territory = territory;
        }

        public virtual void OnEnterState() { }
        public virtual void OnExitState() { }

        public abstract void Refresh();
        public abstract void RefreshVisualState();
    }
}
