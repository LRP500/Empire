using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tools.Variables;
using UnityEngine;

namespace Empire
{
    public abstract class TerritoryState : ScriptableObject
    {
        [Header("General")]

        [SerializeField]
        private string _name = string.Empty;
        public string Name => _name;

        [SerializeField]
        private ColorVariable _color = null;
        public Color Color => _color.Value;

        [Header("Take Over")]

        [SerializeField]
        [LabelText("Success Chance Modifier (%)")]
        private int _takeOverSuccessChanceModifier = 10;
        public int TakeOverSuccessChanceModifier => _takeOverSuccessChanceModifier;

        [SerializeField]
        [LabelText("Neighboard Modifier (%)")]
        private int _takeOverNeighborModifier = 0;
        public int TakeOverNeighborModifier => _takeOverNeighborModifier;

        [Space]

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
