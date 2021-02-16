using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Empire
{
    [CreateAssetMenu(menuName = "Empire/Managers/World Map Manager")]
    public class WorldMapManager : ScriptableManager<WorldMapManager>
    {
        [SerializeField]
        private TerritoryListVariable _territories;
        public IEnumerable<Territory> Territories => _territories.Items;

        [SerializeField]
        private TerritoryListVariable _controlledTerritories;
        public List<Territory> ControlledTerritories => _controlledTerritories.Items;

        [Header("Territory States")]

        [SerializeField]
        [LabelText("Unreachable")]
        private TerritoryState _territoryStateUnreachable;

        [SerializeField]
        [LabelText("Undisputed")]
        private TerritoryState _territoryStateUndisputed;

        [SerializeField]
        [LabelText("Controlled")]
        private TerritoryState _territoryStateControlled;

        [SerializeField]
        [LabelText("In Deal")]
        private TerritoryState _territoryStateInDeal;

        [SerializeField]
        [LabelText("Rival")]
        private TerritoryState _territoryStateRival;

        public override void Initialize()
        {
            _controlledTerritories.Clear();

            foreach (Territory territory in _territories.Items)
            {
                SetUnreachable(territory);
            }
        }

        public void SetStartingTerritory()
        {
            SetControlled(_territories.Random());
        }

        public bool AllTerritoriesControlled()
        {
            foreach (Territory territory in _territories.Items)
            {
                if (!territory.IsControlled)
                {
                    return false;
                }
            }

            return true;
        }

        #region Territory State

        public void SetControlled(Territory territory)
        {
            TransitionTo(territory, _territoryStateControlled);

            if (!_controlledTerritories.Contains(territory))
            {
                _controlledTerritories.Add(territory);
            }
        }

        public void SetUndisputed(Territory territory)
        {
            TransitionTo(territory, _territoryStateUndisputed);
        }

        public void SetInDeal(Territory territory)
        {
            TransitionTo(territory, _territoryStateInDeal);
        }

        public void SetRival(Territory territory)
        {
            _controlledTerritories.Remove(territory);
            TransitionTo(territory, _territoryStateRival);
        }

        private void SetUnreachable(Territory territory)
        {
            TransitionTo(territory, _territoryStateUnreachable);
        }

        private void TransitionTo(Territory territory, TerritoryState state)
        {
            TerritoryState instance = Instantiate(state);
            territory.SetState(instance);

            Debug.Log($"[{name}] Transition to {territory.State.GetType().Name}");
        }

        #endregion Territory State
    }
}
